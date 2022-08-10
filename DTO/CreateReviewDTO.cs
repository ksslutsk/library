namespace Task2.DTO
{
    public class CreateReviewDTO
    {
        public long Id { get; set; }
        public string? Message { get; set; }
        public long BookId { get; set; }
        public string? Reviewer { get; set; }
    }
}
