using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public class Scene : MonoBehaviour
{
    [Header("DatosdelUsuario")]
    [SerializeField] public Text m_TxtUsuario = null;
    [SerializeField] public Text m_TxtMoneda = null;
    [SerializeField] public Text m_TxtEstrellas = null;

    [Header("Home")]
    [SerializeField] private RectTransform logo;
    [SerializeField] private GameObject botonJugar = null;
    [SerializeField] private GameObject botonSalir = null;
    [SerializeField] private GameObject botonCerrarSesion = null;

    [Header("Tramciciones")]
    [SerializeField] private trancicionScena canvasTransicion = null;


    [Header("Login")]
    [SerializeField] private InputField m_loginUsernameInput = null;
    [SerializeField] private InputField m_loginPassword = null;

    [Header("Register")]
    [SerializeField] private InputField m_usernameInput = null;
    [SerializeField] private InputField m_emailInput = null;
    [SerializeField] private Text m_Txt = null;
    [SerializeField] private InputField m_password = null;
    [SerializeField] private InputField m_reEnterPassword = null;

    [Header("Elemen")]
    [SerializeField] private RectTransform m_Header;
    [SerializeField] private GameObject m_background = null;
    [SerializeField] private GameObject m_registerUI = null;
    [SerializeField] private GameObject m_loginUI = null;

    private NetworkManager m_networkManager = null;
    private animacionesLeanTween animacionesLeanTween = null;
    private bool isAnimating = false;

    public string Levels;

    private void Awake() 
    {

        m_networkManager = FindObjectOfType<NetworkManager>();
        canvasTransicion = FindObjectOfType<trancicionScena>();
        animacionesLeanTween = FindObjectOfType<animacionesLeanTween>();
    }
    private void Start()
    {
        if (sesionManager.LoadCredentials())
        {
            m_TxtUsuario.text = "HOLA! " + sesionManager.UserName;
            m_TxtMoneda.text = sesionManager.PuntajeTotal.ToString();
            m_TxtEstrellas.text = sesionManager.Total_estrellas.ToString() + "/9";
            botonCerrarSesion.gameObject.SetActive(sesionManager.IsSessionValid());
        }
        animacionesLeanTween.animacionLogoHome(logo);
        animacionesLeanTween.animacionBotones(botonJugar, 1.1f, 0.7f, 1f);
        animacionesLeanTween.animacionBotones(botonSalir, 1.05f, 1f, 0.5f);
        if (botonCerrarSesion.activeSelf)
        {
            animacionesLeanTween.animacionBotones(botonCerrarSesion, 1.05f, 1f, 0.5f);
        }
    }

    private void Update()
    {
        if (!string.IsNullOrEmpty(m_Txt.text))
        {
            if (!isAnimating)
            {
                isAnimating = true;

                LeanTween.scale(m_Header, new Vector3(1, 1, 1), 0.2f)
                    .setOnComplete(() =>
                    {
                        LeanTween.delayedCall(3f, () =>
                        {
                            m_Txt.text = ""; // Clear the text after the delay
                        isAnimating = false; // Reset the animation flag
                    });
                    });
            }
        }
        else
        {
            if (!isAnimating)
            {
                isAnimating = true;

                LeanTween.scale(m_Header, new Vector3(0, 0, 0), 0.2f)
                    .setOnComplete(() =>
                    {
                        isAnimating = false; // Reset the animation flag
                });
            }
        }
    }

    public void SumitRegister()
    {
        /*m_usernameInput.text == "" || m_emailInput.text == "" || m_password.text == "" || m_reEnterPassword.text == ""*/

        /*string.IsNullOrEmpty(m_usernameInput.text) || string.IsNullOrEmpty(m_emailInput.text) ||
            string.IsNullOrEmpty(m_password.text) || string.IsNullOrEmpty(m_reEnterPassword.text)*/
        if (IsInputEmpty(m_usernameInput, m_emailInput, m_password, m_reEnterPassword))
        {
            m_Txt.text = "Por favor llena Todos los campos";
            return;
        }
        if (!IsValidUsername(m_usernameInput.text))
        {
            m_Txt.text = "El nombre de usuario debe tener exactamente 4 caracteres alfanuméricos.";
            return;
        }
        if (!IsValidEmail(m_emailInput.text))
        {
            m_Txt.text = "Correo electrónico inválido. Por favor, ingresa una dirección válida.";
            return;
        }
        if (!IsValidPassword(m_password.text))
        {
            m_Txt.text = "La contraseña debe tener entre 8 y 15 caracteres.";
            return;
        }
        if (m_password.text != m_reEnterPassword.text)
        {
            m_Txt.text = "Las contraseñas no coinciden.";
            return;
        }
        m_Txt.text = "Procesando ...";
        m_networkManager.CreateUser(m_usernameInput.text, m_emailInput.text, m_password.text, OnResponseReceived);

    }

    public void SumitLogin()
    {
        /*m_loginUsernameInput.text == "" || m_loginPassword.text == ""*/
        /*if (string.IsNullOrEmpty(m_loginUsernameInput.text) || string.IsNullOrEmpty(m_loginPassword.text))*/
        if (IsInputEmpty(m_loginUsernameInput, m_loginPassword))
        {
            m_Txt.text = "Por favor llena Todos los campos Choto";                       
            return;
        }
        m_Txt.text = "Procesando ...";
        m_networkManager.loginUser(m_loginUsernameInput.text, m_loginPassword.text, OnResponseReceived);

    }

    private bool IsInputEmpty(params InputField[] inputFields)
    {
        foreach (var inputField in inputFields)
        {
            if (string.IsNullOrEmpty(inputField.text))
            {
                return true;
            }
        }
        return false;
    }

    private bool IsValidUsername(string username)
    {
        // Verificar que el nombre de usuario tenga exactamente 4 caracteres alfanuméricos
        string usernamePattern = @"^[a-zA-Z0-9]{4,}$";
        return Regex.IsMatch(username, usernamePattern);
    }

    private bool IsValidEmail(string email)
    {
        // Expresión regular para validar el formato del correo electrónico
        string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        return Regex.IsMatch(email, emailPattern);
    }

    private bool IsValidPassword(string password)
    {
        // Verificar que la contraseña tenga entre 8 y 15 caracteres
        return password.Length >= 8 && password.Length <= 15;
    }

    private void ResetFields(params InputField[] inputFields)
    {
        foreach (var inputField in inputFields)
        {
            inputField.text = "";
        }
    }

    private void ResetLabel()
    {
        m_Txt.text = "";
    }
    private void OnResponseReceived(Response response)
    {

        m_Txt.text = response.message;



        if (response.done)
        {
            Debug.Log("SERVIDOR ENV1: " + response.userData.estrellasPorNivel.Nivel1);
            if (IsInputEmpty(m_usernameInput, m_password))
            {
                sesionManager.SetCredentials(m_loginUsernameInput.text, m_loginPassword.text, response.sessionExpiration, 
                                             response.userData.dineroPersonaje, response.userData.estrellasPorNivel.Nivel1, 
                                             response.userData.estrellasPorNivel.Nivel2, response.userData.estrellasPorNivel.Nivel3, 
                                             response.userData.total_estrellas);
            }
            else
            {
                sesionManager.SetCredentials(m_usernameInput.text, m_password.text, response.sessionExpiration, 
                                             response.userData.dineroPersonaje, response.userData.estrellasPorNivel.Nivel1, 
                                             response.userData.estrellasPorNivel.Nivel2, response.userData.estrellasPorNivel.Nivel3, 
                                             response.userData.total_estrellas);
            }
            LeanTween.cancelAll();
            SceneManager.LoadScene("guion");
        }
    }

    public void ShowWindow()
    {
        // Verificar si la sesión sigue siendo válida
        if (sesionManager.LoadCredentials())
        {
            // La sesión está abierta, cambiar a la escena "Levels"
            StartCoroutine(canvasTransicion.CambiarScena());
            botonCerrarSesion.gameObject.SetActive(sesionManager.IsSessionValid());
        }
        else
        {
            if (!m_registerUI.activeSelf && !m_loginUI.activeSelf)
            {
                m_loginUI.SetActive(true);
                animacionesLeanTween.mostrarMenuNormal(m_background, m_loginUI);
            }
            else if (m_registerUI.activeSelf)
            {
                ResetFields(m_usernameInput, m_emailInput, m_password, m_reEnterPassword);
                ResetLabel();
                /*m_registerUI.SetActive(false);
                m_loginUI.SetActive(true);*/
                m_registerUI.SetActive(false);
                m_loginUI.SetActive(true);
                animacionesLeanTween.ocultarMenu(m_background, m_registerUI);
                animacionesLeanTween.mostrarMenuNormal(m_background, m_loginUI);
            }
            else if (m_loginUI.activeSelf)
            {
                ResetFields(m_loginUsernameInput, m_loginPassword);
                ResetLabel();
                /*m_loginUI.SetActive(false);
                m_registerUI.SetActive(true);*/
                m_loginUI.SetActive(false);
                m_registerUI.SetActive(true);
                animacionesLeanTween.ocultarMenu(m_background, m_loginUI);
                animacionesLeanTween.mostrarMenuNormal(m_background, m_registerUI);
            }
        }
    }
    public void cerrarSesion()
    {
        // Verificar si la sesión sigue siendo válida
        if (sesionManager.LoadCredentials() && sesionManager.IsSessionValid())
        {
            sesionManager.ClearCredentials();
            m_Txt.text = "Se cerro sesion exitosamente";
            m_TxtUsuario.text = "INICIA SESION PARA JUGAR";
            m_TxtMoneda.text = sesionManager.PuntajeTotal.ToString();
            m_TxtEstrellas.text = sesionManager.Total_estrellas.ToString() + "/9";
            botonCerrarSesion.gameObject.SetActive(false);
        }
    }

    public void ClosedIU()
    {
        if (m_registerUI.activeSelf)
        {
            ResetFields(m_usernameInput, m_emailInput, m_password, m_reEnterPassword);
            ResetLabel();
            /*m_registerUI.SetActive(false);
            m_background.SetActive(false);*/
            animacionesLeanTween.ocultarMenu(m_background, m_registerUI);
            m_registerUI.SetActive(false);
        }
        else if (m_loginUI.activeSelf)
        {
            ResetFields(m_loginUsernameInput, m_loginPassword);
            ResetLabel();
            /*m_loginUI.SetActive(false);
            m_background.SetActive(false);*/
            animacionesLeanTween.ocultarMenu(m_background, m_loginUI);
            m_loginUI.SetActive(false);

        }
    }

    public void QuitGame()
    {
        Application.Quit();

    }
}
