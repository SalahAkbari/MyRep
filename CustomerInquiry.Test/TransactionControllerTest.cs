using CustomerInquiry.Controllers;
using CustomerInquiry.Domain.DTOs;
using CustomerInquiry.Provider;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CustomerInquiry.Test
{
    public class TransactionControllerTest
    {
        TransactionController _controller;
        ITransactionProvider _provider;
        ICustomerInquiryMockRepository _repo;

        public TransactionControllerTest()
        {
            _repo = new CustomerInquiryMockRepository();
            _provider = new TransactionProvider(_repo);
            _controller = new TransactionController(_provider);
        }

        [Fact]
        public async Task Get_WhenCalled_ReturnsOkResult()
        {
            // Arrange
            var testCustomerId = 2;

            // Act
            var okResult = await _controller.Get(testCustomerId);

            // Assert
            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public async Task Get_WhenCalled_ReturnsAllItems()
        {
            // Arrange
            var testCustomerId = 2;

            // Act
            var okResult = await _controller.Get(testCustomerId) as OkObjectResult;

            // Assert
            var items = Assert.IsType<List<TransactionDTO>>(okResult.Value);
            Assert.Equal(2, items.Count);
        }

        [Fact]
        public async Task GetById_UnknownCustomerIdAndTransactionIdPassed_ReturnsNotFoundResult()
        {
            // Act
            var notFoundResult = await _controller.Get(4,8);

            // Assert
            Assert.IsType<NotFoundResult>(notFoundResult);
        }

        [Fact]
        public async Task GetById_ExistingCustomerIdAndTransactionIdPassed_ReturnsOkResult()
        {
            // Arrange
            var testCustomerId = 2;
            var testTransactionId = 1;

            // Act
            var okResult = await _controller.Get(testCustomerId, testTransactionId);

            // Assert
            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public async Task GetById_ExistingCustomerIdAndTransactionIdPassed_ReturnsRightItem()
        {
            // Arrange
            var testCustomerId = 2;
            var testTransactionId = 1;

            // Act
            var okResult = await _controller.Get(testCustomerId, testTransactionId) as OkObjectResult;

            // Assert
            Assert.IsType<TransactionDTO>(okResult.Value);
            Assert.Equal(testCustomerId, (okResult.Value as TransactionDTO).CustomerId);
            Assert.Equal(testTransactionId, (okResult.Value as TransactionDTO).TransactionID);

        }
    }
}
