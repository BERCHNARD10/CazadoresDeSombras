using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LevelOne : MonoBehaviour
{
    [Header("Datos de Usuario")]
    [SerializeField] private Text m_Txt = null;
    private string userName;
    [SerializeField] private GameObject HeaderImage;

    [Header("MenuPausa")]
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject backgroundUI;
    private bool isPaused = false;

    [Header("MenuNivelCompletado")]
    [SerializeField] private GameObject nevelComplete;
    [SerializeField] private Text m_TxtPuntajeVillanos = null;
    [SerializeField] private Text m_TxtCronometro = null;
    [SerializeField] private Text m_TxtMonedasGanadas = null;
    [SerializeField] private GameObject estrella1;
    [SerializeField] private GameObject estrella2;
    [SerializeField] private GameObject estrella3;
    Vector3 escalaActual;

   [Header("MenuGameOver")]
    public GameObject gameOverUI;

    [Header("AccionesBoton")]
    public Button homeButton; // Botón de continuar en la interfaz de usuario
    public Button levelSelectButton;
    public Button restarLvel;
    public Button pauseButton;

    [Header("ReferenciasAClases")]
    private combateJugador combateJugador;
    private animacionesLeanTween animacionesLeanTween = null;
    [SerializeField] private puntaje puntaje;
    [SerializeField] private puntajeEstrellas puntajeEstrellas;


    [Header("Cronometro")]
    private float tiempoInicio;
    private bool datosEnviados = false;

    [Header("Controles")]
    [SerializeField] private GameObject controles;

    [SerializeField] private int cantidadVillanos = 0;
    [SerializeField] private int limiteRango1 = 0;
    [SerializeField] private int limiteRango2 = 0;
    [SerializeField] private int limiteRango3 = 0;
    string nombreEscenaActual;
    int numeroNivel;
    private void Awake()
    {
        puntaje = FindObjectOfType<puntaje>();
        animacionesLeanTween = FindObjectOfType<animacionesLeanTween>();
        combateJugador = GameObject.FindGameObjectWithTag("Player").GetComponent<combateJugador>();
    }
    private void Start()
    {
        nombreEscenaActual = SceneManager.GetActiveScene().name;

        if (nombreEscenaActual == "nevelOne")
            numeroNivel = 1;
        else if (nombreEscenaActual == "nevelDos")
            numeroNivel = 2;
        else if (nombreEscenaActual == "nevel3")
            numeroNivel = 3;

        combateJugador.MuerteJugador += gameOver;
        animacionesLeanTween.ocultarMenu(backgroundUI, gameOverUI);
        StartCoroutine(ImageDesvanecimiento());
        CredentialsUser();
        Time.timeScale = 1f;
        tiempoInicio = 0f;
        #if UNITY_ANDROID
        controles.gameObject.SetActive(true);
        #elif UNITY_PC || UNITY_EDITOR || UNITY_WEBGL || UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
        controles.gameObject.SetActive(false);
        #endif
    }
    void Update()
    {
        escalaActual = nevelComplete.transform.localScale;
        tiempoInicio += Time.deltaTime;
        float tiempoMinutos = Mathf.Floor(tiempoInicio / 60);
        float tiempoSegundos = Mathf.Floor(tiempoInicio % 60);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }

        if (escalaActual == Vector3.one && !datosEnviados)
        {
            m_TxtPuntajeVillanos.text = puntajeEstrellas.numero.ToString();
            m_TxtCronometro.text = tiempoMinutos.ToString("00") + ":" + tiempoSegundos.ToString("00");
            m_TxtMonedasGanadas.text = puntaje.puntos.ToString();
            StartCoroutine(sesionManager.ModificarPuntaje(puntaje.puntos, true));
            VerificarVillanos(puntajeEstrellas.numero);
            datosEnviados = true;
        }

    }

    public IEnumerator ImageDesvanecimiento()
    {
        yield return new WaitForSeconds(2.0f); // Espera 2 segundos

        // Iniciar la animación de desvanecimiento
        animacionesLeanTween.DesvanecimientoImagen(HeaderImage);

    }

    public void CredentialsUser()
    {
        userName = sesionManager.UserName;
        m_Txt.text = "User: " + userName + "\nSession Expiration: " + sesionManager.SessionExpiration.ToString();
    }

    public void TogglePause()
    {
        if (isPaused)
            Resume();
        else
            Pause();
    }

    public void Pause()
    {
        animacionesLeanTween.mostrarMenuConPausa(backgroundUI, pauseMenuUI);
        isPaused = true;
        pauseButton.gameObject.SetActive(false);
    }

    public void Resume()
    {
        animacionesLeanTween.ocultarMenu(backgroundUI, pauseMenuUI);
        isPaused = false;
        pauseButton.gameObject.SetActive(true);
        Time.timeScale = 1f; // Reanudar el tiempo del juego
    }

    public void restartLevel()
    {
        Time.timeScale = 1f; // Reanudar el tiempo del juego
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void gameOver(object sender, EventArgs e)
    {
        StartCoroutine(ShoGameOver());
    }

    private IEnumerator ShoGameOver()
    {
        yield return new WaitForSeconds(3f);
        animacionesLeanTween.mostrarMenuConPausa(backgroundUI, gameOverUI);
    }

    public void LevelSelectionScene()
    {
        SceneManager.LoadScene("LevelSelectionScene");
    }

    public void showWHome()
    {
        SceneManager.LoadScene("Game");
    }

    private void OnDisable()
    {
        // Asegúrate de reanudar el tiempo si el script se desactiva
        Time.timeScale = 1f;
    }

    public void showLevel2()
    {
        SceneManager.LoadScene("nevelDos");
    }

    public void showLevel3()
    {
        SceneManager.LoadScene("nevel3");
    }

    public void comprarBolasdeFuego()
    {
        puntaje.restarPuntos(10);
    }

    private void VerificarVillanos(int villanosEliminados)
    {
        if (villanosEliminados > limiteRango1 && villanosEliminados <= limiteRango2)
        {
            animacionesLeanTween.AnimarEstrella(estrella1);
            StartCoroutine(sesionManager.enviarEstrellas(numeroNivel, 1));

        }
        else if (villanosEliminados > limiteRango2 && villanosEliminados <= limiteRango3)
        {
            animacionesLeanTween.AnimarEstrella(estrella1);
            animacionesLeanTween.AnimarEstrella(estrella2);
            StartCoroutine(sesionManager.enviarEstrellas(numeroNivel, 2));

        }
        else if (villanosEliminados > limiteRango3)
        {
            animacionesLeanTween.AnimarEstrella(estrella1);
            animacionesLeanTween.AnimarEstrella(estrella2);
            animacionesLeanTween.AnimarEstrella(estrella3);
            StartCoroutine(sesionManager.enviarEstrellas(numeroNivel, 3));

        }
        StartCoroutine(PausarDespuesDeAnimaciones());

    }
    private IEnumerator PausarDespuesDeAnimaciones()
    {
        // Esperar un tiempo suficiente para que las animaciones de estrellas se completen.
        yield return new WaitForSeconds(2);
        // Pausar el juego
        Time.timeScale = 0f;
    }

}