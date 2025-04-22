using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombateCuerpoACuerpo : MonoBehaviour
{
    [SerializeField] private Transform controladorGolpe;
    [SerializeField] private float radioGolpe;
    [SerializeField] private float danioGolpe;
    private cofres cofre;

    private void Awake()
    {
        cofre = GetComponent<cofres>();

    }
    public void Golpe()
    {
        Collider2D[] objetos = Physics2D.OverlapCircleAll(controladorGolpe.position, radioGolpe);
        foreach(Collider2D colisionador in objetos)
        {
            if (colisionador.CompareTag("Enemigo"))
            {
                colisionador.transform.GetComponent<Enemigo>().TomarDaño(danioGolpe);

;            }
            else if (colisionador.CompareTag("Cofre"))
            {
                colisionador.transform.GetComponent<cofres>().abrirCofre();
            }


            else if (colisionador.CompareTag("final"))
            {
                enemigoFinal e = FindObjectOfType<enemigoFinal>();
                e.TomarDaño(danioGolpe);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(controladorGolpe.position, radioGolpe);
    }

}
