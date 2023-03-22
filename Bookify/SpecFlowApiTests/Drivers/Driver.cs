using RestSharp;

namespace Bookify.ApiTestsSpecFlow.Drivers
{
    public class Driver
    {
        public Driver(ScenarioContext scenarioContext)
        {
            var clientOptions = new RestClientOptions()
            {
                BaseUrl = new Uri("https://localhost:7095/"),
                Authenticator = null,
            };
            var client = new RestClient(clientOptions);
            scenarioContext.Add("RestClient", client);
        }
    }
}
