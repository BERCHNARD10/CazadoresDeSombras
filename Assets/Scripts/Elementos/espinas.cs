using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class espinas : MonoBehaviour
{

    [SerializeField] private float tiempoEntreDaño;
    private float tiempoSiguienteDaño;

   /* private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            tiempoSiguienteDaño -= Time.deltaTime;
            if (tiempoSiguienteDaño <= 0)
            {
                collision.GetComponent<combateJugador>().TomarDaño(5, collision.GetContact(0).normal);
                tiempoSiguienteDaño = tiempoEntreDaño;
            }
        }
    }*/

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<combateJugador>().TomarDaño(5, collision.GetContact(0).normal);
        }
    }
}
