using ExtratoBancario.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtratoBancario.Core.Interfaces.Services
{
    public interface ITransactionService
    {
        IList<Transaction> getTransactions(int days);
    }
}
