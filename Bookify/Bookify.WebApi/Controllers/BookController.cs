using Bookify.Services;
using Bookify.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bookify.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService bookService;

        public BookController(IBookService bookService)
        {
            this.bookService = bookService;
        }

        // GET: api/<BookController>
        [HttpGet]
        public async Task<IActionResult> Get(int skip = 0, int take = 5)
        {
            var books = await bookService.Get(skip, take);

            return Ok(books);
        }

        // GET api/<BookController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var book = await bookService.GetById(id);

            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        // POST api/<BookController>
        [HttpPost]
        public async Task<IActionResult> Post(BookInputModel book)
        {
            var newBook = await bookService.Create(book);

            if (newBook == null)
            {
                return BadRequest();
            }

            return Ok(newBook);
        }

        // PUT api/<BookController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, BookInputModel book)
        {
            var updatedBook = await bookService.Update(id, book);
            if (updatedBook == null)
            {
                return NotFound();
            }

            return Ok(updatedBook);
        }

        // DELETE api/<BookController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deletedBook = await bookService.Delete(id);

            return Ok(deletedBook);
        }
    }
}
