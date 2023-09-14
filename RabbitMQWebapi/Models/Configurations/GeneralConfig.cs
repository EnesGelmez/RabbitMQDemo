namespace RabbitMQWebapi.Models.Configurations
{
    public class GeneralConfig : IGeneralConfig
    {
        private string _sqlConnectionString;

        public string SqlConnectionString
        {
            get { return _sqlConnectionString; }
            set { _sqlConnectionString = value; }
        }

        private string uriString;

        public string UriString
        {
            get { return uriString; }
            set { uriString = value; }
        }

        private int _workerControlTime;

        public int WorkerControlTime
        {
            get { return _workerControlTime; }
            set { _workerControlTime = value; }
        }

        private bool _useWorker;
        public bool UseWorker
        {
            get { return _useWorker; }
            set { _useWorker = value; }
        }
    }
}
