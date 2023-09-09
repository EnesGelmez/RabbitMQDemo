namespace RabbitMQWebapi.Models.BaseModels
{
    public class GeneralError : GeneralErrorBaseObject
    {
        private string? message;

        private int key;

        public IDictionary<int, string>? ValidationErrors;

        public int Key
        {
            get
            {
                return key;
            }
            set
            {
                key = value;
            }
        }

        public string Message
        {
            get
            {
                return message;
            }
            set
            {
                message = value;
            }
        }

        public string DisplayMessage { get; set; }

        public string TechnicalDescription { get; set; }

        public GeneralError()
        {
            
        }
        public GeneralError(Exception exception) => message = $"Error:{exception.ToString()}";

        public GeneralError(int errorId) => message = $"Error:{errorId.ToString()}";

        public GeneralError(int errorId, string _message)
        {
            key = errorId;
            message = _message;
        }

    }
}
