namespace cifraspp_API.Models
{
    public class Cifra
    {
        public int idCifra { get; set; }
        public int idCantor { get; set; }
        public int idGenero { get; set; }
        public string? nomeMusica { get; set; }
        public DateTime dataCriada { get; set; }
        public DateTime dataEditada { get; set; }
        public string? letraEAcordes { get; set; }

    }
}