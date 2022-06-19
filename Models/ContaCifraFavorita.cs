using System.ComponentModel.DataAnnotations;

namespace cifraspp_API.Models
{
    public class ContaCifraFavorita
    {
        [Key] public int idCifraFavorita { get; set; }
              public int idCifra { get; set; }
              public int idConta { get; set; }
    }
}