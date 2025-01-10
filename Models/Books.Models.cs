using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Books.Models
{
    public class BookUpdateDto
    {
        public string Name { get; set; }
        public string Author { get; set; }
    }

    public class Books
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
    }

}
