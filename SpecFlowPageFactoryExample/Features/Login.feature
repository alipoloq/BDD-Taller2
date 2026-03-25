Feature: Login
  COMO usuario del the-internet.herokuapp.com
  QUIERO iniciar sesión en la página
  PARA acceder al area segura

  @login_exitoso
  Scenario: Iniciar sesión con credenciales válidas
    Given Estoy en la página de inicio de sesión
    When Ingreso el nombre de usuario "tomsmith" y la contraseña "SuperSecretPassword!"
    And Hago clic en el botón de login
    Then Debería ver un mensaje de éxito indicando "You logged into a secure area!"
