using CustomerInquiry.Controllers;
using CustomerInquiry.Domain.DTOs;
using CustomerInquiry.Provider;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CustomerInquiry.Test
{
    public class CustomerControllerTest
    {
        readonly CustomerController _controller;
        private readonly Mock<ICustomerInquiryMockRepository> _mockRepo;


        public CustomerControllerTest()
        {
            _mockRepo = new Mock<ICustomerInquiryMockRepository>();
            ICustomerProvider provider = new CustomerProvider(_mockRepo.Object);
            _controller = new CustomerController(provider);
        }

        [Fact]
        public async Task Get_WhenCalled_ReturnsOkResult()
        {
            _mockRepo.Setup(m => m.GetCustomers())
                .Returns(Task.FromResult(MockData.Current.Customers.AsEnumerable()));
            // Act
            var okResult = await _controller.Get();

            // Assert
            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public async Task Get_WhenCalled_ReturnsAllItems()
        {
            _mockRepo.Setup(m => m.GetCustomers())
                .Returns(Task.FromResult(MockData.Current.Customers.AsEnumerable()));
            // Act
            var okResult = await _controller.Get() as OkObjectResult;

            // Assert
            var items = Assert.IsType<List<CustomerDto>>(okResult?.Value);
            Assert.Equal(3, items.Count);
        }

        [Fact]
        public async Task GetById_UnknownCustomerIdPassed_ReturnsNotFoundResult()
        {
            //Arrange
            _mockRepo.Setup(x => x.GetCustomer(It.Is<int>(y => y == 4), false))
            .Returns(Task.FromResult(MockData.Current.Customers
            .FirstOrDefault(p => p.CustomerId.Equals(4))));

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
            _mockRepo.Setup(x => x.GetCustomer(It.Is<int>(y => y == testCustomerId), false))
                .Returns(Task.FromResult(MockData.Current.Customers
                    .FirstOrDefault(p => p.CustomerId.Equals(testCustomerId))));

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
            _mockRepo.Setup(x => x.GetCustomer(It.Is<int>(y => y == testCustomerId), false))
                .Returns(Task.FromResult(MockData.Current.Customers
                    .FirstOrDefault(p => p.CustomerId.Equals(testCustomerId))));

            // Act
            var okResult = await _controller.Get(testCustomerId) as OkObjectResult;

            // Assert
            Assert.IsType<CustomerDto>(okResult?.Value);
            Assert.Equal(testCustomerId, ((CustomerDto) okResult.Value).CustomerId);
        }

        [Fact]
        public void Add_MissingObjectNamePassed_ReturnsBadRequest()
        {
            // Arrange
            var nameMissingItem = new CustomerDto
            {
                ContactEmail = "Jack@domain.com",
                MobileNo = "1020304050",
            };


            _mockRepo.Setup(x => x.AddCustomer(It.Is<CustomerDto>(y => y == nameMissingItem)))
                .Callback<CustomerDto>(s => MockData.Current.Customers.Add(s));

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
            // Act
            var badResponse = _controller.Post(null);

            // Assert
            Assert.IsType<BadRequestResult>(badResponse);
        }

        [Fact]
        public void Add_InvalidObjectMobileLengthPassed_ReturnsBadRequest()
        {
            // Arrange
            var invalidMobileItem = new CustomerBaseDto()
            {
                CustomerName = "Jack",
                ContactEmail = "Jack@domain.com",
                MobileNo = "12345678910"
            };

            _mockRepo.Setup(x => x.AddCustomer(It.Is<CustomerDto>(y => y == invalidMobileItem)))
                .Callback<CustomerDto>(s => MockData.Current.Customers.Add(s));
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
            var invalidMobileItem = new CustomerBaseDto()
            {
                CustomerName = "Jack",
                ContactEmail = "Jack@domain.com",
                MobileNo = "123456789m"
            };

            _mockRepo.Setup(x => x.AddCustomer(It.Is<CustomerDto>(y => y == invalidMobileItem)))
                .Callback<CustomerDto>(s => MockData.Current.Customers.Add(s));
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
            var testItem = new CustomerDto
            {
                CustomerName = "Jack",
                ContactEmail = "Jack@domain.com",
                MobileNo = "1020304050"
            };

            _mockRepo.Setup(x => x.AddCustomer(It.Is<CustomerDto>(y => y == testItem)))
                .Callback<CustomerDto>(s => MockData.Current.Customers.Add(s));

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
            var testItem = new CustomerBaseDto()
            {
                CustomerName = "Jack",
                ContactEmail = "Jack@domain.com",
                MobileNo = "1020304050"
            };

            // Act
            _controller.ValidateViewModel(testItem);
            var createdResponse = _controller.Post(testItem) as CreatedAtRouteResult;
            var item = createdResponse?.Value as CustomerDto;

            // Assert
            Assert.IsType<CustomerDto>(item);
            Assert.Equal("Jack", item.CustomerName);
        }
    }
}
