using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NivelSelect : MonoBehaviour
{
    [SerializeField] private GameObject UINevelSelected = null;
    private animacionesLeanTween animacionesLeanTween = null;
    // Asigna los conjuntos de estrellas desde el Inspector
    public GameObject estrellasNv1;
    public GameObject estrellasNv2;
    public GameObject estrellasNv3;
    private void Start()
    {
        animacionesLeanTween = FindObjectOfType<animacionesLeanTween>();
        animacionesLeanTween.animacionBotones(UINevelSelected, 1.05f, 1.5f, 1f);

        // Accede a la cantidad de estrellas ganadas en cada nivel desde tu clase SesionManager
        sesionManager.LoadCredentials();
        int estrellasGanadasNv1 = sesionManager.EstrellasNv1;
        int estrellasGanadasNv2 = sesionManager.EstrellasNv2;
        int estrellasGanadasNv3 = sesionManager.EstrellasNv3;

        // Activa o desactiva los conjuntos de estrellas según la cantidad ganada
        ActivarEstrellas(estrellasNv1, estrellasGanadasNv1);
        ActivarEstrellas(estrellasNv2, estrellasGanadasNv2);
        ActivarEstrellas(estrellasNv3, estrellasGanadasNv3);
    }
    public void nivelSelect(string noLevel)
    {
        LeanTween.cancelAll();

        if (noLevel != null)
        {
            submitButton(noLevel, sesionManager.UserName);
        }
    }

    public void showStar(string nameLevel)
    {
        LeanTween.cancelAll();
        if (nameLevel != null)
        {
            SceneManager.LoadScene(nameLevel); // Carga la siguiente escena después de que el video haya terminado
        }
    }

    public void submitButton(string nameLevel, string userName)
    {
        StartCoroutine(SendRequest("http://189.240.192.140/GameCodeLegends/nivelManager.php", userName, nameLevel));
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

    // Método para activar o desactivar un conjunto de estrellas
    private void ActivarEstrellas(GameObject conjuntoEstrellas, int cantidadEstrellas)
    {
        for (int i = 0; i < conjuntoEstrellas.transform.childCount; i++)
        {
            // Activa las estrellas según la cantidad ganada
            conjuntoEstrellas.transform.GetChild(i).gameObject.SetActive(i < cantidadEstrellas);
        }
    }
}
