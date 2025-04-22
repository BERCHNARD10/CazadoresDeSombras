using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barrraEnergi : MonoBehaviour
{
    private Ethan Ethan;
    [SerializeField] private barraEnergia BarraEnergia;
    [Header("Barra de energia")]
    [SerializeField] private float energia;
    [SerializeField] private float maximaEnergia;

    void Start()
    {
     

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TomarEnergia(float energi)
    {
        Debug.Log(energi);

        energia -= energi;
        Debug.Log(energi);

        BarraEnergia.cambiarEnergiaActual(energia);

        if (energia <= 0)
        {
            Ethan e = FindObjectOfType<Ethan>();
            e.bandera = false;
        }

    }
}
