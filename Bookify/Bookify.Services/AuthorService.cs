using Bookify.Entities;
using Bookify.Models;
using Bookify.Repositories;

namespace Bookify.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<IEnumerable<AuthorDto>> Get(int skip, int take)
        {
            var authors = await _authorRepository.Get(skip, take);

            //Additional logic
            authors = authors.OrderBy(author => author.FullName);
            foreach (var author in authors)
            {
                author.FullName = $"{author.FullName}™";
            }

            return authors;
        }

        public async Task<AuthorDto?> GetById(Guid id)
        {
            var author = await _authorRepository.GetByIdAsync(id);

            return author;
        }

        public async Task<AuthorDto> Create(AuthorInputModel author)
        {
            var authorEntity = new Author
            {
                FullName = author.FullName,
                DateOfBirth = author.DateOfBirth,
            };

            var newAuthor = await _authorRepository.CreateAsync(authorEntity);
            //_logger.LogInformation($"Author {author.FullName} created!");

            return newAuthor;
        }

        public async Task<AuthorDto?> Update(Guid id, AuthorInputModel author)
        {
            var updatedAuthor = await _authorRepository.UpdateAsync(id, author);

            if (updatedAuthor != null)
            {
                //_logger.LogInformation($"Author {author.FullName} updated!");
            }

            return updatedAuthor;
        }

        public async Task<bool> Delete(Guid id)
        {
            var isDeleted = await _authorRepository.DeleteAsync(id);
            //_logger.LogInformation($"Author with id:{id} deleted!");

            return isDeleted;
        }
    }
}
