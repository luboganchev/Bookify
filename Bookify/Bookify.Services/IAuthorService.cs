using Bookify.Models;

namespace Bookify.Services
{
    public interface IAuthorService
    {
        Task<IEnumerable<AuthorDto>> Get(int skip, int take);

        Task<AuthorDto?> GetById(Guid id);

        Task<AuthorDto> Create(AuthorInputModel author);

        Task<AuthorDto?> Update(Guid id, AuthorInputModel author);

        Task<bool> Delete(Guid id);
    }
}
