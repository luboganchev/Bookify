using RestSharp;

namespace Bookify.ApiTestsSpecFlow.ApiClients
{
    public class ApiClient
    {
        public ApiClient(ScenarioContext scenarioContext)
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
