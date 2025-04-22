using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class combateJugador : MonoBehaviour
{
    [SerializeField] private float vida;
    [SerializeField] private float maximaVida;
    public event EventHandler MuerteJugador;
    [SerializeField] private barradeVida barradeVidaJugador;
    private Ethan Ethan;
    private Animator animator;
    [SerializeField] private float tiempoPerdidaControl;
    [SerializeField] private GameObject efectoMuerte;
    private Rigidbody2D rnd2D;

    // Start is called before the first frame update
    private void Awake()
    {
        Ethan = GetComponent<Ethan>();
        animator = GetComponent<Animator>();
        rnd2D = GetComponent<Rigidbody2D>();
    }
    void Start()
    {

        vida = maximaVida;
        barradeVida b = FindObjectOfType < barradeVida>();
        b.inicializarBarradeVida(vida);
        barradeVidaJugador.inicializarBarradeVida(vida);

    }

    public void TomarDaño(float daño)
    {
        cinemachineCamera.Instance.MoverCamara(4, 4, 0.5f);
        Debug.Log(daño);

        vida -= daño;
        Debug.Log(vida);

        barradeVidaJugador.cambiarVidaActual(vida);
        StartCoroutine(PerderControl());

        if (vida <= 0)
        {
            HandleMuerte();
        }
    }

    public void TomarDaño(float daño, Vector2 posicion)
    {
        cinemachineCamera.Instance.MoverCamara(4, 4, 0.5f);
        vida -= daño;
        StartCoroutine(PerderControl());
        Ethan.rebote(posicion);
        barradeVidaJugador.cambiarVidaActual(vida);
        if (vida <= 0)
        {
            HandleMuerte();
        }
    }
    public void Curar(float curacion)
    {
        if ((vida + curacion) > maximaVida)
        {
            vida = maximaVida;
            barradeVidaJugador.cambiarVidaActual(vida);
        }
        else
        {
            vida += curacion;
            barradeVidaJugador.cambiarVidaActual(vida);
        }
    }
    private void Destruir()
    {

    }

    private void HandleMuerte()
    {
        rnd2D.constraints = RigidbodyConstraints2D.FreezeAll;
        MuerteJugador?.Invoke(this, EventArgs.Empty);
        Instantiate(efectoMuerte, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private IEnumerator PerderControl()
    {
        Ethan.sePuedeMover = false;
        animator.SetBool("golpeEspina", true);
        yield return new WaitForSeconds(tiempoPerdidaControl);
        Ethan.sePuedeMover = true;
        animator.SetBool("golpeEspina", false);
    }
}
