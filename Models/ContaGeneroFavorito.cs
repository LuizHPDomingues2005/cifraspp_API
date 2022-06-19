using System.ComponentModel.DataAnnotations;

namespace cifraspp_API.Models
{
    public class ContaGeneroFavorito
    {
        [Key] public int idGeneroFavorito { get; set; }
              public int idGenero { get; set; }
              public int idConta { get; set; }
    }
}