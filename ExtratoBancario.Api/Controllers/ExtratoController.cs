using ExtratoBancario.Core.Helper;
using ExtratoBancario.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ExtratoBancario.Api.Controllers
{
    public class ExtratoController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public ExtratoController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        /// <summary>
        /// Obtém o extrato bancário filtrado por um período específico em dias.
        /// </summary>
        /// <param name="days">O número de dias para filtrar as transações. Aceita valores como 5, 10, 15 e 20.</param>
        /// <returns>Uma resposta contendo o extrato filtrado.</returns>
        /// <response code="200">Retorna o extrato bancário filtrado.</response>
        /// <response code="400">Se o número de dias for inválido.</response>
        [HttpGet]
        [Route("api/extrato")]
        public IActionResult GetExtrato(int days)
        {
            if (days != 5 && days != 10 && days != 15 && days != 20)
            {
                return BadRequest("O número de dias deve ser 5, 10, 15 ou 20.");
            }
            var transactions = _transactionService.getTransactions(days);
            return Ok(transactions);
        }

        /// <summary>
        /// Gera um arquivo PDF com o extrato bancário filtrado por um período específico em dias.
        /// </summary>
        /// <param name="days">O número de dias para filtrar as transações.</param>
        /// <returns>Um arquivo PDF contendo o extrato bancário filtrado.</returns>
        /// <response code="200">Retorna o arquivo PDF com o extrato bancário.</response>
        /// <response code="400">Se o número de dias for inválido.</response>
        [HttpGet]
        [Route("api/extrato/pdf")]
        public IActionResult GetExtratoPDF(int days)
        {
            if (days != 5 && days != 10 && days != 15 && days != 20)
            {
                return BadRequest("O número de dias deve ser 5, 10, 15 ou 20.");
            }
            var transactions = _transactionService.getTransactions(days);

            return File(PdfCreater.GeneratePdf(transactions), "application/pdf", "Extrato.pdf");
        }
    }
}
