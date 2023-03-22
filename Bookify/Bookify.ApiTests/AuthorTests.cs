using Bookify.Models;
using FluentAssertions;
using NUnit.Framework;
using RestSharp;
using System.Net;
using Microsoft.EntityFrameworkCore;
using Bookify.ApiTests;
using NUnit.Allure.Core;

namespace Bookify.Testing.Api
{
    [AllureNUnit]
    public class AuthorTests : Base
    {
        private const string authorUri = "api/Author";

        [Test]
        [Category("Smoke")]
        public void GetAllAuthorsTest()
        {
            var request = new RestRequest(authorUri, Method.Get);

            var response =  client.Execute<List<AuthorDto>>(request);

            Assert.IsNotNull(response);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Data?.Count.Should().Be(5);
        }

        [TestCase(4, 1)]
        [TestCase(3, 2)]
        public void GetAllAuthorsTestParameterized(int skip, int take)
        {
            var request = new RestRequest(authorUri, Method.Get);
            request
                .AddParameter("skip", skip)
                .AddParameter("take", take);
            var response = client.Execute<List<AuthorDto>>(request);

            Assert.IsNotNull(response);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Data?.Count.Should().Be(take);
        }

        [Test]
        public void GetAuthorByIdTest()
        {
            var authorName = "Test Author 2";

            var createRequest = new RestRequest(authorUri, Method.Post);
            var author = new AuthorInputModel
            {
                FullName = authorName,
            };
            createRequest.AddBody(author);

            var createResponse = client.Execute<AuthorDto>(createRequest);

            createResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var authorId = createResponse.Data?.Id;


            var request = new RestRequest(authorUri + "/" + authorId, Method.Get);
            var response = client.Execute<AuthorDto>(request);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Data?.FullName.Should().Be(authorName);
        }

        [Test]
        public void GetAuthorByIdTest_Negative()
        {
            var authorId = Guid.NewGuid().ToString();
            var request = new RestRequest(authorUri + "/" + authorId, Method.Get);
            var response = client.Execute<AuthorDto>(request);

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Test]
        public void PostAuthorTest()
        {
            var author = "Test Author";
            var request = new RestRequest(authorUri, Method.Post);
            request.AddBody(new AuthorInputModel
            {
                FullName = author,
            });
            var response = client.Execute<AuthorDto>(request);

            Assert.IsNotNull(response);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            response.Data?.FullName.Should().Be(author);
        }

        [Test]
        public void PostAuthorTest_Negative()
        {
            var author = "Test Author";
            var request = new RestRequest(authorUri, Method.Post);
            request.AddBody(new AuthorInputModel
            {
                FullName = null,
            });
            var response = client.Execute(request);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            response.Content.Should().Contain("'Full Name' must not be empty.");
        }

        [Test]
        public void PutAuthorTest()
        {
            var createRequest = new RestRequest(authorUri, Method.Post);
            var author = new AuthorInputModel
            {
                FullName = "Test Author",
            };
            createRequest.AddBody(author);

            var createResponse = client.Execute<AuthorDto>(createRequest);

            createResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var authorId = createResponse.Data?.Id;

            var updatedAuthorName = "Test Author Updated";
            var putRequest = new RestRequest(authorUri + "/" + authorId, Method.Post);
            author.FullName = updatedAuthorName;
            putRequest.AddBody(author);

            var putResponse = client.Execute<AuthorDto>(createRequest);
             
            putResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            putResponse.Data?.FullName.Should().Be(updatedAuthorName);
        }

        [Test]
        public void DeleteAuthorTest()
        {
            var createRequest = new RestRequest(authorUri, Method.Post);
            var author = new AuthorInputModel
            {
                FullName = "Test Author",
            };
            createRequest.AddBody(author);

            var createResponse = client.Execute<AuthorDto>(createRequest);

            createResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var authorId = createResponse.Data?.Id;

            var deleteRequest = new RestRequest(authorUri + "/" + authorId, Method.Delete);

            var deleteResponse = client.Execute(deleteRequest);

            deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var getRequest = new RestRequest(authorUri + "/" + authorId, Method.Get);
            var getResponse = client.Execute<AuthorDto>(getRequest);
            getResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}