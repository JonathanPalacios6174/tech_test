namespace tech_test.Models
{
    public class MempoolStats
    {
        public int funded_txo_count { get; set; }
        public int funded_txo_sum { get; set; }
        public int spent_txo_count { get; set; }
        public int spent_txo_sum { get; set; }
        public int tx_count { get; set; }
    }
}
