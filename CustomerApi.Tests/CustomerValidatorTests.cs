using CustomerApi.Models;
using CustomerApi.Validators;

namespace CustomerApi.Tests
{
    [TestFixture]
    public class CustomerValidatorTests
    {
        [Test]
        public void Validate_ValidCustomer_ReturnsTrue()
        {
            // Arrange
            var customer = new Customer
            {
                Name = "John Doe",
                Address = "123 Main St",
                City = "Anytown",
                State = "CA",
                Zip = "12345",
                Country = "US",
                Email = "john.doe@example.com",
                Phone = "123-456-7890"
            };

            // Act
            var result = CustomerValidator.Validate(customer);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void Validate_InvalidUSZipCode_ReturnsFalse()
        {
            // Arrange
            var customer = new Customer
            {
                Name = "John Doe",
                Address = "123 Main St",
                City = "Anytown",
                State = "CA",
                Zip = "1234",
                Country = "US",
                Email = "john.doe@example.com",
                Phone = "123-456-7890"
            };

            // Act
            var result = CustomerValidator.Validate(customer);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
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
            var result = CustomerValidator.Validate(customer);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void Validate_ValidCanadianCustomer_ReturnsTrue()
        {
            // Arrange
            var customer = new Customer
            {
                Name = "Jane Doe",
                Address = "456 Maple St",
                City = "Toronto",
                State = "ON",
                Zip = "M5H 2N2",
                Country = "CA",
                Email = "jane.doe@example.com",
                Phone = "987-654-3210"
            };

            // Act
            var result = CustomerValidator.Validate(customer);

            // Assert
            Assert.IsTrue(result);
        }
    }
}
