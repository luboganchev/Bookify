using Bookify.Models;
using Bookify.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bookify.WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int skip = 0, int take = 5)
        {
            var authors = await _authorService.Get(skip, take);

            return Ok(authors);
        }

        // GET api/<AuthorController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var author = await _authorService.GetById(id);
            if (author == null)
            {
                return NotFound();
            }

            return Ok(author);
        }

        // POST api/<AuthorController>
        [HttpPost]
        public async Task<IActionResult> Post(AuthorInputModel author)
        {
            var newAuthor = await _authorService.Create(author);

            return Ok(newAuthor);
        }

        // PUT api/<AuthorController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, AuthorInputModel author)
        {
            var updatedAuthor = await _authorService.Update(id, author);
            if (updatedAuthor == null)
            {
                return NotFound();
            }

            return Ok(updatedAuthor);
        }

        // DELETE api/<AuthorController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var isAuthorDeleted = await _authorService.Delete(id);

            return Ok(isAuthorDeleted);
        }
    }
}
