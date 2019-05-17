using CustomerInquiry.Controllers;
using CustomerInquiry.Domain.DTOs;
using CustomerInquiry.Provider;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerInquiry.DataAccess;
using Xunit;

namespace CustomerInquiry.Test
{
    public class TransactionControllerTest
    {
        readonly TransactionController _controller;

        public TransactionControllerTest()
        {
            IGenericEfRepository<TransactionDto> repo = new TransactionMockRepository<TransactionDto>();
            ITransactionProvider provider = new TransactionProvider(repo);
            _controller = new TransactionController(provider);
        }

        [Fact]
        public async Task Get_WhenCalled_ReturnsOkResult()
        {
            // Arrange
            const int testCustomerId = 2;

            // Act
            var okResult = await _controller.Get(testCustomerId);

            // Assert
            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public async Task Get_WhenCalled_ReturnsAllItems()
        {
            // Arrange
            const int testCustomerId = 2;

            // Act
            var okResult = await _controller.Get(testCustomerId) as OkObjectResult;

            // Assert
            var items = Assert.IsType<List<TransactionDto>>(okResult?.Value);
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
            const int testCustomerId = 2;
            const int testTransactionId = 1;

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
            Assert.IsType<TransactionDto>(okResult?.Value);
            Assert.Equal(testCustomerId, ((TransactionDto) okResult.Value).CustomerId);
            Assert.Equal(testTransactionId, ((TransactionDto) okResult.Value).TransactionId);

        }

        [Fact]
        public void Add_TransactionWithNewlyInvoice_ReturnsOKResult()
        {
            // Arrange
            var theItem = new TransactionBaseDto
            {
                Amount = 2,
                CurrencyCode = "USD",
                Invoice = "InvoiceTest",
                Status = Domain.Enums.StatusType.Success
            };

            // Act

            //See how the ValidateViewModel extension method in the Helper class is useful here
            _controller.ValidateViewModel(theItem);
            //I have used the above useful extension method to simulate validation instead of adding customly like below
            //_controller.ModelState.AddModelError("CustomerName", "Required");

            var theResponse = _controller.Post(1, theItem);

            // Assert
            Assert.IsType<CreatedAtRouteResult>(theResponse);
        }
    }
}
