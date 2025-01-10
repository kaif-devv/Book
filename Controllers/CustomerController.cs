using Books.Models;
using Books.Services;
using Microsoft.AspNetCore.Mvc;

namespace Books.Controllers
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
        [Route("GetCustomers")]
        public List<Customer> GetCustomers()
        {
            return _customerService.GetAllCustomers();
        }

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

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, CustomerUpdateDto customer)
        {
            if (customer == null)
                return BadRequest("Customer data is required");

            var result = await _customerService.UpdateCustomer(id, customer);
            if (!result)
                return NotFound($"Customer with ID {id} not found");

            return NoContent();
        }

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

            return NoContent();
        }
    }
}