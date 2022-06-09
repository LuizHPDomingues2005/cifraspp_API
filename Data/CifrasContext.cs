using Microsoft.EntityFrameworkCore;
using cifraspp_API.Models;

namespace cifraspp_API.Data
{
    public class CifrasContext : DbContext
    {
        public CifrasContext(DbContextOptions<CifrasContext> options) : base(options)
        {

        }

        // definição dos contextos baseados nos modelos de dados
        public DbSet<Conta> Conta { get; set; }
        public DbSet<Cifra> Cifra { get; set; }
        public DbSet<Cantor> Cantor { get; set; }
        public DbSet<Comentario> Comentario { get; set; }
        public DbSet<Genero> Genero { get; set; }
    }
}