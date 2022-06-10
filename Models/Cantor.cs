using System.ComponentModel.DataAnnotations;

namespace cifraspp_API.Models
{
    public class Cantor
    {
        [Key]

        public int idCantor { get; set; }
        public string? nomeCantor { get; set; }
        public int? qtdDeCifras { get; set; }
        

    }
}