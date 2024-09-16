namespace tech_test.Models
{
    public class MempoolBalance
    {
        public string txid { get; set; }
        public int version { get; set; }
        public int locktime { get; set; }
        public List<Vin> vin { get; set; }
        public List<Vout> vout { get; set; }
        public int size { get; set; }
        public int weight { get; set; }
        public int sigops { get; set; }
        public int fee { get; set; }
        public Status status { get; set; }
    }
}

