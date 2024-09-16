namespace tech_test.Interface
{
    public interface IBalanceVariation
    {
        Task<(long balance30DaysAgo, long balance7DaysAgo)> BalanceVariations(string address);
    }
}
