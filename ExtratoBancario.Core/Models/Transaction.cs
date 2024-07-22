using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtratoBancario.Core.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public string TipoTransacao { get; set; }
        public decimal Valor { get; set; }
    }
}
