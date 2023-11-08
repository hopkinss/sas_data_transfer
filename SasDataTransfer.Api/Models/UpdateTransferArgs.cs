namespace SasDataTransfer.Api.Models
{
    public class UpdateTransferArgs
    {        
        public int TransferId { get; set; }
        public bool IsSuccess { get; set; }
        public Dictionary<string,List<string>>? Data { get; set; }
    }
}
