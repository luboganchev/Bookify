using Bookify.ApiTestsSpecFlow.ApiClients;
using TechTalk.SpecFlow;

namespace Bookify.ApiTestsSpecFlow.Hooks
{
    [Binding]
    public class Hooks
    {
        private readonly ScenarioContext _scenarioContext;

  
        public Hooks(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [BeforeScenario()]
        public void InitializeApiClient()
        {
            ApiClient client = new ApiClient(_scenarioContext); 
        }

    }
}
