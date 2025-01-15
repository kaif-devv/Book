using Books.Models;
using Books.Services;
using Microsoft.AspNetCore.Mvc;

namespace Books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellersController : ControllerBase
    {
        private readonly SellerService _sellerService;

        public SellersController(SellerService sellerService)
        {
            _sellerService = sellerService;
        }

        // Get all sellers
        [HttpGet]
        [Route("GetSellers")]
        public List<Seller> GetSellers()
        {
            return _sellerService.GetAllSellers();
        }

        // Get seller by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Seller>> GetSeller(int id)
        {
            var seller = await _sellerService.GetSellerById(id);
            if (seller == null)
            {
                return NotFound($"Seller with ID {id} not found");
            }
            return seller;
        }

        // Update the seller by ID
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSeller(int id, SellerUpdateDto seller)
        {
            if (seller == null)
                return BadRequest("Seller data is required");

            var result = await _sellerService.UpdateSeller(id, seller);
            if (!result)
                return NotFound($"Seller with ID {id} not found");

            return Ok("Seller Updated Successfully");
        }

        // Add a new seller
        [HttpPost]
        [Route("AddSeller")]
        public ActionResult<string> PostSeller(Seller seller)
        {
            return _sellerService.AddSeller(seller);
        }

        // Delete seller by ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSeller(int id)
        {
            var result = await _sellerService.DeleteSeller(id);
            if (!result)
                return NotFound($"Seller with ID {id} not found");

            return Ok("Seller Deleted Successfully");
        }
    }
}