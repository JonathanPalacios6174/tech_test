namespace tech_test.Models
{
    public class Status
    {
        public bool confirmed { get; set; }
        public long? block_time { get; set; } // Agregado para capturar el tiempo del bloque
        public int? block_height { get; set; } // Agregado si es necesario para el cálculo
    }

}
