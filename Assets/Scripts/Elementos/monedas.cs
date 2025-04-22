using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monedas : MonoBehaviour
{
    [SerializeField] private float cantidadPuntos;
    public AudioClip sonidoMoneda;

   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Camera.main.GetComponent<AudioSource>().PlayOneShot(sonidoMoneda,0.250f);

            puntaje puntaje = FindObjectOfType<puntaje>();
            puntaje.sumarPuntos(cantidadPuntos);

            Destroy(gameObject);

        }
    }
}

