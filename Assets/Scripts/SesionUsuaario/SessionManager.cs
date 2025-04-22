
using System;
using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public static class sesionManager 
{
    public static string UserName { get; private set; }
    public static string Password { get; private set; }
    public static DateTime SessionExpiration { get; private set; }
    public static float PuntajeTotal { get; private set; }
    public static int EstrellasNv1 { get; private set; }
    public static int EstrellasNv2 { get; private set; } 
    public static int EstrellasNv3 { get; private set; }
    public static int Total_estrellas { get; private set; } 

    public static void SetCredentials(string userName, string password, DateTime sessionExpiration, float puntajeTotal, int estrellasNv1, int estrellasNv2, int estrellasNv3, int total_estrellas)
    {
        UserName = userName;
        Password = password;
        SessionExpiration = sessionExpiration;
        PuntajeTotal = puntajeTotal;
        EstrellasNv1 = estrellasNv1;
        EstrellasNv2 = estrellasNv2;
        EstrellasNv3 = estrellasNv3;
        Total_estrellas = total_estrellas;
        // Guardar las credenciales en PlayerPrefs
        PlayerPrefs.SetString("Username", UserName);
        PlayerPrefs.SetString("Password", Password);
        PlayerPrefs.SetString("SessionExpiration", SessionExpiration.ToString());
        PlayerPrefs.SetFloat("PuntajeTotal", PuntajeTotal);
        PlayerPrefs.SetInt("EstrellasNv1", EstrellasNv1);
        PlayerPrefs.SetInt("EstrellasNv2", EstrellasNv2);
        PlayerPrefs.SetInt("EstrellasNv3", EstrellasNv3);
        PlayerPrefs.SetInt("Total_estrellas", Total_estrellas);
    }

    public static bool LoadCredentials()
    {
        string username, password, sessionExpirationString;
        float puntajeTotal;
        int estrellasNv1, estrellasNv2, estrellasNv3, total_estrellas;
        DateTime sessionExpiration;
        if (PlayerPrefs.HasKey("Username") && PlayerPrefs.HasKey("Password") && PlayerPrefs.HasKey("SessionExpiration") 
        && PlayerPrefs.HasKey("EstrellasNv1") && PlayerPrefs.HasKey("EstrellasNv2") && PlayerPrefs.HasKey("EstrellasNv3") 
        && PlayerPrefs.HasKey("PuntajeTotal") && PlayerPrefs.HasKey("Total_estrellas"))
        {
            username = PlayerPrefs.GetString("Username");
            password = PlayerPrefs.GetString("Password");
            sessionExpirationString = PlayerPrefs.GetString("SessionExpiration");
            sessionExpiration = DateTime.Parse(sessionExpirationString);
            puntajeTotal = PlayerPrefs.GetFloat("PuntajeTotal");
            estrellasNv1 = PlayerPrefs.GetInt("EstrellasNv1");
            estrellasNv2 = PlayerPrefs.GetInt("EstrellasNv2");
            estrellasNv3 = PlayerPrefs.GetInt("EstrellasNv3");
            total_estrellas = PlayerPrefs.GetInt("Total_estrellas");
            SetCredentials(username, password, sessionExpiration, puntajeTotal, estrellasNv1, estrellasNv2, estrellasNv3, total_estrellas);
            return true;
        }
        return false;
    }


    public static bool IsSessionValid()
    {
        if (DateTime.Now <= SessionExpiration)
        {
            // La sesión está abierta
            return true;
        }
        else
        {
            // La sesión ha expirado, borrar las credenciales
            ClearCredentials();
            return false;
        }
    }

    public static void ClearCredentials()
    {
        UserName = null;
        Password = null;
        SessionExpiration = DateTime.MinValue;
        PuntajeTotal = 0;
        EstrellasNv1 = 0;
        EstrellasNv2 = 0;
        EstrellasNv3 = 0;
        Total_estrellas = 0;
        // Eliminar las credenciales de PlayerPrefs
        PlayerPrefs.DeleteKey("Username");
        PlayerPrefs.DeleteKey("Password");
        PlayerPrefs.DeleteKey("SessionExpiration");
        PlayerPrefs.DeleteKey("EstrellasNv1");
        PlayerPrefs.DeleteKey("EstrellasNv2");
        PlayerPrefs.DeleteKey("EstrellasNv3");
        PlayerPrefs.DeleteKey("PuntajeTotal");
        PlayerPrefs.DeleteKey("Total_estrellas");
    }

    public static IEnumerator ModificarPuntaje(float puntajeModificado, bool sumarPuntaje)
    {
        LoadCredentials();
        string url = "https://bernard.cod3developer.com/CazadoresDeSombras/WebServices/nivelManager.php";

        if (sumarPuntaje)
        {
            PuntajeTotal += puntajeModificado;
        }
        else
        {
            PuntajeTotal -= puntajeModificado;
        }

        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            SecureForm form = new SecureForm();
            form.secureForm.AddField("userName", UserName);
            form.secureForm.AddField("dineroJugador", PuntajeTotal.ToString());
            UnityWebRequest www = UnityWebRequest.Post(url, form.secureForm);

            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.LogError("Error en la solicitud: " + www.error);
                yield break;
            }

            Debug.Log("Respuesta del servidor: " + www.downloadHandler.text);
        }

        PlayerPrefs.SetFloat("PuntajeTotal", PuntajeTotal);
    }

    public static IEnumerator enviarEstrellas(int nivel, int cantEstrellas)
    {
        LoadCredentials();
        string url = "https://bernard.cod3developer.com/CazadoresDeSombras/WebServices/nivelManager.php";

        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            SecureForm form = new SecureForm();
            form.secureForm.AddField("userName", UserName);
            form.secureForm.AddField("nivel", nivel.ToString());
            form.secureForm.AddField("estrellas", cantEstrellas.ToString());
            UnityWebRequest www = UnityWebRequest.Post(url, form.secureForm);

            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.LogError("Error en la solicitud: " + www.error);
                yield break;
            }

            Debug.Log("Respuesta del servidor: " + www.downloadHandler.text);
        }

        if(nivel==1)
            PlayerPrefs.SetInt("EstrellasNv1", cantEstrellas);
        else if(nivel==2)
            PlayerPrefs.SetInt("EstrellasNv2", cantEstrellas);
        else if(nivel == 3)
            PlayerPrefs.SetInt("EstrellasNv3", cantEstrellas);
    }

}
