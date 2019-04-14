using CustomerInquiry.Controllers;
using CustomerInquiry.Domain.DTOs;
using CustomerInquiry.Provider;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CustomerInquiry.Test
{
    public class CustomerControllerTest
    {
        CustomerController _controller;
        ICustomerProvider _provider;
        ICustomerInquiryMockRepository _repo;

        public CustomerControllerTest()
        {
            _repo = new CustomerInquiryMockRepository();
            _provider = new CustomerProvider(_repo);
            _controller = new CustomerController(_provider);
        }

        [Fact]
        public async Task Get_WhenCalled_ReturnsOkResult()
        {
            // Act
            var okResult = await _controller.Get();

            // Assert
            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public async Task Get_WhenCalled_ReturnsAllItems()
        {
            // Act
            var okResult = await _controller.Get() as OkObjectResult;

            // Assert
            var items = Assert.IsType<List<CustomerDTO>>(okResult.Value);
            Assert.Equal(3, items.Count);
        }

        [Fact]
        public async Task GetById_UnknownCustomerIdPassed_ReturnsNotFoundResult()
        {
            // Act
            var notFoundResult = await _controller.Get(4);

            // Assert
            Assert.IsType<NotFoundResult>(notFoundResult);
        }

        [Fact]
        public async Task GetById_ExistingCustomerIdPassed_ReturnsOkResult()
        {
            // Arrange
            var testCustomerId = 2;

            // Act
            var okResult = await _controller.Get(testCustomerId);

            // Assert
            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public async Task GetById_ExistingCustomerIdPassed_ReturnsRightItem()
        {
            // Arrange
            var testCustomerId = 2;

            // Act
            var okResult = await _controller.Get(testCustomerId) as OkObjectResult;

            // Assert
            Assert.IsType<CustomerDTO>(okResult.Value);
            Assert.Equal(testCustomerId, (okResult.Value as CustomerDTO).CustomerID);
        }

        [Fact]
        public void Add_MissingObjectNamePassed_ReturnsBadRequest()
        {
            // Arrange
            var nameMissingItem = new CustomerBaseDTO()
            {
                ContactEmail = "Jack@domain.com",
                MobileNo = "1020304050"
            };

            // Act

            //See how the ValidateViewModel extension method in the Helper class is useful here
            _controller.ValidateViewModel(nameMissingItem);
            //I have used the above useful extension method to simulate validation instead of adding customly like below
            //_controller.ModelState.AddModelError("CustomerName", "Required");
            
            var badResponse = _controller.Post(nameMissingItem);

            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }

        [Fact]
        public void Add_NullObjectPassed_ReturnsBadRequest()
        {
            // Arrange
            CustomerBaseDTO nullItem = null;

            // Act
            var badResponse = _controller.Post(nullItem);

            // Assert
            Assert.IsType<BadRequestResult>(badResponse);
        }

        [Fact]
        public void Add_InvalidObjectMobileLengthPassed_ReturnsBadRequest()
        {
            // Arrange
            var invalidMobileItem = new CustomerBaseDTO()
            {
                CustomerName = "Jack",
                ContactEmail = "Jack@domain.com",
                MobileNo = "12345678910"
            };

            // Act

            _controller.ValidateViewModel(invalidMobileItem);
            var badResponse = _controller.Post(invalidMobileItem);

            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }

        [Fact]
        public void Add_InvalidObjectMobileCharacterPassed_ReturnsBadRequest()
        {
            // Arrange
            var invalidMobileItem = new CustomerBaseDTO()
            {
                CustomerName = "Jack",
                ContactEmail = "Jack@domain.com",
                MobileNo = "123456789m"
            };

            // Act

            _controller.ValidateViewModel(invalidMobileItem);
            var badResponse = _controller.Post(invalidMobileItem);

            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }

        [Fact]
        public void Add_ValidObjectPassed_ReturnsCreatedResponse()
        {
            // Arrange
            var testItem = new CustomerBaseDTO()
            {
                CustomerName = "Jack",
                ContactEmail = "Jack@domain.com",
                MobileNo = "1020304050"
            };

            // Act
            _controller.ValidateViewModel(testItem);
            var createdResponse = _controller.Post(testItem);

            // Assert
            Assert.IsType<CreatedAtRouteResult>(createdResponse);
        }

        [Fact]
        public void Add_ValidObjectPassed_ReturnedResponseHasCreatedItem()
        {
            // Arrange
            var testItem = new CustomerBaseDTO()
            {
                CustomerName = "Jack",
                ContactEmail = "Jack@domain.com",
                MobileNo = "1020304050"
            };

            // Act
            _controller.ValidateViewModel(testItem);
            var createdResponse = _controller.Post(testItem) as CreatedAtRouteResult;
            var item = createdResponse.Value as CustomerDTO;

            // Assert
            Assert.IsType<CustomerDTO>(item);
            Assert.Equal("Jack", item.CustomerName);
        }
    }
}
