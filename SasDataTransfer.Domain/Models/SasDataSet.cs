using System.ComponentModel.DataAnnotations.Schema;

namespace SasDataTransfer.Domain.Models
{
    public class SasDataSet
    {
        public int Id { get; set; }
        public string DataSetName { get; set; }        
        public int TransferId { get; set; }
        public Transfer Transfer { get; set; }
        public ICollection<Variable> Variables { get; set; }
    }
}