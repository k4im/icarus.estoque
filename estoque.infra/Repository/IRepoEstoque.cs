namespace estoque.infra.Repository;

public interface IRepoEstoque
{
    Task<ProdutoDTO> buscarProdutoId(int? id);
    Task<Response<ProdutoDTO>> buscarProdutos(int pagina, float resultado);
    Task<Response<ProdutoDTO>> BuscarProdutosPorNome(string filtro, int pagina, float resultado);
    Task<bool> adicionarProduto(Produto model);

    Task<bool> removerProduto(int? id);

    Task<bool> atualizarProduto(int? id, Produto model);
    Task atualizarEstoque(EnvelopeRecebido model);
}
