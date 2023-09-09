using RabbitMQWebapi.Models.BaseModels;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RabbitMQWebapi.Models.ResponseModels
{
    public class GeneralResponse<T> : GeneralResponseBaseObject
    {
        protected GeneralError? _generalError;
        //public bool HasError => GeneralError != null;

        public GeneralError GeneralError
        {
            get => _generalError;
            set => _generalError = value;
        }

        public T Data { get; set; }

        public DateTime ResponseTime => DateTime.Now;
    }
}
