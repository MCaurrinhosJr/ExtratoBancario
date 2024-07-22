using ExtratoBancario.Core.Interfaces.Repositories;
using ExtratoBancario.Core.Interfaces.Services;
using ExtratoBancario.Core.Models;
using ExtratoBancario.Core.Services;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExtratoBancario.Test.Services
{
    public class TransactionServiceTests
    {
        private Mock<ITransactionRepository> _transactionRepositoryMock;
        private ITransactionService _transactionService;

        [SetUp]
        public void Setup()
        {
            _transactionRepositoryMock = new Mock<ITransactionRepository>();
            _transactionService = new TransactionService(_transactionRepositoryMock.Object);
        }

        [Test]
        public void GetTransacoesFiltradas_DeveRetornarTransacoesFiltradas()
        {
            // Arrange
            var dias = 10;
            var transactions = new List<Transaction>
            {
                new Transaction { Data = DateTime.Now.AddDays(-5), TipoTransacao = "Crédito", Valor = 100 },
                new Transaction { Data = DateTime.Now.AddDays(-15), TipoTransacao = "Débito", Valor = 50 }
            };
            _transactionRepositoryMock.Setup(repo => repo.GetTransactions(dias))
                                    .Returns(transactions.Where(t => t.Data >= DateTime.Now.AddDays(-dias)).ToList());

            // Act
            var resultado = _transactionService.getTransactions(dias).ToList();

            // Assert
            Assert.AreEqual(1, resultado.Count);
            Assert.AreEqual("Crédito", resultado.First().TipoTransacao);
        }

        [Test]
        public void GetTransacoesFiltradas_DeveRetornarListaVaziaQuandoNenhumaTransacao()
        {
            // Arrange
            var dias = 10;
            _transactionRepositoryMock.Setup(repo => repo.GetTransactions(dias))
                                      .Returns(new List<Transaction>());

            // Act
            var resultado = _transactionService.getTransactions(dias).ToList();

            // Assert
            Assert.IsEmpty(resultado);
        }

        [Test]
        public void GetTransacoesFiltradas_DeveRetornarListaVaziaQuandoDiasMenorOuIgualAZero()
        {
            // Arrange
            var dias = 0;
            _transactionRepositoryMock.Setup(repo => repo.GetTransactions(dias))
                                      .Returns(new List<Transaction>());

            // Act
            var resultado = _transactionService.getTransactions(dias).ToList();

            // Assert
            Assert.IsEmpty(resultado);
        }
    }
}