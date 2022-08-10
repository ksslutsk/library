using System.ComponentModel.DataAnnotations.Schema;

namespace Task2.Models
{
    public class Review
    {
        public long Id { get; set; }
        public string? Message { get; set; }
        [ForeignKey("Book")]
        public long BookId { get; set; }
        public string? Reviewer { get; set; }
        public virtual Book Book { get; set; }
    }
}
