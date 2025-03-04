using Moq;
using Microsoft.AspNetCore.Mvc;
using CustomerApi.Controllers;
using CustomerApi.Models;
using CustomerApi.Services;

namespace CustomerApi.Tests
{
    [TestFixture]
    public class CustomersControllerTests
    {
        private Mock<ICustomerService> _mockService;
        private CustomersController _controller;

        [SetUp]
        public void Setup()
        {
            _mockService = new Mock<ICustomerService>();
            _controller = new CustomersController(_mockService.Object);
        }

        [Test]
        public void GetAll_ReturnsOkResult_WithListOfCustomers()
        {
            // Arrange
            var customers = new List<Customer>
            {
                new Customer { Id = 1, Name = "Shwetha Sakaray", Email = "Shwetha.sakaray1990@gmail.com", Phone = "1234567890" },
                new Customer { Id = 2, Name = "Shwetha Sakaray2", Email = "Shwetha.sakaray1991@gmail.com", Phone = "0987654321" }
            };
            _mockService.Setup(service => service.GetAllCustomers()).Returns(customers);

            // Act
            var result = _controller.GetAll();

            // Assert
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var returnCustomers = okResult.Value as List<Customer>;
            Assert.IsNotNull(returnCustomers);
            Assert.AreEqual(2, returnCustomers.Count);
        }

        [Test]
        public void GetById_ReturnsOkResult_WithCustomer()
        {
            // Arrange
            var customer = new Customer { Id = 1, Name = "Shwetha Sakaray", Email = "Shwetha.sakaray1990@gmail.com", Phone = "1234567890" };
            _mockService.Setup(service => service.GetCustomerById(1)).Returns(customer);

            // Act
            var result = _controller.GetById(1);

            // Assert
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var returnCustomer = okResult.Value as Customer;
            Assert.IsNotNull(returnCustomer);
            Assert.AreEqual(customer.Id, returnCustomer.Id);
        }

        [Test]
        public void GetById_ReturnsNotFoundResult_WhenCustomerNotFound()
        {
            // Arrange
            _mockService.Setup(service => service.GetCustomerById(1)).Returns((Customer)null);

            // Act
            var result = _controller.GetById(1);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }

        [Test]
        public void Create_ReturnsCreatedAtActionResult_WithCustomer()
        {
            // Arrange
            var customer = new Customer { Name = "Shwetha Sakaray", Email = "Shwetha.sakaray1990@gmail.com", Phone = "1234567890" };

            // Act
            var result = _controller.Create(customer);

            // Assert
            var createdAtActionResult = result.Result as CreatedAtActionResult;
            Assert.IsNotNull(createdAtActionResult);
            var returnCustomer = createdAtActionResult.Value as Customer;
            Assert.IsNotNull(returnCustomer);
            Assert.AreEqual(customer.Name, returnCustomer.Name);
        }

        [Test]
        public void Update_ReturnsNoContentResult_WhenCustomerExists()
        {
            // Arrange
            var customer = new Customer { Id = 1, Name = "Shwetha Sakaray", Email = "Shwetha.sakaray1990@gmail.com", Phone = "1234567890" };
            _mockService.Setup(service => service.GetCustomerById(1)).Returns(customer);

            // Act
            var result = _controller.Update(1, customer);

            // Assert
            Assert.IsInstanceOf<NoContentResult>(result);
        }

        [Test]
        public void Update_ReturnsNotFoundResult_WhenCustomerNotFound()
        {
            // Arrange
            var customer = new Customer { Id = 1, Name = "Shwetha Sakaray", Email = "Shwetha.sakaray1990@gmail.com", Phone = "1234567890" };
            _mockService.Setup(service => service.GetCustomerById(1)).Returns((Customer)null);

            // Act
            var result = _controller.Update(1, customer);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public void Delete_ReturnsNoContentResult_WhenCustomerExists()
        {
            // Arrange
            var customer = new Customer { Id = 1, Name = "Shwetha Sakaray", Email = "Shwetha.sakaray1990@gmail.com", Phone = "1231567890" };
            _mockService.Setup(service => service.GetCustomerById(1)).Returns(customer);

            // Act
            var result = _controller.Delete(1);

            // Assert
            Assert.IsInstanceOf<NoContentResult>(result);
        }

        [Test]
        public void Delete_ReturnsNotFoundResult_WhenCustomerNotFound()
        {
            // Arrange
            _mockService.Setup(service => service.GetCustomerById(1)).Returns((Customer)null);

            // Act
            var result = _controller.Delete(1);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }
    }
}