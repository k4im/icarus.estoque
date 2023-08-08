namespace estoque.infra.AssynComm.Extensions;

public class Base
{
    /* Variaveis utilizadas pelo consumidor*/
    public string FilaEstoque = "atualizar.estoque";
    public string ExchangeEstoque = "projeto.adicionado/api.projetos";
    public string routingKeyEstoque = "projeto.atualizar.estoque";

    /* Variavies utilizadas pelo publisher*/
    public string filaDeletados = "produtos.disponiveis.deletados";
    public string routingKeyDeletado = "produtos.disponiveis.produto.deletado";
    public string filaAdicionados = "produtos.disponiveis";
    public string routingKeyAdicionado = "produtos.disponiveis.produto.adicionado";
    public string filaAtualizados = "produtos.disponiveis.atualizados";
    public string routingKeyAtualizado = "produtos.disponiveis.produto.atualizado";
    public string exchange = "produtos/api.estoque";
}
