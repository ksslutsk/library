using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Task2.DTO;
using Task2.Models;
using Task2.Services;


namespace Task2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("OpenCORSPolicy")]
    public class BooksController : Controller
    {
        /*private readonly DataContext _context;*/
        private readonly BookService _bookService;
        private readonly IMapper _mapper;

        public BooksController(/*DataContext context*/BookService bookService, IMapper mapper)
        {
            /*_context = context;*/
            _bookService = bookService;
            _mapper = mapper;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShortBookInfoDTO>>> GetBooks([FromQuery] BookFilter bookFilter)
        {
            //BookFilter emptyFilter = new BookFilter();
            return Ok(await _bookService.GetBooks(bookFilter));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(long id)
        {
            var book = await _bookService.GetBook(id);
            var bookDTO = _mapper.Map<BookDetailDTO>(book);
            return Ok(bookDTO);
        }
        [HttpPost("save")]
        public async Task<IActionResult> CreateOrUpdateBook(CreateOrUpdateBookDTO createOrUpdateBook)
        {
            var bookModel = _mapper.Map<Book>(createOrUpdateBook);
            var bookId = await _bookService.CreateOrUpdate(bookModel);

            return Ok(bookId);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(long id)
        {
            var book = await _bookService.Delete(id);
            if (book == null)
            {
                return NotFound();
            }
            
            /*
            тут видяляти відгуки і оцінку
            */

            return NoContent();
        }
        
        [HttpPut("{id}/review")]
        public async Task<ActionResult<Review>> SaveReview(CreateReviewDTO review)
        {
            var book = await _bookService.GetBook(review.BookId);
            if (book == null)
            {
                return BadRequest("Book not found");
            }
            var reviewModel = _mapper.Map<Review>(review);
            var reviewId = await _bookService.AddReview(book.Id, reviewModel);
            return CreatedAtAction("SaveReview", new { id = reviewId });
        }
      
        [HttpPut("{id}/rate")]
        public async Task<IActionResult> SaveRate(long id,[FromBody] CreateRatingDTO createRating)
        {
            var ratingId = await _bookService.AddRate(id, createRating.Score);
            return CreatedAtAction("SaveRate", new { id = ratingId });
        }
    }
}
