using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using tech_test.Interface;

namespace tech_test.Controllers
{
    public class BalanceController : Controller
    {
        private readonly IOnChainBalance _onChainBalance;
        private readonly IMempoolBalances _mempoolBalances;
        private readonly IBalanceVariation _balanceVariation;



        public BalanceController(IOnChainBalance onChainBalance, IMempoolBalances mempoolBalances, IBalanceVariation balanceVariation)
        {
            _onChainBalance = onChainBalance;
            _mempoolBalances = mempoolBalances;
            _balanceVariation = balanceVariation;
        }

      

        // GET: BalanceController/OnChainBalance/{address}
        public async Task<ActionResult> OnChainBalance(string address)
        {
            try
            {
                long balance = await _onChainBalance.GetOnChainBalance(address);
                ViewBag.Balance = balance;
            }
            catch (Exception ex)
            {
                // Manejo de errores
                ViewBag.ErrorMessage = "Error al obtener el balance: " + ex.Message;
            }

            return View();
        }


        public async Task<ActionResult> MempoolBalance(string address)
        {
            try
            {
                long balance = await _mempoolBalances.MempoolBalances(address);
                ViewBag.Balance = balance;
            }
            catch (Exception ex)
            {
                // Manejo de errores
                ViewBag.ErrorMessage = "Error al obtener el balance: " + ex.Message;
            }

            return View();
        }

        public async Task<ActionResult> BalanceVariation(string address)
        {
            try
            {
                // Obtiene los balances de 30 días y 7 días
                var (balance30DaysAgo, balance7DaysAgo) = await _balanceVariation.BalanceVariations(address);

                // Pasa los balances a la vista usando ViewBag
                ViewBag.Balance30DaysAgo = balance30DaysAgo;
                ViewBag.Balance7DaysAgo = balance7DaysAgo;
            }
            catch (Exception ex)
            {
                // Manejo de errores
                ViewBag.ErrorMessage = "Error al obtener el balance: " + ex.Message;
            }

            return View();
        }

    }
}
