using System.ComponentModel.DataAnnotations;

namespace cifraspp_API.Models
{
    public class Genero
    {

        [Key]

        public int idGenero { get; set; }
        public string? nomeGenero { get; set; }
        public int? qtdDeCifras { get; set; }
        
    }
}