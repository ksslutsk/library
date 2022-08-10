using Task2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Task2.Services
{
    public class BookService
    {
        private readonly DataContext _context;
        public BookService(DataContext context) {
            _context = context; 
        }

        public async Task<Book?> GetBook(long id) {
            var book = await _context.Books.FindAsync(id);
            if (book == null) return null;
            var reviews = _context.Reviews.Where(review => review.BookId == id).ToList();
            book.Reviews = reviews;
            var result_ratings = from rating in _context.Ratings where rating.BookId == book.Id select rating;
            var count = result_ratings.Count();
            var sum = result_ratings.Sum(r => r.Score);

            var average = count==0 ? 0 : sum / count;
            book.Rating = average;

            return book;
        }
        public async Task<IEnumerable<ShortBookInfo>> GetBooks(BookFilter filter) {
            var reitings = (from rating in _context.Ratings select rating).ToList();
            var books = (from book in _context.Books
                         where filter.Genre == null | book.Genre == filter.Genre 
                         select new ShortBookInfo
                         {
                             Id = book.Id,
                             Title = book.Title,
                             Cover = book.Cover,
                             Content = book.Content,
                             Author = book.Author,
                             Genre = book.Genre,
                             ReviewsNumber = (from review in _context.Reviews where book.Id == review.BookId select review).Count(),
                             Rating = ((from rating in _context.Ratings where book.Id == rating.BookId select rating).Count() == 0 ? 0 :
                             (from rating in _context.Ratings where book.Id == rating.BookId select rating).Average(r => r.Score))
                         }).AsEnumerable().OrderBy<ShortBookInfo, string>(b =>
                         {
                             switch (filter.Order)
                             {
                                 case BookOrder.title:
                                     return b.Title;
                                 case BookOrder.author:
                                     return b.Author;
                                 default:
                                     return string.Empty;
                             }
                         });

            return books;
        }
        public async Task<long> CreateOrUpdate(Book model)
        {
            var book = await _context.Books.FindAsync(model.Id);
        
            if (book == null)
            {
                var savedBook = _context.Books.Add(model);
                _context.SaveChanges();
                return savedBook.Entity.Id;
            }

            //не чіпати
            _context.Entry(book).State = EntityState.Detached;
            _context.Books.Update(model);
            _context.SaveChanges();
            return book.Id;
        }
        public async Task<Book?> Delete(long id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return null;
            }
            _context.Books.Remove(book);

            await _context.SaveChangesAsync();
            return book;
        }
        public async Task<long?> AddReview(long bookId, Review review)
        {
            var savedReview = _context.Reviews.Add(review);
            _context.SaveChanges();
            return savedReview.Entity.Id;
        }

        public async Task<long?> AddRate(long bookId, decimal score)
        {
            var rating = new Rating() { Score = score, BookId = bookId };
            var savedRating = _context.Ratings.Add(rating);
            await _context.SaveChangesAsync();

            return savedRating.Entity.Id;
        }        
    }
}
