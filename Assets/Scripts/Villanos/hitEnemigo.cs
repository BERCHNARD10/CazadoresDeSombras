using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitEnemigo : MonoBehaviour
{

    [SerializeField] private Transform controladorDisparoEnemigo;
    [SerializeField] private GameObject balaEnemigo;
    [SerializeField] private bool enemigoconAtaqueaDistancia;


    /* private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            float damageAmount = 10f; // Puedes ajustar el valor del daño según tus necesidades.
            collision.gameObject.GetComponent<combateJugador>().TomarDaño(damageAmount);
            Debug.Log("Daño al jugador");
        }
    }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!enemigoconAtaqueaDistancia)
            {
                float damageAmount = 10f; // Puedes ajustar el valor del daño según tus necesidades.
                collision.gameObject.GetComponent<combateJugador>().TomarDaño(damageAmount);
                Debug.Log("Daño al jugador");
            }
            else
            {
                Disparar();
            }
        }
    }

    public void Disparar()
    {
        Instantiate(balaEnemigo, controladorDisparoEnemigo.position, controladorDisparoEnemigo.rotation);
    }
}
