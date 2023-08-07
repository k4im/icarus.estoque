namespace estoque.domain.Entity;

public class Envelope
{
    public Envelope(int id, string nome, int quantidade, string correlationID)
    {
        Id = id;
        Nome = nome;
        Quantidade = quantidade;
        CorrelationID = correlationID;
    }

    public int Id { get; }
    public string Nome { get; }
    public int Quantidade { get; }
    public string CorrelationID { get; }
}
