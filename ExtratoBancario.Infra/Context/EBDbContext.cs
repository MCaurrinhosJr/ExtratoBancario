using ExtratoBancario.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace ExtratoBancario.Infra.Context
{
    public class EBDbContext : DbContext
    {
        public EBDbContext(DbContextOptions<EBDbContext> options) : base(options) { }

        public DbSet<Transaction> Transactions { get; set; }
    }
}
