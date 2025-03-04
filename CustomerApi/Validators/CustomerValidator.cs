using CustomerApi.Models;
using CustomerApi.Constants;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace CustomerApi.Validators
{
    public static class CustomerValidator
    {
        public static bool Validate(Customer customer)
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

            if (customer.Country == CustomerConstants.US_country && !IsValidUSZipCode(customer.Zip))
            {
                return false;
            }

            return true;
        }

        private static bool IsValidUSZipCode(string zipCode)
        {
            return Regex.IsMatch(zipCode, @"^\d{5}(-\d{4})?$");
        }
    }
}
