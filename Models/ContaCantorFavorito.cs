using System.ComponentModel.DataAnnotations;

namespace cifraspp_API.Models
{
    public class ContaCantorFavorito
    {
        [Key] public int idCantorFavorito { get; set; }
              public int idCantor { get; set; }
              public int idConta { get; set; }
    }
}