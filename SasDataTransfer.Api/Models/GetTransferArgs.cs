namespace SasDataTransfer.Api.Models
{
    public class GetTransferArgs
    {
        public string Protocol { get; set; }
        public string Analysis { get; set; }
        public string SjmJob { get; set; }
        public string? User { get; set; }
    }
}
