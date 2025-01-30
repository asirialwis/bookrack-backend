using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
namespace BookManagerAPI.Dtos;
    
     [Route("api/book")]
    [ApiController]
     public class BooksController : ControllerBase
    {
        // In-memory list to store books
        private static List<Book> Books = new List<Book>
        {
            new Book { Id = 1, Title = "Book 1", Author = "Author 1", ISBN = "1234567890", PublicationDate = new DateTime(2020, 1, 1) },
            new Book { Id = 2, Title = "Book 2", Author = "Author 2", ISBN = "0987654321", PublicationDate = new DateTime(2021, 6, 15) }
        };

        // GET: api/Books
        [HttpGet]
        public ActionResult<IEnumerable<BookDto>> GetBooks()
        {
            var bookDtos = Books.Select(b => new BookDto
            {
                Id = b.Id,
                Title = b.Title,
                Author = b.Author,
                ISBN = b.ISBN,
                PublicationDate = b.PublicationDate
            }).ToList();

            return Ok(bookDtos);
        }

        // GET: api/Books/{id}
        [HttpGet("{id}")]
        public ActionResult<BookDto> GetBook(int id)
        {
            var book = Books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return NotFound(new { Message = "Book not found" });
            }

            var bookDto = new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                ISBN = book.ISBN,
                PublicationDate = book.PublicationDate
            };

            return Ok(bookDto);
        }

        // POST: api/Books
        [HttpPost]
        public ActionResult<BookDto> CreateBook([FromBody] CreateBookDto createBookDto)
        {
            if (createBookDto == null)
            {
                return BadRequest(new { Message = "Invalid book data" });
            }

            var book = new Book
            {
                Id = Books.Any() ? Books.Max(b => b.Id) + 1 : 1,
                Title = createBookDto.Title,
                Author = createBookDto.Author,
                ISBN = createBookDto.ISBN,
                PublicationDate = createBookDto.PublicationDate
            };

            Books.Add(book);

            var bookDto = new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                ISBN = book.ISBN,
                PublicationDate = book.PublicationDate
            };

            return CreatedAtAction(nameof(GetBook), new { id = book.Id }, bookDto);
        }

        // PUT: api/Books/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateBook(int id, [FromBody] UpdateBookDto updateBookDto)
        {
            var book = Books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return NotFound(new { Message = "Book not found" });
            }

            book.Title = updateBookDto.Title ?? book.Title;
            book.Author = updateBookDto.Author ?? book.Author;
            book.ISBN = updateBookDto.ISBN ?? book.ISBN;
            book.PublicationDate = updateBookDto.PublicationDate ?? book.PublicationDate;

            return Ok(new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                ISBN = book.ISBN,
                PublicationDate = book.PublicationDate
            });
        }

        // DELETE: api/Books/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteBook(int id)
        {
            var book = Books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return NotFound(new { Message = "Book not found" });
            }

            Books.Remove(book);
            return NoContent();
    }
}
