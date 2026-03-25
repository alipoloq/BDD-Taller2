using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace SpecFlowPageFactoryExample.Pages
{
    public class SecureAreaPage
    {
        private readonly IWebDriver _driver;

        public SecureAreaPage(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.Id, Using = "flash")]
        private IWebElement LblSuccessMessage { get; set; }

        public string GetSuccessMessage()
        {
            return LblSuccessMessage.Text;
        }
    }
}
