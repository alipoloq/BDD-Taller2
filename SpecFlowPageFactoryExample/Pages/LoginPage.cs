using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace SpecFlowPageFactoryExample.Pages
{
    public class LoginPage
    {
        private readonly IWebDriver _driver;

        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.Id, Using = "username")]
        private IWebElement TxtUsername { get; set; }

        [FindsBy(How = How.Id, Using = "password")]
        private IWebElement TxtPassword { get; set; }

        [FindsBy(How = How.CssSelector, Using = "button[type='submit']")]
        private IWebElement BtnLogin { get; set; }

        public void NavigateTo()
        {
            _driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/login");
        }

        public void EnterCredentials(string username, string password)
        {
            TxtUsername.SendKeys(username);
            TxtPassword.SendKeys(password);
        }

        public void ClickLogin()
        {
            BtnLogin.Click();
        }
    }
}
