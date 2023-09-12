namespace estoque.infra.AssynComm;

public class MessagePublisher : MessagePublisherExtension, IMessagePublisher
{
    readonly IConfiguration _config;
    readonly IConnection _connection;
    readonly IModel _channel;
    readonly IHttpContextAccessor _content;
    public MessagePublisher(IConfiguration config, IHttpContextAccessor content)
    {
        _config = config;
        _content = content;
        // Definindo a ConnectionFactory, passando o hostname, user, pwd, port
        var factory = new ConnectionFactory()
        {
            HostName = _config["RabbitMQ"],
            UserName = Environment.GetEnvironmentVariable("RABBIT_MQ_USER"),
            Password = Environment.GetEnvironmentVariable("RABBIT_MQ_PWD"),
            Port = Convert.ToInt32(_config["RabbitPort"])
        };

        try
        {
            //Criando a conexão ao broker
            _connection = factory.CreateConnection();
            // Criando o modelo da conexão
            _channel = _connection.CreateModel();
            declararFilas(_channel);
            _connection.ConnectionShutdown += RabbitMQFailed;
        }
        catch (Exception e)
        {
            Console.WriteLine($"--> Não foi possivel se conectar com o Message Bus: {e.Message}");
        }

    }
    public void PublicarProduto(Produto produto)
        => EnviarEvento(SerializarObjeto(produto), routingKeyAdicionado);

    public void AtualizarProduto(Produto produto)
        => EnviarEvento(SerializarObjeto(produto), routingKeyAtualizado);
    public void DeletarProduto(Produto produto)
        => EnviarEvento(SerializarObjeto(produto), routingKeyDeletado);

    void EnviarEvento(string evento, string routingKey)
    {
        if (_channel.IsOpen)
        {
            // transformando o json em array de bytes
            var body = Encoding.UTF8.GetBytes(evento);

            // Persistindo a mensagem no broker
            var props = _channel.CreateBasicProperties();
            props.Persistent = true;

            // Realizando o envio para o exchange 
            _channel.BasicPublish(exchange: exchange,
                routingKey: routingKey,
                basicProperties: props,
                body: body);
            Console.WriteLine("--> Evento enviado ao RabbitMQ");
        }
    }
    string SerializarObjeto(Produto model)
    {
        var correlationID = _content.HttpContext.Request.Headers["X-CorrelationID"].ToString();
        var produtoModel = new Envelope(model.Id, model.Nome, model.Quantidade, correlationID);
        return JsonConvert.SerializeObject(produtoModel);
    }
    void RabbitMQFailed(object state, ShutdownEventArgs e)
       => Console.WriteLine("RabbitMQ encerrou de forma inesperada");
}
