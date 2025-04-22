using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class puntajeEstrellas : MonoBehaviour
{
    public static int numero = 0;

    [SerializeField] Text Puntuacionestrellas;

    private void Start()
    {
        Puntuacionestrellas.text = numero.ToString("");
    }


    private void Update()
    {
        Puntuacionestrellas.text = numero.ToString();
    }
}
