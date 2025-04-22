using UnityEngine;

public class SecureForm
{
    private WWWForm m_secureForm = null;
    private const string CONNECTION_PASSWORD = "(T,*,g>nSa'S[2W";
    private const string ANDROID_PASSWORD = "5:m1H~0ToCa9OGI";
    private const string PC_PASSWORD = "YBdQ<9?zX(%mgM7";
    private const string WEB_PASSWORD = "?OeUvWSOJ;9H.&3";

    public WWWForm secureForm { get { return m_secureForm; } }
    public SecureForm()
    {
        m_secureForm = new WWWForm();
        m_secureForm.AddField("connectionPass", CONNECTION_PASSWORD);

#if UNITY_ANDROID
        m_secureForm.AddField("os", "android");
        m_secureForm.AddField("platformPass", ANDROID_PASSWORD);
#elif UNITY_PC || UNITY_EDITOR || UNITY_WEBGL || UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
        m_secureForm.AddField("os", "pc");
        m_secureForm.AddField("platformPass", PC_PASSWORD);
#elif UNITY_WEBGL
        m_secureForm.AddField("os", "web");
        m_secureForm.AddField("platformPass", WEB_PASSWORD);
#endif
    }
}