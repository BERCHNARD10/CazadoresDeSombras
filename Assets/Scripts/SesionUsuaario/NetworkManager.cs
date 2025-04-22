using UnityEngine;
using System;
using System.Collections;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.Collections.Generic;

public class NetworkManager : MonoBehaviour
{
    public void CreateUser(string userName, string email, string pass, Action<Response> response)
    {
        StartCoroutine(SendRequest("https://bernard.cod3developer.com/CazadoresDeSombras/WebServices/createUser.php", userName, email, pass, response));
    }

    public void loginUser(string userName, string pass, Action<Response> response)
    {
        StartCoroutine(SendRequest("https://bernard.cod3developer.com/CazadoresDeSombras/WebServices/loginUser.php", userName, null, pass, response));
    }

    private IEnumerator SendRequest(string url, string userName, string email, string pass, Action<Response> response)
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            Debug.Log("No hay conexión a Internet.");
            response(new Response { done = false, message = "No hay conexión a Internet." });
            yield break;
        }

        SecureForm form = new SecureForm();
        form.secureForm.AddField("userName", userName);
        if (!string.IsNullOrEmpty(email))
            form.secureForm.AddField("email", email);
        form.secureForm.AddField("pass", pass);

        UnityWebRequest www = UnityWebRequest.Post(url, form.secureForm);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log("Error en la solicitud: " + www.error);
            response(new Response { done = false, message = "Error en la solicitud, se fue la conexion a internet" /*+ www.error */});
            yield break;
        }
        Debug.Log("Respuesta JSON recibida: " + www.downloadHandler.text);
        Response serverResponse = JsonUtility.FromJson<Response>(www.downloadHandler.text);

        if (!string.IsNullOrEmpty(serverResponse.sessionExpirationString))
        {
            string format = "dd/MM/yyyy HH:mm:ss";
            serverResponse.sessionExpiration = DateTime.ParseExact(serverResponse.sessionExpirationString, format, null);
        }
        response(serverResponse);
    }
}

[Serializable]
public class Response
{
    public bool done = false;
    public string message = "";
    public string sessionExpirationString = "";
    public DateTime sessionExpiration;
    public UserData userData;

    [Serializable]
    public class UserData
    {
        public int total_estrellas;
        public float dineroPersonaje;
        public EstrellasPorNivel estrellasPorNivel;

        [Serializable]
        public class EstrellasPorNivel
        {
            public int Nivel1;
            public int Nivel2;
            public int Nivel3;
        }
    }
}