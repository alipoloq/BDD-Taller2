using FluentAssertions;
using SpecFlowPageFactoryExample.Pages;
using TechTalk.SpecFlow;

namespace SpecFlowPageFactoryExample.Steps
{
    [Binding]
    public class LoginSteps
    {
        private readonly LoginPage _loginPage;
        private readonly SecureAreaPage _secureAreaPage;

        // Utilizamos Inyección de Dependencias nativa de SpecFlow para instanciar las páginas
        public LoginSteps(LoginPage loginPage, SecureAreaPage secureAreaPage)
        {
            _loginPage = loginPage;
            _secureAreaPage = secureAreaPage;
        }

        [Given(@"Estoy en la página de inicio de sesión")]
        public void GivenEstoyEnLaPaginaDeInicioDeSesion()
        {
            _loginPage.NavigateTo();
        }

        [When(@"Ingreso el nombre de usuario ""(.*)"" y la contraseña ""(.*)""")]
        public void WhenIngresoElNombreDeUsuarioYLaContrasena(string username, string password)
        {
            _loginPage.EnterCredentials(username, password);
        }

        [When(@"Hago clic en el botón de login")]
        public void WhenHagoClicEnElBotonDeLogin()
        {
            _loginPage.ClickLogin();
        }

        [Then(@"Debería ver un mensaje de éxito indicando ""(.*)""")]
        public void ThenDeberiaVerUnMensajeDeExitoIndicando(string expectedMessage)
        {
            string actualMessage = _secureAreaPage.GetSuccessMessage();
            actualMessage.Should().Contain(expectedMessage);
        }
    }
}
