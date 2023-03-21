using RestSharp;
using System.Net;

namespace SpecFlowApiTests.StepDefinitions
{
    [Binding]
    public class AuthorApiSteps
    {
        private RestClient _client;
        private RestRequest _request;
        private RestResponse _response;

        public AuthorApiSteps(ScenarioContext scenarioContext)
        {
            _client = scenarioContext.Get<RestClient>("RestClient");
        }

        // [Given("I create an Api Client")]
        // public void GivenICreateAnApiClient()
        // {
        //    _client = new RestClient("https://localhost:7095/");
        // }

        [Given("I create Get Author request")]
        public void GivenICreateGetAuthorRequest()
        {
            _request = new RestRequest("api/Author", Method.Get);
        }

        [When("I execute the request")]
        public void WhenIExecuteTheRequest()
        {
            _response = _client.Execute(_request);
        }

        [Then("I see that the status code is '(.*)'")]
        public void ThenISeeThatTheStatusCodeIs(HttpStatusCode statusCode)
        {
            _response.StatusCode.Should().Be(statusCode);
        }
    }
}