namespace RabbitMQWebapi.Models.BaseModels
{
    public abstract class GeneralErrorBaseObject : IGeneralErrorBaseObject
    {
        private DateTime _responseDate;

        public DateTime ResponseDate
        {
            get { return _responseDate; }
            set { _responseDate = value; }
        }

    }
}
