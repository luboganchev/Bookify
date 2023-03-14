using Bookify.Entities;
using Bookify.Models;

namespace Bookify.Repositories
{
    public interface IBookRepository
    {
        Task<IEnumerable<BookDto?>> Get(int skip, int take);

        Task<BookDto?> GetById(Guid id);

        Task<BookDto> Create(Book book);

        Task<BookDto?> Update(Guid id, BookInputModel book);

        Task<bool> Delete(Guid id);
    }
}