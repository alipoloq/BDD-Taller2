using BoDi;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;

namespace SpecFlowPageFactoryExample.Support
{
    [Binding]
    public class Hooks
    {
        private readonly IObjectContainer _container;
        private IWebDriver _driver;

        public Hooks(IObjectContainer container)
        {
            _container = container;
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            var options = new ChromeOptions();
            // Ejecutar en modo headless si es preferible en CI/CD. 
            // options.AddArgument("--headless"); 
            options.AddArgument("--start-maximized");

            _driver = new ChromeDriver(options);
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            
            // Registramos el driver en el contenedor de IoC de SpecFlow 
            // para inyectarlo en las páginas y step definitions.
            _container.RegisterInstanceAs<IWebDriver>(_driver);
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
    }
}
