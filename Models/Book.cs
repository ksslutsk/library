﻿namespace Task2.Models
{
    public class Book
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string? Cover { get; set; }
        public string? Content { get; set; }
        public string Author { get; set; }
        public string? Genre { get; set; }
        public decimal? Rating { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
