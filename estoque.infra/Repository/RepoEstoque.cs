using MySqlConnector;

namespace estoque.infra.Repository;

public class RepoEstoque : IRepoEstoque
{
    readonly IMessagePublisher _publisher;
    delegate void produtoEventHandler(Produto model);
    event produtoEventHandler AoCriarProduto;
    event produtoEventHandler AoDeletarProduto;
    event produtoEventHandler AoAtualizarProduto;
    public string connStr = Environment.GetEnvironmentVariable("DB_CONNECTION");
    public RepoEstoque(IMessagePublisher publisher)
    {
        _publisher = publisher;
        AoCriarProduto += _publisher.PublicarProduto;
        AoAtualizarProduto += _publisher.AtualizarProduto;
        AoDeletarProduto += _publisher.DeletarProduto;
    }

    public async Task<bool> adicionarProduto(Produto model)
    {
        try
        {
            using var db = new DataContext();
            db.Produtos.Add(model);
            await db.SaveChangesAsync();
            AoCriarProduto.Invoke(model);
            return true;

        }
        catch (DbUpdateConcurrencyException)
        {
            Console.WriteLine("Não foi possivel realizar a operação, a mesma já foi realizada por outro usuario!");
            return false;
        }
        catch (ArgumentException e)
        {
            // Caso o software tente adicionar um item mais de uma vez 
            // problema identificado com o IHostServiced rodando em background
            Console.WriteLine($"Debug: {e.Message}");
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Não foi possivel realizar a operação no repo: {e.Message}");
            return false;
        }
    }

    public async Task<bool> atualizarProduto(int? id, Produto model)
    {
        try
        {
            using var db = new DataContext();
            var produto = await db.Produtos.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            produto.atualizarProduto(model);
            await db.SaveChangesAsync();
            AoAtualizarProduto.Invoke(produto);
            return true;

        }
        catch (DbUpdateConcurrencyException)
        {
            Console.WriteLine("Não foi possivel realizar a operação, a mesma já foi realizada por outro usuario!");
            return false;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Não foi possivel realizar a operação no repo: {e.Message}");
            return false;
        }
    }

    public async Task<ProdutoDTO> buscarProdutoId(int? id)
    {
        try
        {
            var query = "SELECT * FROM Produtos WHERE Id LIKE @busca";
            using var conn = new MySqlConnection(connStr);
            var produto = await conn.QueryFirstOrDefaultAsync<ProdutoDTO>(query, new { busca = id });
            if (produto == null) return null;
            return produto;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<Response<ProdutoDTO>> buscarProdutos(int pagina, float resultado)
    {
        var query = "SELECT * FROM Produtos LIMIT @resultados OFFSET @paginas";
        var queryCount = "SELECT COUNT(*) FROM Produtos";
        try
        {

            using var conn = new MySqlConnection(connStr);
            var total = conn.ExecuteScalar<int>(queryCount);
            var paginasTotal = Math.Ceiling(total / resultado);
            var produtos = await conn.QueryAsync<ProdutoDTO>(query, new { paginas = pagina, resultados = resultado });
            return new Response<ProdutoDTO>(produtos.ToList(), pagina, (int)paginasTotal);
        }
        catch (Exception e)
        {
            Console.WriteLine("Ocorreu um erro ao realizar a operação: " + e.Message);
            return null;
        }
    }

    public async Task<bool> removerProduto(int? id)
    {
        try
        {
            using var db = new DataContext();
            var produto = await db.Produtos.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            if (produto == null) return false;
            db.Remove(produto);
            await db.SaveChangesAsync();
            AoDeletarProduto.Invoke(produto);
            return true;

        }
        catch (DbUpdateConcurrencyException)
        {
            Console.WriteLine("Não é possivel realizar esta operação, a mesma já foi realizada");
            return false;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Não foi possivel realizar a operação no repo: {e.Message}");
            return false;
        }
    }

    public async Task atualizarEstoque(EnvelopeRecebido model)
    {
        try
        {
            using var db = new DataContext();
            var produto = await db.Produtos.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == model.ProdutoUtilizado);
            if (produto == null) Console.WriteLine("Produto nulo");
            produto.diminuirQuantidade(model.QuantidadeUtilizado);
            await db.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            Console.WriteLine("Não é possivel realizar esta operação, a mesma já foi realizada");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Não foi possivel realizar a operação no repo: {e.Message}");
        }
    }
}
