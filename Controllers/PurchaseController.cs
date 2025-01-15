using Books.Models;
using Books.Services;
using Microsoft.AspNetCore.Mvc;

namespace Books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchasesController : ControllerBase
    {
        private readonly PurchaseService _purchaseService;

        public PurchasesController(PurchaseService purchaseService)
        {
            _purchaseService = purchaseService;
        }

        // Get all purchases
        [HttpGet]
        [Route("GetPurchases")]
        public List<Purchase> GetPurchases()
        {
            return _purchaseService.GetAllPurchases();
        }

        // Get purchase by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Purchase>> GetPurchase(int id)
        {
            var purchase = await _purchaseService.GetPurchaseById(id);
            if (purchase == null)
            {
                return NotFound($"Purchase with ID {id} not found");
            }
            return purchase;
        }

        // Update the purchase by ID
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPurchase(int id, PurchaseUpdateDto purchase)
        {
            if (purchase == null)
                return BadRequest("Purchase data is required");

            var result = await _purchaseService.UpdatePurchase(id, purchase);
            if (!result)
                return NotFound($"Purchase with ID {id} not found");

            return Ok("Purchase Updated Successfully");
        }

        // Add a new purchase
        [HttpPost]
        [Route("AddPurchase")]
        public ActionResult<string> PostPurchase(Purchase purchase)
        {
            return _purchaseService.AddPurchase(purchase);
        }

        // Delete purchase by ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePurchase(int id)
        {
            var result = await _purchaseService.DeletePurchase(id);
            if (!result)
                return NotFound($"Purchase with ID {id} not found");

            return Ok("Purchase Deleted Successfully");
        }
    }
}