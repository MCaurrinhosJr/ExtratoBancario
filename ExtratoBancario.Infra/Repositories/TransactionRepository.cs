using ExtratoBancario.Core.Interfaces.Repositories;
using ExtratoBancario.Core.Models;
using ExtratoBancario.Infra.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtratoBancario.Infra.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly EBDbContext _context;

        public TransactionRepository(EBDbContext context)
        {
            _context = context;
        }

        public IList<Transaction> GetTransactions(int days)
        {
            var dataRange = DateTime.Now.AddDays(-days);

            return _context.Transactions.Where(t => t.Data >= dataRange).OrderBy(t => t.Data).ToList();
        }
    }
}
