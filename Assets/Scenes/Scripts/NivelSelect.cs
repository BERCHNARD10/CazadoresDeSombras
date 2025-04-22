using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;


public class NivelSelect : MonoBehaviour
{
    public void nivelSelect(string nameLevel)
    {
        if(nameLevel!=null)
        {
            submitButton(nameLevel);
            SceneManager.LoadScene(nameLevel); // Carga la siguiente escena después de que el video haya terminado
        }
    }

    public void submitButton(string nameLevel)
    {
        string userName = sesionManager.UserName;
        StartCoroutine(SendRequest("https://chocolateelreal.com/Game/LEVELS.php", userName, nameLevel));
    }

    private IEnumerator SendRequest(string url, string userName, string levelSelection)
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            Debug.Log("No hay conexión a Internet.");
            yield break;
        }

        SecureForm form = new SecureForm();
        form.secureForm.AddField("userName", userName);
        form.secureForm.AddField("levelSelection", levelSelection);

        UnityWebRequest www = UnityWebRequest.Post(url, form.secureForm);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.LogError("Error en la solicitud: " + www.error);
            yield break;
        }

        Debug.Log("Respuesta del servidor: " + www.downloadHandler.text);
    }

}
