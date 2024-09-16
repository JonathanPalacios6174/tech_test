namespace tech_test.Interface
{
    public interface IMempoolBalances
    {
        Task<long> MempoolBalances(string address);
    }
}
