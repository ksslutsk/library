﻿namespace Task2.DTO
{
    public class BookDetailDTO
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string? Cover { get; set; }
        public string? Content { get; set; }
        public string Author { get; set; }
        public string? Genre { get; set; }
        public decimal? Rating { get; set; }
        public List<ReviewDTO> Reviews { get; set; } = new List<ReviewDTO>();

    }
}
