using System.ComponentModel.DataAnnotations.Schema;

namespace Task2.Models
{
    public class Rating
    {
        public int Id { get; set; }
        [ForeignKey("Book")]
        public long BookId { get; set; }
        public decimal Score { get; set; }
    }
}
