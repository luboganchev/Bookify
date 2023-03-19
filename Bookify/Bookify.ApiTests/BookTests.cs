using Bookify.Models;
using FluentAssertions;
using NUnit.Framework;
using RestSharp;
using System.Net;
using Microsoft.EntityFrameworkCore;
using Bookify.ApiTests;
using static Bookify.ApiTests.TestData.TestData;
using NUnit.Allure.Core;
using NUnit.Allure.Attributes;

namespace Bookify.Testing.Api
{
    [AllureNUnit]
    [AllureSuite("Books Api Suite")]
    [AllureFeature("Books Feature")]
    public class BookTests : Base
    {
        private const string bookUri = "api/Book";


        [AllureStory("As a user I want to get a list with all available books so I can modify it")]
        [Test(Description = "Get all available books")]
        public void GetAllBooksTest()
        {
            var request = new RestRequest(bookUri, Method.Get);

            var response =  client.Execute<List<BookDto>>(request);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Data?.Count.Should().Be(5);
        }

        [TestCase(4, 1)]
        [TestCase(3, 2)]
        public void GetAllBooksTestParameterized(int skip, int take)
        {
            var request = new RestRequest(bookUri, Method.Get);
            request
                .AddParameter("skip", skip)
                .AddParameter("take", take);
            var response = client.Execute<List<BookDto>>(request);

            Assert.IsNotNull(response);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Data?.Count.Should().Be(take);
        }

        [Test]
        public void GetBookByIdTest()
        {
            var createRequest = new RestRequest(bookUri, Method.Post);
            createRequest.AddBody(DefauldBookData);

            var createResponse = client.Execute<BookDto>(createRequest);

            createResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var bookId = createResponse.Data?.Id;

            var request = new RestRequest($"{bookUri}/{bookId}", Method.Get);
            var response = client.Execute<BookDto>(request);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Data?.Title.Should().Be(DefauldBookData.Title);
        }

        [Test]
        public void PostBookTest()
        {
            var createRequest = new RestRequest(bookUri, Method.Post);
            createRequest.AddBody(DefauldBookData);

            var createResponse = client.Execute<BookDto>(createRequest);

            createResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            createResponse.Data?.Title.Should().Be(DefauldBookData.Title);
        }

        [Test]
        public void PutBookTest()
        {
            var createRequest = new RestRequest(bookUri, Method.Post);
            createRequest.AddBody(DefauldBookData);

            var createResponse = client.Execute<BookDto>(createRequest);

            createResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var bookId = createResponse.Data?.Id;

            var updatedBookName = "Test Book Updated";
            var putRequest = new RestRequest($"{bookUri}/{bookId}", Method.Post);
            DefauldBookData.Title = updatedBookName;
            putRequest.AddBody(DefauldBookData);

            var putResponse = client.Execute<BookDto>(createRequest);

            putResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            putResponse.Data?.Title.Should().Be(updatedBookName);
        }

        [Test]
        public void DeleteBookTest()
        {
            var createRequest = new RestRequest(bookUri, Method.Post);
            createRequest.AddBody(DefauldBookData);

            var createResponse = client.Execute<BookDto>(createRequest);

            createResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var bookId = createResponse.Data?.Id;

            var deleteRequest = new RestRequest($"{bookUri}/{bookId}", Method.Delete);
            var deleteResponse = client.Execute(deleteRequest);

            deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var getRequest = new RestRequest($"{bookUri}/{bookId}", Method.Get);
            var getResponse = client.Execute<BookDto>(getRequest);
            getResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}