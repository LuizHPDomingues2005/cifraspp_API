using System.ComponentModel.DataAnnotations;

namespace cifraspp_API.Models
{
    public class Comentario
    {

        [Key]

        public int idComentario { get; set; }
        public int idCifra { get; set; }
        public string? mensagem { get; set; }

    }
}