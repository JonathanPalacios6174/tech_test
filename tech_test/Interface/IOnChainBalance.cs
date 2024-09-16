namespace tech_test.Interface
{
    public interface IOnChainBalance
    {
        Task<long> GetOnChainBalance(string address);
    }
}
