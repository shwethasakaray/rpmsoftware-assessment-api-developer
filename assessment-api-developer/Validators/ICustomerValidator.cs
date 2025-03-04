using assessment_platform_developer.Models;

namespace assessment_platform_developer.Validators
{
    public interface ICustomerValidator
    {
        bool Validate(Customer customer);
    }
}
