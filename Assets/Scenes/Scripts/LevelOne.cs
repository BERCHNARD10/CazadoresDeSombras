using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelOne : MonoBehaviour
{

    [SerializeField] private Text m_Txt = null;

    private string userName, password;

    private void Start()
    {
        CredentialsUser();
    }

    public void CredentialsUser() 
    {
        userName = sesionManager.UserName;
        password = sesionManager.Password;
        m_Txt.text = "User: " + userName + "Password: " + password;
    }
}
