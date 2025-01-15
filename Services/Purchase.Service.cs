using Books.Models;
using Microsoft.EntityFrameworkCore;

namespace Books.Services
{
    public interface IPurchaseService
    {
        List<Purchase> GetAllPurchases();
        Task<Purchase> GetPurchaseById(int id);
        Task<bool> UpdatePurchase(int id, PurchaseUpdateDto purchase);
        string AddPurchase(Purchase purchase);
        Task<bool> DeletePurchase(int id);
        bool PurchaseExists(int id);
    }
    public class PurchaseService : IPurchaseService
    {
        private readonly BooksContext _context;

        public PurchaseService(BooksContext context)
        {
            _context = context;
        }

// Get all purchases
        public List<Purchase> GetAllPurchases()
        {
            return _context.Purchases.Include(p => p.Book).Include(p => p.Customer).ToList();
        }

// Get purchase by ID
        public async Task<Purchase> GetPurchaseById(int id)
        {
            return await _context.Purchases.Include(p => p.Book).Include(p => p.Customer).FirstOrDefaultAsync(p => p.Id == id);
        }
// Update the purchase by ID
        public async Task<bool> UpdatePurchase(int id, PurchaseUpdateDto purchase)
        {
            var existingPurchase = await _context.Purchases.FindAsync(id);
            if (existingPurchase == null) return false;

            _context.Entry(existingPurchase).CurrentValues.SetValues(purchase);

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PurchaseExists(id))
                    return false;
                throw;
            }
        }

        // Add a new purchase

        public string AddPurchase(Purchase purchase)
        {
            _context.Purchases.Add(purchase);
            _context.SaveChanges();
            if (purchase.Id == 0)
                return "Failed to add purchase";
            return "Purchase Added Successfully";
        }

// Delete purchase by ID
        public async Task<bool> DeletePurchase(int id)
        {
            var purchase = await _context.Purchases.FindAsync(id);
            if (purchase == null)
                return false;

            _context.Purchases.Remove(purchase);
            await _context.SaveChangesAsync();
            return true;
        }

        public bool PurchaseExists(int id)
        {
            return _context.Purchases.Any(e => e.Id == id);
        }
    }
}
