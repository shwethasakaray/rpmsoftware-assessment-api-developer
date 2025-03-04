using Microsoft.AspNetCore.Mvc;
using CustomerApi.Models;
using CustomerApi.Services;
using CustomerApi.Validators;

namespace CustomerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Customer>> GetAll()
        {
            return Ok(_customerService.GetAllCustomers());
        }

        [HttpGet("{id}")]
        public ActionResult<Customer> GetById(int id)
        {
            var customer = _customerService.GetCustomerById(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpPost]
        public ActionResult<Customer> Create(Customer customer)
        {
            if(!CustomerValidator.Validate(customer))
            {
                return BadRequest("Invalid Customer data");
            }
            _customerService.AddCustomer(customer);
            return CreatedAtAction(nameof(GetById), new { id = customer.Id }, customer);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Customer customer)
        {
            if (id != customer.Id || !CustomerValidator.Validate(customer))
            {
                return BadRequest("Invalid customer data");
            }

            var existingCustomer = _customerService.GetCustomerById(id);
            if (existingCustomer == null)
            {
                return NotFound();
            }

            _customerService.UpdateCustomer(customer);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var customer = _customerService.GetCustomerById(id);
            if (customer == null)
            {
                return NotFound();
            }

            _customerService.DeleteCustomer(id);
            return NoContent();
        }
    }
}