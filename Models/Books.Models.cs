using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Books.Models
{
    public class BookUpdateDto
    {
        public string Name { get; set; }
        public string Author { get; set; }
    }

    public class Books : BookUpdateDto
    {
        [Key]
        public int Id { get; set; }
    }

}
