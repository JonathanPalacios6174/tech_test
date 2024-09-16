namespace tech_test.Models
{
    public class Transaction
    {
        public long time { get; set; } // Time of transaction in UNIX format
        public long balance { get; set; } // Balance change in SATS
        public int confirmations { get; set; } // Confirmations of the transaction
    }
}
