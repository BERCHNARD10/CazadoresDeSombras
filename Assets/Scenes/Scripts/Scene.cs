using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{
    [Header("Login")]
    [SerializeField] private InputField m_loginUsernameInput = null;
    [SerializeField] private InputField m_loginPassword = null;

    [Header("Register")]
    [SerializeField] private InputField m_usernameInput = null;
    [SerializeField] private InputField m_emailInput = null;
    [SerializeField] private Text m_Txt = null;
    [SerializeField] private InputField m_password = null;
    [SerializeField] private InputField m_reEnterPassword = null;

    [SerializeField] private GameObject m_Header = null;
    [SerializeField] private GameObject m_background = null;
    [SerializeField] private GameObject m_registerUI = null;
    [SerializeField] private GameObject m_loginUI = null;

    private NetworkManager m_networkManager = null;
    public string Levels;

    private float messageTimer = 0f;
    private const float messageDuration = 4f;


    private void Awake() {
        m_networkManager = FindObjectOfType<NetworkManager>();
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
        if (m_password.text == m_reEnterPassword.text)
        {
            m_Txt.text = "Procesando ...";
            m_networkManager.CreateUser(m_usernameInput.text, m_emailInput.text, m_password.text, OnResponseReceived);
        }
        else
        {
            m_Txt.text = "Las Contrasenas no conciden";
        }
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
            if (IsInputEmpty(m_usernameInput, m_password))
            {
                sesionManager.SetCredentials(m_loginUsernameInput.text, m_loginPassword.text);
            }
            else
            {
                sesionManager.SetCredentials(m_usernameInput.text, m_password.text);
            }


            // Usuario logeado exitosamente, cambia a la escena "Levels"
            SceneManager.LoadScene(Levels);
        }
    }

    private void Update()
    {
        if (!string.IsNullOrEmpty(m_Txt.text))
        {
            m_Header.SetActive(true);
            messageTimer += Time.deltaTime;

            if (messageTimer >= messageDuration)
            {
                m_Txt.text = "";
                m_Header.SetActive(false);
                messageTimer = 0f;
            }
        }
    }


    public void ShowWindow()
    {
        if (!m_registerUI.activeSelf && !m_loginUI.activeSelf)
        {
            m_loginUI.SetActive(true);
            m_background.SetActive(true);

        }
        else if (m_registerUI.activeSelf)
        {
            ResetFields(m_usernameInput, m_emailInput, m_password, m_reEnterPassword);
            ResetLabel();
            m_registerUI.SetActive(false);
            m_loginUI.SetActive(true);

        }
        else if (m_loginUI.activeSelf)
        {
            ResetFields(m_loginUsernameInput, m_loginPassword);
            ResetLabel();
            m_loginUI.SetActive(false);
            m_registerUI.SetActive(true);
        }
    }

    public void ClosedIU()
    {
        if (m_registerUI.activeSelf)
        {
            ResetFields(m_usernameInput, m_emailInput, m_password, m_reEnterPassword);
            ResetLabel();
            m_registerUI.SetActive(false);
            m_background.SetActive(false);
        }
        else if (m_loginUI.activeSelf)
        {
            ResetFields(m_loginUsernameInput, m_loginPassword); 
            ResetLabel();
            m_loginUI.SetActive(false);
            m_background.SetActive(false);
        }
    }
}
