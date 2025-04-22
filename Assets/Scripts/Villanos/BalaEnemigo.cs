using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaEnemigo : MonoBehaviour
{
    [SerializeField] private float velocidad;
    [SerializeField] private float danioGolpe;
    private bool haCausadoDanio = false; // Nueva bandera booleana


    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * velocidad * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !haCausadoDanio)
        {
            Debug.Log(danioGolpe);
            collision.transform.GetComponent<combateJugador>().TomarDaño(danioGolpe);
            haCausadoDanio = true; // Establecer la bandera en true para evitar daño repetido
            Destroy(gameObject);
        }
    }
}
