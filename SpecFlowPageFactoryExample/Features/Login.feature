Feature: Login
  COMO usuario del the-internet.herokuapp.com
  QUIERO ingresar mi correo electrónico
  PARA recuperar mi contraseña

  @login_exitoso
  Scenario: Iniciar sesión con credenciales olvidadas
    Given Estoy en la página de inicio de sesión
    When Ingreso el correo electrónico "aelisapolo@gmail.com"
    And Hago clic en el botón Retrieve password
    Then Debería ver un mensaje de éxito indicando "An email has been sent for password recovery!"
