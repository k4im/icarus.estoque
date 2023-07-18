using estoque.domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace estoque.infra.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        { }

        public DbSet<Produto> Produtos { get; set; }
    }
}