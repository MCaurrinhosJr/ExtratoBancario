using ExtratoBancario.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtratoBancario.Core.Interfaces.Repositories
{
    public interface ITransactionRepository
    {
        IList<Transaction> GetTransactions(int days);
    }
}
