namespace SpecFlowPageFactoryExample.Pages
{
    using OpenQA.Selenium;
    using SeleniumExtras.PageObjects;

    public class LoginPage
    {
        private readonly IWebDriver _driver;

        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.Id, Using = "email")]
        private IWebElement TxtEmail { get; set; }

        [FindsBy(How = How.CssSelector, Using = "button[type='submit']")]
        private IWebElement BtnRetrievePassword { get; set; }

        public void NavigateTo()
        {
            _driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/forgot_password");
        }

        public void EnterCredentials(string username)
        {
            TxtEmail.SendKeys(username);
        }

        public void ClickLogin()
        {
            BtnRetrievePassword.Click();
        }
    }
}
