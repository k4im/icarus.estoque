namespace estoque.infra.AssynComm;

public interface IMessagePublisher
{
    void PublicarProduto(Produto produto);
    void AtualizarProduto(Produto produto);
    void DeletarProduto(Produto produto);
}
