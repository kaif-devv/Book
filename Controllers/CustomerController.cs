using Books.Models;
using Books.Services;
using Microsoft.AspNetCore.Mvc;

namespace Books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly CustomerService _customerService;

        public CustomersController(CustomerService customerService)
        {
            _customerService = customerService;
        }

        // Get all customers
        [HttpGet]
        [Route("GetCustomers")]
        public List<Customer> GetCustomers()
        {
            return _customerService.GetAllCustomers();
        }

        // Get customer by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var customer = await _customerService.GetCustomerById(id);
            if (customer == null)
            {
                return NotFound($"Customer with ID {id} not found");
            }
            return customer;
        }

        // Update the customer by ID
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, CustomerUpdateDto customer)
        {
            if (customer == null)
                return BadRequest("Customer data is required");

            var result = await _customerService.UpdateCustomer(id, customer);
            if (!result)
                return NotFound($"Customer with ID {id} not found");

            return Ok("Customer Updated Successfully");
        }

        // Add a new customer
        [HttpPost]
        [Route("AddCustomer")]
        public ActionResult<string> PostCustomer(Customer customer)
        {
            return _customerService.AddCustomer(customer);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var result = await _customerService.DeleteCustomer(id);
            if (!result)
                return NotFound($"Customer with ID {id} not found");

            return Ok("Customer Deleted Successfully");
        }
    }
}