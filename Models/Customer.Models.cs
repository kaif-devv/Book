using System.ComponentModel.DataAnnotations;

namespace Books.Models
{
    public class CustomerUpdateDto
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }
    public class Customer : CustomerUpdateDto
    {
        [Key]
        public int Id { get; set; }
    }
}
