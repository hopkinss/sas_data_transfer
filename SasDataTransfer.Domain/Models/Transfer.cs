using System.ComponentModel.DataAnnotations.Schema;

namespace SasDataTransfer.Domain.Models
{
    public class Transfer
    {
        public int Id { get; set; }
        public string UserAccount { get; set; }
        public DateTime RequestDate { get; set; }
        public string InputLoc { get; set; }
        public string OutputLoc { get; set; }
        public string SjmJobName { get; set; }
        public bool? IsTransferSuccessful { get; set; }        
        public int AnalysisId { get; set; }
        public Analysis Analysis { get; set; }
        public virtual ICollection<SasDataSet> SasDataSets { get; set; }

    }
}