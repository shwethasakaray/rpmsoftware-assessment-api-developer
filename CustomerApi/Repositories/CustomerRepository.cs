using System.Collections.Generic;
using System.Linq;
using CustomerApi.Models;

namespace CustomerApi.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly List<Customer> _customers = new List<Customer>();

        public IEnumerable<Customer> GetAll()
        {
            return _customers;
        }

        public Customer GetById(int id)
        {
            return _customers.FirstOrDefault(c => c.Id == id);
        }

        public void Add(Customer customer)
        {
            customer.Id = _customers.Count > 0 ? _customers.Max(c => c.Id) + 1 : 1;
            _customers.Add(customer);
        }

        public void Update(Customer customer)
        {
            var existingCustomer = GetById(customer.Id);
            if (existingCustomer != null)
            {
                existingCustomer.Name = customer.Name;
                existingCustomer.Email = customer.Email;
                existingCustomer.Phone = customer.Phone;
            }
        }

        public void Delete(int id)
        {
            var customer = GetById(id);
            if (customer != null)
            {
                _customers.Remove(customer);
            }
        }
    }
}
