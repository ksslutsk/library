using System.Text.Json.Serialization;

namespace Task2.Models
{
    public enum BookOrder
    {
        author, title, id
    }
    public class BookFilter
    {
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? Genre { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public BookOrder Order { get; set; } = BookOrder.id;
    }
}
