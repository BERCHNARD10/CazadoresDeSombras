using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RanEnemi : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator Animator;
    public enemigoFinal enemigo;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Animator.SetBool("caminar", false);
            Animator.SetBool("caminar", false);
            Animator.SetBool("ataque", true);
            enemigo.atacando = true;
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
