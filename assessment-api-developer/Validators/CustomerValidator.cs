using assessment_platform_developer.Models;
using System.Text.RegularExpressions;

namespace assessment_platform_developer.Validators
{
    public class CustomerValidator : ICustomerValidator
    {
        public bool Validate(Customer customer)
        {
            if (string.IsNullOrWhiteSpace(customer.Name) ||
                string.IsNullOrWhiteSpace(customer.Email) ||
                string.IsNullOrWhiteSpace(customer.Phone) ||
                string.IsNullOrWhiteSpace(customer.Address) ||
                string.IsNullOrWhiteSpace(customer.City) ||
                string.IsNullOrWhiteSpace(customer.State) ||
                string.IsNullOrWhiteSpace(customer.Zip) ||
                string.IsNullOrWhiteSpace(customer.Country))
            {
                return false;
            }

            if (customer.Country == "US" && !IsValidUSZipCode(customer.Zip))
            {
                return false;
            }

            return true;
        }

        private bool IsValidUSZipCode(string zipCode)
        {
            return Regex.IsMatch(zipCode, @"^\d{5}(-\d{4})?$");
        }
    }
}