using NUnit.Framework;
using RestSharp;


namespace Bookify.ApiTests
{
    public class Base
    {
        protected RestClient client;

        [OneTimeSetUp]
        public void Setup()
        {
            var clientOptions = new RestClientOptions()
            {
                BaseUrl = new Uri("https://localhost:7095/"),
                Authenticator = null,
            };
            client = new RestClient(clientOptions);
        }

        [OneTimeTearDown]
        public void tearDown()
        {

        }
    }
}
