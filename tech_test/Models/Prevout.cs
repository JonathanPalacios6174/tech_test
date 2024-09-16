namespace tech_test.Models
{
    public class Prevout
    {
        public string scriptpubkey { get; set; }
        public string scriptpubkey_asm { get; set; }
        public string scriptpubkey_type { get; set; }
        public string scriptpubkey_address { get; set; }
        public int value { get; set; }
    }
}
