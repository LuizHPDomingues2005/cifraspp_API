using System.ComponentModel.DataAnnotations;

namespace cifraspp_API.Models
{
    public class ContaComentario
    {
        [Key] public int idContaComentario { get; set; }
              public int idComentario { get; set; }
              public int idConta { get; set; }
    }
}