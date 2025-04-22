using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class subirVida : MonoBehaviour
{
    [SerializeField] private float cantidadDeVida;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            combateJugador vida = FindObjectOfType<combateJugador>();
            vida.Curar(cantidadDeVida);
            Destroy(gameObject);
        }
    }
}
