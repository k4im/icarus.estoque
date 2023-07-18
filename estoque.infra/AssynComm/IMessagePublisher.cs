namespace estoque.infra.AssynComm
{
    public interface IMessagePublisher
    {
        void publicarProduto(Produto produto);
        void atualizarProduto(Produto produto);
        void deletarProduto(Produto produto);
    }
}