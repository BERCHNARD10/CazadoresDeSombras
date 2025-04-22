using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class IntroController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string nextSceneName;

    private void Start()
    {
        videoPlayer.loopPointReached += VideoFinished; // Se ejecuta cuando el video ha terminado de reproducirse
        videoPlayer.Play();
    }

    private void VideoFinished(VideoPlayer vp)
    {
        SceneManager.LoadScene(nextSceneName); // Carga la siguiente escena después de que el video haya terminado
        
    }
}
