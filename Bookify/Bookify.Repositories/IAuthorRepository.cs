using Bookify.Entities;
using Bookify.Models;

namespace Bookify.Repositories
{
    public interface IAuthorRepository
    {
        Task<IEnumerable<AuthorDto>> Get(int skip, int take);

        Task<AuthorDto?> GetByIdAsync(Guid id);

        Task<AuthorDto> CreateAsync(Author author);

        Task<AuthorDto?> UpdateAsync(Guid id, AuthorInputModel author);

        Task<bool> DeleteAsync(Guid id);
    }
}
