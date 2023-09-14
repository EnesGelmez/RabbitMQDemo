namespace RabbitMQWebapi.Models.Configurations
{
    public interface IGeneralConfig
    {
        public string SqlConnectionString { get; }
        public string UriString { get; }
        public int WorkerControlTime { get; }
        public bool UseWorker { get; }
    }
}
