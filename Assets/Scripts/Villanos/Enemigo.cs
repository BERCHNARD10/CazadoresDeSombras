using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemigo : MonoBehaviour
{
    [SerializeField] private float vida;
    [SerializeField] private float maximaVida;
    [SerializeField] private barradeVida BarradeVida;
    [SerializeField] private GameObject efectoMuerte;
    private Animator animator;
    public int rutina;
    public float cronometro;
    public int direccion;
    public float speed_walk;
    public float speed_run;
    public GameObject target;
    public bool atacando;

    public float rango_vision;
    public float rango_ataque;
    public GameObject rango;
    public GameObject hit;




    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        vida = maximaVida;
        barradeVida b = FindObjectOfType<barradeVida>();
        b.inicializarBarradeVida(vida);
        BarradeVida.inicializarBarradeVida(vida);
    }
    private void Update()
    {
        if (target != null)
        {
            if (BarradeVida != null)
            {
                BarradeVida.transform.position = transform.position + new Vector3(0f, 1.5f, 0f);
            }

            comportamientoV();
        }
    }
    public void comportamientoV()
    {
        if (Math.Abs(transform.position.x - target.transform.position.x) > rango_vision && !atacando)
        {
            animator.SetBool("correr", false);
            cronometro += 1 * Time.deltaTime;
            if (cronometro >= 4)
            {
                rutina = Random.Range(0, 2);
                cronometro = 0;
            }

            switch (rutina)
            {
                case 0:
                    animator.SetBool("caminar", false);
                    break;
                case 1:
                    direccion = Random.Range(0, 2);
                    rutina++;
                    break;
                case 2:
                    switch (direccion)
                    {
                        case 0:
                            transform.rotation = Quaternion.Euler(0, 0, 0);
                            transform.Translate(Vector3.right * speed_walk * Time.deltaTime);
                            break;
                        case 1:
                            transform.rotation = Quaternion.Euler(0, 180, 0);
                            transform.Translate(Vector3.right * speed_walk * Time.deltaTime);
                            break;
                    }
                    animator.SetBool("caminar", true);
                    break;
            }
        }
        else
        {
            if (Math.Abs(transform.position.x - target.transform.position.x) > rango_ataque && !atacando)
            {
                if (transform.position.x < target.transform.position.x)
                {
                    animator.SetBool("caminar", false);
                    animator.SetBool("correr", true);
                    transform.Translate(Vector3.right * speed_run * Time.deltaTime);
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    animator.SetBool("ataque", false);
                }
                else
                {
                    animator.SetBool("caminar", false);
                    animator.SetBool("correr", true);
                    transform.Translate(Vector3.right * speed_run * Time.deltaTime);
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                    animator.SetBool("ataque", false);
                }
            }
            else
            {
                if (!atacando)
                {
                    if (transform.position.x < target.transform.position.x)
                    {
                        transform.rotation = Quaternion.Euler(0, 0, 0);
                    }
                    else
                    {
                        transform.rotation = Quaternion.Euler(0, 180, 0);

                    }
                    animator.SetBool("caminar", false);
                    animator.SetBool("correr", false);
                }
            }
        }
    }

    public void Final_Ani()
    {
        animator.SetBool("ataque", false);
        atacando = false;
        rango.GetComponent<BoxCollider2D>().enabled = true;
    }

    public void colliderWeaponTrue()
    {
        hit.GetComponent<BoxCollider2D>().enabled = true;
    }

    public void colliderWeaponFalse()
    {
        hit.GetComponent<BoxCollider2D>().enabled = false;
    }
    public void TomarDaño(float daño)
    {
        cinemachineCamera.Instance.MoverCamara(2, 2, 0.5f);
        vida -= daño;
        BarradeVida.cambiarVidaActual(vida);
        animator.SetTrigger("daño");

        if (vida <= 0)
        {
            Muerte();
        }
    }

    private void Muerte()
    {
        Instantiate(efectoMuerte, transform.position, Quaternion.identity);

        puntajeEstrellas.numero += 1;
        Destroy(gameObject);

    }
}
