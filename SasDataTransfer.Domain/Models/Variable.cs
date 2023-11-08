using System.Data;

namespace SasDataTransfer.Domain.Models
{
    public class Variable
    {
        public int Id { get; set; }
        public string VariableName { get; set; }
        
        public int DatasetId { get; set; }
        public SasDataSet DataSet { get; set; }

    }
}