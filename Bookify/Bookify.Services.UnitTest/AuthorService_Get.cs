using Bookify.Models;
using Bookify.Repositories;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Bookify.Services.UnitTest
{
    [TestClass]
    public class AuthorService_Get
    {
        private Mock<IAuthorRepository> authorRepository;
        private Mock<ILogger<Services.AuthorService>> logger;
        private Services.AuthorService sut;

        [TestInitialize]
        public void Init()
        {
            authorRepository = new Mock<IAuthorRepository>();
            logger = new Mock<ILogger<Services.AuthorService>>();
            sut = new Services.AuthorService(authorRepository.Object, logger.Object);
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