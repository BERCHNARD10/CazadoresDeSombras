using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class compras : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject canvaCompra;
    [SerializeField] private GameObject backgroundUI;
    [SerializeField] Button comprarFuego;
    [SerializeField] Button cerrar;
    [SerializeField] Button btnComprar;
    public float tiempo = 1;
    public float tiempoAnterior;
    private animacionesLeanTween animacionesLeanTween = null;

    private void Awake()
    {
        animacionesLeanTween = FindObjectOfType<animacionesLeanTween>();
    }
    void Start()
    {
        btnComprar.onClick.AddListener(mostrar);
        cerrar.onClick.AddListener(cerrarV);
        btnComprar.gameObject.SetActive(false);
    }

    public void restarPuntos()
    {
        puntaje c = FindObjectOfType<puntaje>();
        c.restarPuntos(10);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            btnComprar.gameObject.SetActive(true);
        }
    }

    public void mostrar()
    {
        Ethan e = FindObjectOfType<Ethan>();
        e.pasos.Pause();
        animacionesLeanTween.mostrarMenuConPausa(backgroundUI, canvaCompra);        
        btnComprar.gameObject.SetActive(false);
    }

    public void cerrarV()
    {
        animacionesLeanTween.ocultarMenu(backgroundUI, canvaCompra);
        btnComprar.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    public void validarRespuesta1()
    {
        puntaje puntaje = FindObjectOfType<puntaje>();
        puntaje.sumarPuntos(5);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        btnComprar.gameObject.SetActive(false);

    }
}
