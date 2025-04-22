using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class barradeVida : MonoBehaviour
{
    private Slider slider;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        animator = GetComponent<Animator>();
    }

    public void cambiarVidaMaxima(float vidaMaxima)
    {
        slider.maxValue = vidaMaxima;
    }

    public void cambiarVidaActual(float cantidadVida)
    {
        slider.value = cantidadVida;
        animator.SetTrigger("Golpe");
    }

    public void inicializarBarradeVida(float cantidadVida)
    {
        cambiarVidaMaxima(cantidadVida);
        cambiarVidaActual(cantidadVida);
    }
}
