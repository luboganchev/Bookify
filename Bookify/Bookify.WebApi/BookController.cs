using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bookify.WebApi
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
    }
}
