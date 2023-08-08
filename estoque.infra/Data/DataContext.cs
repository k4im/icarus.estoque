namespace estoque.infra.Data;

public class DataContext : DbContext
{
    public DataContext() : base(new DbContextOptionsBuilder().UseSqlite("Data Source=estoque.db;").Options)
    {
        Database.EnsureCreated();
    }

    public DbSet<Produto> Produtos { get; set; }
}
