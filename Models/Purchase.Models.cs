using System.ComponentModel.DataAnnotations;

namespace Books.Models
{
    public class PurchaseUpdateDto
    {
        public DateTime PurchaseDate { get; set; }
        public Books Book { get; set; }
        public Customer Customer { get; set; }
    }
    public class Purchase : PurchaseUpdateDto
    {

        [Key]
        public int Id { get; set; }
    }
}
