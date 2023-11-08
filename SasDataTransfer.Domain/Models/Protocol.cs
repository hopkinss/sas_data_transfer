using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SasDataTransfer.Domain.Models
{
    [Index(nameof(ProtocolName), IsUnique = true)]
    public class Protocol
    {
        public int Id { get; set; }
        [Required]
        public string ProtocolName { get; set; }
 
        public virtual ICollection<Analysis> Analyses { get; set; }

    }
}