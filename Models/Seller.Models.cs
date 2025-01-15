using System.ComponentModel.DataAnnotations;

namespace Books.Models
{
    public class SellerUpdateDto
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }
    public class Seller : SellerUpdateDto
    {
        [Key]
        public int Id { get; set; }
    }
}
