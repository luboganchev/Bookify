using Bookify.Entities;
using Bookify.Models;
using Bookify.Repositories;
using Microsoft.Extensions.Logging;

namespace Bookify.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly ILogger _logger;

        public BookService(
            IBookRepository bookRepository,
            IAuthorRepository authorRepository,
            ILogger<BookService> logger)
        {
            _bookRepository = bookRepository;
            this._authorRepository = authorRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<BookDto>> Get(int skip, int take)
        {
            var books = await _bookRepository.Get(skip, take);

            return books;
        }

        public async Task<BookDto?> GetById(Guid id)
        {
            var book = await _bookRepository.GetById(id);

            return book;
        }

        public async Task<BookDto> Create(BookInputModel book)
        {
            var author = await _authorRepository.GetByIdAsync(book.AuthorId);
            if (author == null)
            {
                return null;
            }

            var bookEntity = new Book(book.Title, book.PagesCount, book.AuthorId);
            var newBook = await _bookRepository.Create(bookEntity);
            _logger.LogInformation($"Book {newBook.Title} created!");

            return newBook;
        }

        public async Task<BookDto> Update(Guid id, BookInputModel book)
        {
            throw new NotImplementedException();

            //var author = await _authorRepository.GetByIdAsync(book.AuthorId);
            //if (author == null)
            //{
            //    return null;
            //}

            //var updatedBook = await _bookRepository.Update(id, book);
            //if (updatedBook != null)
            //{
            //    _logger.LogInformation($"Book {updatedBook.Title} updated!");
            //}

            //return updatedBook;
        }

        public Task<bool> Delete(Guid id)
        {
            var isDeleted = _bookRepository.Delete(id);
            //_logger.LogInformation($"Book with id:{id} deleted!");

            return isDeleted;
        }
    }
}