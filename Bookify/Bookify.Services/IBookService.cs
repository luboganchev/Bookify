using Bookify.Models;

namespace Bookify.Services
{
    public interface IBookService
    {
        Task<IEnumerable<BookDto>> Get(int skip, int take);

        Task<BookDto> GetById(Guid id);

        Task<BookDto> Create(BookInputModel book);

        Task<BookDto> Update(Guid id, BookInputModel book);

        Task<bool> Delete(Guid id);
    }
}
