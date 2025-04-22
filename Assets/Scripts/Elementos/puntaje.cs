using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class puntaje : MonoBehaviour
{

    public float puntos ;
    public Text m_Txt;   // Start is called before the first frame update
    public Text MostrarTotal;

    void Start()
    {
        m_Txt = GetComponent<Text>();       
    }

    // Update is called once per frame
    void Update()
    {
        m_Txt.text = puntos.ToString();
        MostrarTotal.text = sesionManager.PuntajeTotal.ToString();
    }

    public void sumarPuntos(float puntosEntrada)
    {

        puntos += puntosEntrada;
    }

    public void restarPuntos(float puntosEntrada)
    {
        HealthBar h = FindObjectOfType<HealthBar>();
        if (sesionManager.PuntajeTotal >= 10 && h.currentHealth < 100)
        {
            StartCoroutine(sesionManager.ModificarPuntaje(puntosEntrada, false));
            h.sumarEnergia(10);
            Debug.Log(puntosEntrada);
        }
        else
        {
            Debug.Log("No tienes suficientes monedas o barra de energia llena");
        }
    }
}
