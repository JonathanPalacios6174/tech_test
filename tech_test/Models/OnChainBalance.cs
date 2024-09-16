namespace tech_test.Models
{
    public class OnChainBalance
    {
        public string address { get; set; }
        public ChainStats chain_stats { get; set; }
        public MempoolStats mempool_stats { get; set; }
    }
}
