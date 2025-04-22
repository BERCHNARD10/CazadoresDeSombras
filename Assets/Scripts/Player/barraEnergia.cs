using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class barraEnergia : MonoBehaviour
{
    private Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();

    }

    public void cambiarEnergiaMaxima(float energiaMaxima)
    {
        slider.maxValue = energiaMaxima;
    }

    public void cambiarEnergiaActual(float cantidadEnergia)
    {
        slider.value = cantidadEnergia;

    }

    public void inicializarBarraEnergia(float cantidaEneriga)
    {
        cambiarEnergiaMaxima(cantidaEneriga);
        cambiarEnergiaActual(cantidaEneriga);
    }
}
