using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ethan : MonoBehaviour
{
    [Header("Movimiento")]
    private Rigidbody2D Rigidbody2D;
    private float movimientoHorizontal=0f;
    [SerializeField] private float velocidadDeNovimiento;
    [Range(0 , 0.3f)] [SerializeField] private float suavizadoDeNovimiento;
    private Vector3 velocidad = Vector3.zero;

    private bool mirandoDerecha = true;
    [Header("Salto")]
    [SerializeField] private float fuerzaDeSalto;
    [SerializeField] private LayerMask queEsSuelo;
    [SerializeField] private Transform controllerSuelo;
    [SerializeField] private Vector3 dimensionesCaja;
    [SerializeField] private bool enSuelo;
    private bool salto = false;


    [Header("Animacion")]
    private Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        movimientoHorizontal = Input.GetAxis("Horizontal") * velocidadDeNovimiento;

        animator.SetFloat("Horizontal", Mathf.Abs(movimientoHorizontal));

        if (Input.GetButtonDown("Jump"))
        {
            salto = true;
        }
    }

    private void FixedUpdate()
    {
        enSuelo = Physics2D.OverlapBox(controllerSuelo.position, dimensionesCaja, 0f, queEsSuelo);
        animator.SetBool("enSuelo", enSuelo);
        Mover(movimientoHorizontal * Time.fixedDeltaTime, salto);

        salto = false;
    }

    private void Mover(float mover, bool saltar)
    {
        Vector3 velocidadObjetivo = new Vector2(mover, Rigidbody2D.velocity.y);
        Rigidbody2D.velocity = Vector3.SmoothDamp(Rigidbody2D.velocity, velocidadObjetivo, ref velocidad, suavizadoDeNovimiento);

        if(mover>0 && !mirandoDerecha)
        {
            //
            Girar();
        }
        else if(mover<0 && mirandoDerecha)
        {
            Girar();
        }

        if(enSuelo && saltar)
        {
            enSuelo = false;
            Rigidbody2D.AddForce(new Vector2(0f, fuerzaDeSalto));
        }
    }

    private void Girar()
    {
        mirandoDerecha = !mirandoDerecha;
        Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(controllerSuelo.position, dimensionesCaja);
    }
}
