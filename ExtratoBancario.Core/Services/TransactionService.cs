using ExtratoBancario.Core.Interfaces.Repositories;
using ExtratoBancario.Core.Interfaces.Services;
using ExtratoBancario.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtratoBancario.Core.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }
        public IList<Transaction> getTransactions(int days)
        {
            return _transactionRepository.GetTransactions(days);
        }
    }
}
