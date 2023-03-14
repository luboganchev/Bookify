using Bookify.Entities;
using Bookify.Infrastructure;
using Bookify.Models;
using Bookify.Repositories.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Bookify.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly BookifyDbContext _dbContext;

        public BookRepository(BookifyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<BookDto?>> Get(int skip, int take)
        {
            var books = await _dbContext.Books
                .Include(b => b.Author)
                .FilterDeleted()
                .Skip(skip)
                .Take(take)
                .Select(b => new BookDto
                {
                    Id = b.Id,
                    Title = b.Title,
                    AuthorName = b.Author.FullName,
                    PagesCount = b.PagesCount
                })
                .AsNoTracking()
                .ToArrayAsync();

            return books;
        }

        public async Task<BookDto?> GetById(Guid id)
        {
            return await _dbContext.Books
                .Include(b => b.Author)
                .FilterDeleted()
                .Where(b => b.Id == id)
                .Select(b => new BookDto
                {
                    Id = b.Id,
                    Title = b.Title,
                    AuthorName = b.Author.FullName,
                    PagesCount = b.PagesCount
                })
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<BookDto> Create(Book book)
        {
            await _dbContext.Books.AddAsync(book);
            await _dbContext.SaveChangesAsync();

            await _dbContext.Entry(book).Reference(b => b.Author).LoadAsync();

            return new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                AuthorName = book.Author.FullName,
                PagesCount = book.PagesCount
            };
        }

        public async Task<BookDto?> Update(Guid id, BookInputModel book)
        {
            var bookEntity = await _dbContext.Books
                .Include(b => b.Author)
                .FilterDeleted()
                .FirstOrDefaultAsync(b => b.Id == id);

            if (bookEntity != null)
            {
                bookEntity.Title = book.Title;
                bookEntity.AuthorId = book.AuthorId;
                bookEntity.PagesCount = book.PagesCount;

                await _dbContext.SaveChangesAsync();

                return new BookDto
                {
                    Id = bookEntity.Id,
                    Title = bookEntity.Title,
                    PagesCount = bookEntity.PagesCount,
                    AuthorName = bookEntity.Author.FullName
                };
            }

            return null;
        }

        public async Task<bool> Delete(Guid id)
        {
            var book = await _dbContext.Books
                  .FilterDeleted()
                  .FirstOrDefaultAsync(b => b.Id == id);

            if (book != null)
            {
                _dbContext.Books.Remove(book);
                await _dbContext.SaveChangesAsync();

                return true;
            }

            return false;
        }
    }
}
