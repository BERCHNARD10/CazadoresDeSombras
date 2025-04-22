using UnityEngine;
using System;
using System.Collections;
using UnityEngine.Networking;

public class NetworkManager : MonoBehaviour
{
    public void CreateUser(string userName, string email, string pass, Action<Response> response)
    {
        StartCoroutine(SendRequest("https://chocolateelreal.com/Game/createUser.php", userName, email, pass, response));
    }

    public void loginUser(string userName, string pass, Action<Response> response)
    {
        StartCoroutine(SendRequest("https://chocolateelreal.com/Game/loginUser.php", userName, null, pass, response));
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
            Debug.LogError("Error en la solicitud: " + www.error);
            response(new Response { done = false, message = "Error en la solicitud: " + www.error });
            yield break;
        }

        Debug.Log("Respuesta del servidor: " + www.downloadHandler.text);
        response(JsonUtility.FromJson<Response>(www.downloadHandler.text));
    }
}



[Serializable]
public class Response
{
    public bool done = false;
    public string message = "";
}
