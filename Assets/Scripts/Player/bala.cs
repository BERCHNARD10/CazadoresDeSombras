using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bala : MonoBehaviour
{
    [SerializeField] private float velocidad;
    [SerializeField] private float danioGolpe;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * velocidad * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemigo"))
        {
            collision.transform.GetComponent<Enemigo>().TomarDaño(danioGolpe);
            Destroy(gameObject);
        }
        else if (collision.CompareTag("final"))
        {
            collision.transform.GetComponent<enemigoFinal>().TomarDaño(danioGolpe);
            Destroy(gameObject);

            //enemigoFinal e = FindObjectOfType<enemigoFinal>();
            //e.TomarDaño(danioGolpe);
        }
    }

}
