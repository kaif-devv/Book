using Books.Models;
using Microsoft.EntityFrameworkCore;

namespace Books.Services
{
    public interface ICustomerService
    {
        List<Customer> GetAllCustomers();
        Task<Customer> GetCustomerById(int id);
        Task<bool> UpdateCustomer(int id, CustomerUpdateDto customer);
        string AddCustomer(Customer customer);
        Task<bool> DeleteCustomer(int id);
        bool CustomerExists(int id);
    }
    public class CustomerService : ICustomerService
    {
        private readonly BooksContext _context;

        public CustomerService(BooksContext context)
        {
            _context = context;
        }

        public List<Customer> GetAllCustomers()
        {
            return _context.Customers.ToList();
        }

        public async Task<Customer> GetCustomerById(int id)
        {
            return await _context.Customers.FindAsync(id);
        }

        public async Task<bool> UpdateCustomer(int id, CustomerUpdateDto customer)
        {
            var existingCustomer = await _context.Customers.FindAsync(id);
            if (existingCustomer == null) return false;

            // Update properties from DTO to existing entity
            _context.Entry(existingCustomer).CurrentValues.SetValues(customer);


            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
                    return false;
                throw;
            }
        }

        public string AddCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
            return "Customer Added Successfully";
        }

        public async Task<bool> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
                return false;

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return true;
        }

        public bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }
    }
}
