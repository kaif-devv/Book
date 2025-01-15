using Books.Models;
using Microsoft.EntityFrameworkCore;

namespace Books.Services
{
    public interface ISellerService
    {
        List<Seller> GetAllSellers();
        Task<Seller> GetSellerById(int id);
        Task<bool> UpdateSeller(int id, SellerUpdateDto seller);
        string AddSeller(Seller seller);
        Task<bool> DeleteSeller(int id);
    }
    public class SellerService : ISellerService
    {
        private readonly BooksContext _context;

        public SellerService(BooksContext context)
        {
            _context = context;
        }

        public List<Seller> GetAllSellers()
        {
            return _context.Sellers.ToList();
        }

        public async Task<Seller> GetSellerById(int id)
        {
            return await _context.Sellers.FindAsync(id);
        }

        public async Task<bool> UpdateSeller(int id, SellerUpdateDto seller)
        {
            var existingSeller = await _context.Sellers.FindAsync(id);
            if (existingSeller == null) return false;

            _context.Entry(existingSeller).CurrentValues.SetValues(seller);

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SellerExists(id))
                    return false;
                throw;
            }
        }

        public string AddSeller(Seller seller)
        {
            _context.Sellers.Add(seller);
            _context.SaveChanges();
            if (seller.Id == 0)
                return "Failed to add seller";
            return "Seller Added Successfully";
        }

        public async Task<bool> DeleteSeller(int id)
        {
            var seller = await _context.Sellers.FindAsync(id);
            if (seller == null)
                return false;

            _context.Sellers.Remove(seller);
            await _context.SaveChangesAsync();
            return true;
        }

        public bool SellerExists(int id)
        {
            return _context.Sellers.Any(e => e.Id == id);
        }
    }
}
