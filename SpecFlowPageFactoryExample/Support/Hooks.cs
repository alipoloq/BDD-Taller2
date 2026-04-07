using System.IO;
using BoDi;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Model;

namespace SpecFlowPageFactoryExample.Support
{
    [Binding]
    public class Hooks
    {
        private readonly IObjectContainer _container;
        private IWebDriver _driver;
        private readonly ScenarioContext _scenarioContext;
        private static ExtentReports _extent;
        private static ExtentTest _feature;
        private static ExtentTest _scenario;

        public Hooks(IObjectContainer container, ScenarioContext scenarioContext)
        {
            _container = container;
            _scenarioContext = scenarioContext;
        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "TestResults");
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            var reporter = new ExtentSparkReporter(Path.Combine(path, "index.html"));
            _extent = new ExtentReports();
            _extent.AttachReporter(reporter);
        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext)
        {
            _feature = _extent.CreateTest<Feature>(featureContext.FeatureInfo.Title);
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            _scenario = _feature.CreateNode<Scenario>(_scenarioContext.ScenarioInfo.Title);

            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");

            _driver = new ChromeDriver(options);
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            
            _container.RegisterInstanceAs<IWebDriver>(_driver);
        }

        [AfterStep]
        public void AfterStep()
        {
            var stepType = _scenarioContext.StepContext.StepInfo.StepDefinitionType.ToString();
            var stepName = _scenarioContext.StepContext.StepInfo.Text;

            if (_scenarioContext.TestError == null)
            {
                if (stepType == "Given") _scenario.CreateNode<Given>(stepName);
                else if (stepType == "When") _scenario.CreateNode<When>(stepName);
                else if (stepType == "Then") _scenario.CreateNode<Then>(stepName);
            }
            else
            {
                var mediaEntity = CaptureScreenshotAndReturnModel(_scenarioContext.ScenarioInfo.Title.Trim());
                
                if (stepType == "Given") _scenario.CreateNode<Given>(stepName).Fail(_scenarioContext.TestError.Message, mediaEntity);
                else if (stepType == "When") _scenario.CreateNode<When>(stepName).Fail(_scenarioContext.TestError.Message, mediaEntity);
                else if (stepType == "Then") _scenario.CreateNode<Then>(stepName).Fail(_scenarioContext.TestError.Message, mediaEntity);
            }
        }

        [AfterScenario]
        public void AfterScenario()
        {
            if (_driver != null)
            {
                _driver.Quit();
                _driver.Dispose();
            }
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            _extent.Flush();
        }

        private Media CaptureScreenshotAndReturnModel(string screenShotName)
        {
            var screenshot = ((ITakesScreenshot)_driver).GetScreenshot().AsBase64EncodedString;
            return MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot, screenShotName).Build();
        }
    }
}
