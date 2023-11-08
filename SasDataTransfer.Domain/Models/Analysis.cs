using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SasDataTransfer.Domain.Models
{
    [Index(nameof(AnalysisName),IsUnique =true)]
    public class Analysis
    {
        public int Id { get; set; }
        [Required]
        public string AnalysisName { get; set; }       
        public int ProtocolId { get; set; }
        public Protocol Protocol { get; set; }
   
        public virtual ICollection<Transfer> Transfers { get; set; }
    }
}