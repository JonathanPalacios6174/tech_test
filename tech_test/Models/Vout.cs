namespace tech_test.Models
{
    public class Vout
    {
        public string txid { get; set; }
        public int vout { get; set; }
        public Prevout prevout { get; set; } // Propiedad Prevout incluida
        public string scriptsig { get; set; }
        public string scriptsig_asm { get; set; }
        public List<string> witness { get; set; }
        public bool is_coinbase { get; set; }
        public long sequence { get; set; }
        public string inner_witnessscript_asm { get; set; }
    }
}
