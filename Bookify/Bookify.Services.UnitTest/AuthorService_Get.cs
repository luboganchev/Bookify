using Bookify.Models;
using Bookify.Repositories;
using Microsoft.Extensions.Logging;
using Moq;

namespace Bookify.Services.UnitTest
{
    [TestClass]
    public class AuthorService_Get
    {
        private Mock<IAuthorRepository> authorRepository;
        private Services.AuthorService sut;

        [TestInitialize]
        public void Init()
        {
            authorRepository = new Mock<IAuthorRepository>();
            sut = new Services.AuthorService(authorRepository.Object);
        }

        [TestMethod]
        public async Task Should_Return_All_Authors_By_Page()
        {
            // Arrange
            var authors = new List<AuthorDto>
            {
                new AuthorDto { FullName = "Mike" },
                new AuthorDto { FullName = "Maria" },
                new AuthorDto { FullName = "Jane" },
                new AuthorDto { FullName = "Arnold" }
            };
            authorRepository
                .Setup(a => a.Get(0, 5))
                .Returns(() => Task.FromResult(authors.AsEnumerable()));

            // Act
            var result = await sut.Get(0, 5);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(authors.Count, result.Count());
        }

        [TestMethod]
        public async Task Should_Return_All_Author_Ordered_Alphabetically()
        {
            // Arrange
            var authors = new List<AuthorDto>
            {
                new AuthorDto { FullName = "Zoe" },
                new AuthorDto { FullName = "Maria" },
                new AuthorDto { FullName = "Jane" },
                new AuthorDto { FullName = "Arnold" }
            };
            authorRepository
                .Setup(a => a.Get(0, 5))
                .Returns(() => Task.FromResult(authors.AsEnumerable()));

            // Act
            var result = await sut.Get(0, 5);

            // Assert
            Assert.IsTrue(result.First().FullName.StartsWith("Arnold"));
            Assert.IsTrue(result.Last().FullName.StartsWith("Zoe"));
        }
    }
}