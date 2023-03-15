using Bookify.Entities;
using Bookify.Infrastructure;
using Bookify.Models;
using Bookify.Repositories.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Bookify.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly BookifyDbContext _dbContext;

        public AuthorRepository(BookifyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<AuthorDto>> Get(int skip, int take)
        {
            var authors = await _dbContext.Authors
                .FilterDeleted()
                .Skip(skip)
                .Take(take)
                .Select(author => new AuthorDto
                {
                    Id = author.Id,
                    FullName = author.FullName,
                    DateOfBirth = author.DateOfBirth,
                })
                .AsNoTracking()
                .ToArrayAsync();

            return authors;
        }

        public async Task<AuthorDto?> GetByIdAsync(Guid id)
        {
            var author = await _dbContext.Authors
                .FilterDeleted()
                .AsNoTracking()
                .Where(x => x.Id == id)
                .Select(a => new AuthorDto
                {
                    Id = a.Id,
                    FullName = a.FullName,
                    DateOfBirth = a.DateOfBirth,
                }).FirstOrDefaultAsync();

            return author;
        }

        public async Task<AuthorDto> CreateAsync(Author author)
        {
            var newAuthor = _dbContext.Authors.Add(author);
            await _dbContext.SaveChangesAsync();

            return new AuthorDto
            {
                Id = newAuthor.Entity.Id,
                FullName = newAuthor.Entity.FullName,
                DateOfBirth = newAuthor.Entity.DateOfBirth
            };
        }

        public async Task<AuthorDto?> UpdateAsync(Guid id, AuthorInputModel author)
        {
            var authorEntity = await _dbContext.Authors
                  .FilterDeleted()
                  .FirstOrDefaultAsync(x => x.Id == id);

            if (authorEntity != null)
            {
                authorEntity.FullName = author.FullName;
                authorEntity.DateOfBirth = author.DateOfBirth;

                await _dbContext.SaveChangesAsync();

                return new AuthorDto
                {
                    Id = id,
                    FullName = author.FullName,
                    DateOfBirth = author.DateOfBirth
                };
            }

            return null;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var author = await _dbContext.Authors
                  .FilterDeleted()
                  .FirstOrDefaultAsync(a => a.Id == id);

            if (author != null)
            {
                author.IsDeleted = true;
                await _dbContext.SaveChangesAsync();

                return true;
            }

            return false;
        }
    }
}
