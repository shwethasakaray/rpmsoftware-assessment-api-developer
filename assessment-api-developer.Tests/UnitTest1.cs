using Microsoft.VisualStudio.TestTools.UnitTesting;
using assessment_platform_developer.Models;
using assessment_platform_developer.Validators;

namespace assessment_platform_developer.Tests
{
    [TestClass]
    public class CustomerValidatorTests
    {
        private ICustomerValidator _customerValidator;

        [TestInitialize]
        public void Setup()
        {
            _customerValidator = new CustomerValidator();
        }

        [TestMethod]
        public void Validate_ValidCustomer_ReturnsTrue()
        {
            // Arrange
            var customer = new Customer
            {
                Name = "Shwetha Sakaray",
                Address = "123 Main St",
                City = "Calgary",
                State = "CA",
                Zip = "12345",
                Country = "US",
                Email = "Shwetha.sakaray1990@gmail.com",
                Phone = "123-456-7890"
            };

            // Act
            var result = _customerValidator.Validate(customer);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Validate_InvalidUSZipCode_ReturnsFalse()
        {
            // Arrange
            var customer = new Customer
            {
                Name = "Shwetha Sakaray",
                Address = "123 Main St",
                City = "Calgary",
                State = "CA",
                Zip = "1234",
                Country = "US",
                Email = "Shwetha.sakaray1990@gmail.com",
                Phone = "123-456-7890"
            };

            // Act
            var result = _customerValidator.Validate(customer);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Validate_MissingRequiredFields_ReturnsFalse()
        {
            // Arrange
            var customer = new Customer
            {
                Name = "",
                Address = "",
                City = "",
                State = "",
                Zip = "",
                Country = "",
                Email = "",
                Phone = ""
            };

            // Act
            var result = _customerValidator.Validate(customer);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Validate_ValidCanadianCustomer_ReturnsTrue()
        {
            // Arrange
            var customer = new Customer
            {
                Name = "Shwetha Sakaray",
                Address = "456 Maple St",
                City = "Toronto",
                State = "ON",
                Zip = "M5H 2N2",
                Country = "CA",
                Email = "Shwetha.sakaray1990@gmail.com",
                Phone = "987-654-3210"
            };

            // Act
            var result = _customerValidator.Validate(customer);

            // Assert
            Assert.IsTrue(result);
        }
    }
}