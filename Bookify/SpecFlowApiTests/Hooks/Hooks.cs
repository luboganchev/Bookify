using Bookify.ApiTestsSpecFlow.Drivers;
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
            Driver client = new Driver(_scenarioContext); 
        }

    }
}
