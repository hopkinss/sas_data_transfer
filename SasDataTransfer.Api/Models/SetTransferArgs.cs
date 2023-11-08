using System.Security.Permissions;

namespace SasDataTransfer.Api.Models
{
    public class SetTransferArgs
    {
        public string Protocol { get; set; }
        public string Analysis { get; set; }
        public string InDir { get; set; }
        public string OutDir { get; set; }
        public string SjmJob { get; set; }
        public string? User { get; set; }
        public DateTime? RequestDate { get; set; }
    }
}
