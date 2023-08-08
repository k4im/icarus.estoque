namespace estoque.infra.AssynComm.Extensions;

public class MessagePublisherExtension : Base
{
    public void declararFilas(IModel channel)
    {
        // Definindo a fila no RabbitMQ
        channel.QueueDeclare(queue: filaAdicionados, durable: true,
            exclusive: false,
            autoDelete: false);

        // Definindo a fila no RabbitMQ
        channel.QueueDeclare(queue: filaAtualizados, durable: true,
            exclusive: false,
            autoDelete: false);

        // Definindo a fila no RabbitMQ
        channel.QueueDeclare(queue: filaDeletados, durable: true,
            exclusive: false,
            autoDelete: false);

        // Definindo o Exchange no RabbitMQ
        channel.ExchangeDeclare(exchange: exchange,
        type: ExchangeType.Topic,
        durable: true,
        autoDelete: false);

        // Linkando a fila ao exchange
        channel.QueueBind(queue: filaAdicionados,
            exchange: exchange,
            routingKey: routingKeyAdicionado);

        // Linkando a fila ao exchange
        channel.QueueBind(queue: filaAtualizados,
            exchange: exchange,
            routingKey: routingKeyAtualizado);

        // Linkando a fila ao exchange
        channel.QueueBind(queue: filaDeletados,
            exchange: exchange,
            routingKey: routingKeyDeletado);

    }
}
