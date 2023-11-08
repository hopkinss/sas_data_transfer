using Microsoft.EntityFrameworkCore;
using SasDataTransfer.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SasDataTransfer.Domain
{
    public class SasDataTransferContext : DbContext
    {
        public SasDataTransferContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Protocol> Protocol { get; set; }
        public DbSet<Analysis> Analysis { get; set; }
        public DbSet<Transfer> Transfer { get; set; }
        public DbSet<SasDataSet> SasDataSet { get; set; }
        public DbSet<Variable> Variable { get; set; }

    }
}
