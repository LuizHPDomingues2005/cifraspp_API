namespace cifraspp_API.Models
{
    public class Conta
    {
        public int idConta { get; set; }
        public string? username { get; set; }
        public string? senha { get; set; }
        public string? email { get; set; }
        public bool adm { get; set; }
        public int qtdComentarios { get; set; }
        
    }
}