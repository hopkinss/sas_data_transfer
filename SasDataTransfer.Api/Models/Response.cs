namespace SasDataTransfer.Api.Models
{
    public class Response
    {
        public Response()
        {
                
        }
        public Response(bool isSuccess,string message,MessageType msgType)
        {
            IsSuccess = isSuccess;
            Message = message;
            MessageType = msgType;
        }
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public MessageType MessageType { get; set; }
        public int? TransferId { get; set; }
    }
}
