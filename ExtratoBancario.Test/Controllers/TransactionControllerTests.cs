using NUnit.Framework;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using ExtratoBancario.Core.Interfaces.Services;
using ExtratoBancario.Api.Controllers;
using ExtratoBancario.Core.Models;
using ExtratoBancario.Core.Helper;


namespace ExtratoBancario.Test.Controllers
{
    public class TransactionControllerTests
    {
        private Mock<ITransactionService> _transactionServiceMock;
        private ExtratoController _controller;

        [SetUp]
        public void Setup()
        {
            _transactionServiceMock = new Mock<ITransactionService>();
            _controller = new ExtratoController(_transactionServiceMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            if (_controller is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }

        [Test]
        public void GetExtrato_DeveRetornarExtratoFiltrado()
        {
            // Arrange
            var dias = 10;
            var transactions = new List<Transaction>
            {
                new Transaction { Data = DateTime.Now.AddDays(-5), TipoTransacao = "Crédito", Valor = 100 },
                new Transaction { Data = DateTime.Now.AddDays(-15), TipoTransacao = "Débito", Valor = 50 }
            };
            _transactionServiceMock.Setup(service => service.getTransactions(dias))
                                 .Returns(transactions.Where(t => t.Data >= DateTime.Now.AddDays(-dias)).ToList());

            // Act
            var resultado = _controller.GetExtrato(dias) as OkObjectResult;
            var resultadoTransacoes = resultado?.Value as IEnumerable<Transaction>;

            // Assert
            Assert.IsNotNull(resultado);
            Assert.AreEqual(200, resultado.StatusCode);
            Assert.IsNotNull(resultadoTransacoes);
            Assert.AreEqual(1, resultadoTransacoes.Count());
            Assert.AreEqual("Crédito", resultadoTransacoes.First().TipoTransacao);
        }

        [Test]
        public void GetExtrato_DeveRetornarListaVaziaQuandoNenhumaTransacao()
        {
            // Arrange
            var dias = 10;
            _transactionServiceMock.Setup(service => service.getTransactions(dias))
                                   .Returns(new List<Transaction>());

            // Act
            var resultado = _controller.GetExtrato(dias) as OkObjectResult;
            var resultadoTransacoes = resultado?.Value as IEnumerable<Transaction>;

            // Assert
            Assert.IsNotNull(resultado);
            Assert.AreEqual(200, resultado.StatusCode);
            Assert.IsNotNull(resultadoTransacoes);
            Assert.IsEmpty(resultadoTransacoes);
        }

        [Test]
        public void GetExtratoPdf_DeveGerarPdf()
        {
            // Arrange
            var dias = 10;
            var transactions = new List<Transaction>
            {
                new Transaction { Data = DateTime.Now.AddDays(-5), TipoTransacao = "Crédito", Valor = 100 }
            };
            _transactionServiceMock.Setup(service => service.getTransactions(dias))
                                 .Returns(transactions);

            // Act
            var resultado = _controller.GetExtratoPDF(dias) as FileContentResult;

            // Assert
            Assert.IsNotNull(resultado);
            Assert.AreEqual("application/pdf", resultado.ContentType);
            Assert.IsTrue(resultado.FileContents.Length > 0);
        }

        [Test]
        public void GeneratePdf_DeveGerarPdfMesmoQuandoListaVazia()
        {
            // Arrange
            var transactions = new List<Transaction>();

            // Act
            var resultadoPdf = PdfCreater.GeneratePdf(transactions);

            // Assert
            Assert.IsNotNull(resultadoPdf);
            Assert.IsTrue(resultadoPdf.Length > 0);
        }
    }
}