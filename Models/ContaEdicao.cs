using System.ComponentModel.DataAnnotations;

namespace cifraspp_API.Models
{
    public class ContaEdicao
    {
        [Key] public int idEdicao { get; set; }
              public int idCifra { get; set; }
              public int idConta { get; set; }
    }
}