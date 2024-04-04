using System.ComponentModel.DataAnnotations;

namespace BookStore.API.Data
{
    public class Books
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        public string Description { get; set; }

    }
}
