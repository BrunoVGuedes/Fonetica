using NLog;
using NLog.Common;
using NLog.Config;
using NLog.Targets;
using RabbitMQ.Client;
using System;
using System.Text;

namespace Lider.DPVAT.APIFonetica.Infra.Agent
{
    [Target("RabbitMQ")]
    public class RabbitMQTarget : TargetWithLayout
    {

        #region Private Fields

        private IConnection connection;
        private ConnectionFactory factory;

        #endregion Private Fields

        #region Public Constructors     

        #endregion Public Constructors

        #region Public Properties

        [RequiredParameter]
        public string Exchange { get; set; }

        public string HostName { get; set; }

        [RequiredParameter]
        public string Password { get; set; }

        public int Port { get; set; }

        [RequiredParameter]
        public string RoutingKey { get; set; }

        [RequiredParameter]
        public string UserName { get; set; }

        public string VirtualHost { get; set; }

        #endregion Public Properties

        #region Protected Methods

        protected override void CloseTarget()
        {
            connection?.Close();

            base.CloseTarget();
        }

        protected override void InitializeTarget()
        {
            try
            {
                if (string.IsNullOrEmpty(Exchange))
                {
                    throw new ArgumentNullException(Exchange);
                }

                factory = new ConnectionFactory { HostName = HostName, VirtualHost = VirtualHost, Port = Port, UserName = UserName, Password = Password };
                connection = factory.CreateConnection();

                // Declare the exchange
                using (var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare(Exchange, "direct", true, true);
                }
            }
            catch (Exception ex)
            {
                InternalLogger.Error($"Could not setup NLog target {GetType().Name} - {ex.ToString()}");
            }

            base.InitializeTarget();
        }

        protected override void Write(LogEventInfo logEvent)
        {
            var logMessage = Layout.Render(logEvent);

            try
            {
                using (var channel = connection.CreateModel())
                {
                    var body = Encoding.UTF8.GetBytes(logMessage);

                    var props = channel.CreateBasicProperties();
                    props.ContentType = "text/plain";
                    props.DeliveryMode = 2;

                    channel.BasicPublish(exchange: Exchange,
                                         routingKey: RoutingKey,
                                         basicProperties: null,
                                         body: body);
                }
            }
            catch (Exception ex)
            {
                InternalLogger.Error("{ Could not send to RabbitMQ: {0} Log: {1} }", ex.ToString(), logMessage);
            }
        }

        #endregion Protected Methods

    }
}