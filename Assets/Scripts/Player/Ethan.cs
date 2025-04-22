using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ethan : MonoBehaviour
{
    [Header("Sonido")]
    public AudioSource pasos ;
    public AudioSource saltoS;
    public AudioSource ataque;
    public AudioSource disparo;
    public bool  bandera=true;
    private double tiempo= 0.440;
    private float tiempoAnterior = 0f;

    [Header("Movimiento")]
    private Rigidbody2D Rigidbody2D;
    private float movimientoHorizontal = 0f;
    private float movimientoAnterior = 0f;
    private Vector2 input;
    [SerializeField] private float velocidadDeNovimiento;
    [Range(0, 0.3f)] [SerializeField] private float suavizadoDeNovimiento;
    private Vector3 velocidad = Vector3.zero;
    private bool mirandoDerecha = true;

    [Header("Salto")]
    [SerializeField] private float fuerzaDeSalto;
    [SerializeField] private LayerMask queEsSuelo;
    [SerializeField] private Transform controllerSuelo;
    [SerializeField] private Vector3 dimensionesCaja;
    [SerializeField] private bool enSuelo;
    private bool salto = false;
    private bool primerSaltoRealizado = false;

    [Header("Escaleras")]
    [SerializeField] private float velocidadEscalar;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private bool enEscaleras;
    [SerializeField] private bool escalando;
    private float gravedadInicial;

    [Header("Daño")]
    [SerializeField] private Vector2 velocidadRebote;
    [SerializeField] public bool sePuedeMover = true;

    [Header("Animacion")]
    private Animator animator;

    [Header("Android")]
    public bool isLeft = false;
    public bool isRight = false;
    public bool isJump = false;
    public bool isUp = false;
    public bool isDown = false;


    [Header("Combate Cuerpo a Cuerpo")]
    private bool ataqueNormal = true;

    [SerializeField] private ParticleSystem particulas;
    // Start is called before the first frame update
    private void Awake()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
    }
    void Start()
    {
        gravedadInicial = Rigidbody2D.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        #if UNITY_ANDROID
            HandleAndroidInput();

        #elif UNITY_PC || UNITY_EDITOR || UNITY_WEBGL || UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
        HandlePCInput();
        #endif

        EstaEnScaleras();
        movimientoHorizontal = input.x * velocidadDeNovimiento;
       
        animator.SetFloat("Horizontal", Mathf.Abs(movimientoHorizontal));

        if(Mathf.Abs(Rigidbody2D.velocity.y) > Mathf.Epsilon)
        {
            animator.SetFloat("velocidadY",Mathf.Sign(Rigidbody2D.velocity.y));
        }
        else
        {
            animator.SetFloat("velocidadY", 0);
        }
    }
    #region AndroidInputMethods
#if UNITY_ANDROID
    private void HandleAndroidInput()
    {
        input.x = (isLeft ? -1f : 0f) + (isRight ? 1f : 0f);
        input.y = (isUp ? 1f : 0f) + (isDown ? -1f : 0f);

        if (isJump)
        {
            salto = true;
            isJump = false;
        }

        if(isDown)
        {
            desactivarPlataformas();
        }
    }
    public void clickLeft()
    {
        isLeft = true;
    }
    public void releaseLeft()
    {
        isLeft = false;
    }
    public void clickRight()
    {
        isRight = true;
    }
    public void releaseRight()
    {
        isRight = false;
    }
    public void clickJump()
    {
        isJump = true;
    }
    public void clickUp()
    {
        isUp = true;
    }
    public void releaseUp()
    {
        isUp = false;
    }
    public void clickDown()
    {
        isDown = true;
    }
    public void releaseDown()
    {
        isDown = false;
    }
#endif
    #endregion
    #region PCInputMethods
    #if UNITY_PC || UNITY_EDITOR || UNITY_WEBGL || UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
    private void HandlePCInput()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
        if (Input.GetButtonDown("Jump"))
        {
            salto = true;
        }

        if (Input.GetButtonDown("Vertical"))
        {
            desactivarPlataformas();
        }

        if (Input.GetButtonDown("Fire2") )
        {
            clicAtaqueDistancia();  
        }

        if (Input.GetMouseButtonDown(0))
        {
            clicAtaque();
        }
    }
    #endif
    #endregion

    private void FixedUpdate()
    {
        enSuelo = Physics2D.OverlapBox(controllerSuelo.position, dimensionesCaja, 0f, queEsSuelo);
        animator.SetBool("enSuelo", enSuelo);
        animator.SetBool("escalando", escalando);
        if (sePuedeMover)
        {
            Mover(movimientoHorizontal * Time.fixedDeltaTime, salto);
        }
        if (enSuelo)
        {
            primerSaltoRealizado = false; // Reiniciar la variable al tocar el suelo
        }
        Escalar();
        salto = false;

        if (Input.GetButtonDown("Horizontal") && enSuelo )
        {
            if ((tiempoAnterior + 1.958) <= Time.time)
            pasos.Play();
        }
        if (Input.GetButtonDown("Horizontal") && !enSuelo)
        {
            pasos.Pause();
        }
        if (Input.GetButtonUp("Horizontal") && enSuelo)
        {
            pasos.Pause();
        }

        if (Input.GetButtonDown("Jump") )
        {
            saltoS.Play();
            pasos.Pause();
        }
        if (Input.GetButtonDown("Jump") && Input.GetButtonUp("Horizontal"))
        {
            saltoS.Play();
        }
        if (Input.GetButtonDown("Fire2"))
        {
            disparo.Play();
        }

        if ((Input.GetMouseButtonDown(0)) && (( tiempoAnterior+ tiempo) <= Time.time))
        {
            ataque.Play();
            tiempoAnterior = Time.time;
            Debug.Log(Time.time);
        }
    }
   
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(controllerSuelo.position, dimensionesCaja);
    }

    private void Mover(float mover, bool saltar)
    {
        Vector3 velocidadObjetivo = new Vector2(mover, Rigidbody2D.velocity.y);
        Rigidbody2D.velocity = Vector3.SmoothDamp(Rigidbody2D.velocity, velocidadObjetivo, ref velocidad, suavizadoDeNovimiento);

       
        if (mover > 0 && !mirandoDerecha)
        {
            //
            Girar();
           
        }
        else if (mover < 0 && mirandoDerecha)
        {
            Girar();
        }

        if (enSuelo && saltar)
        {
            enSuelo = false;
            primerSaltoRealizado = true; // Realizar el primer salto
            Rigidbody2D.AddForce(new Vector2(0f, fuerzaDeSalto));
            particulas.Play();
        }
        else if (!enSuelo && primerSaltoRealizado && saltar)
        {
            // Realizar el segundo salto si ya se ha realizado el primer salto y se presiona el botón de salto nuevamente en el aire
            Rigidbody2D.velocity = new Vector2(Rigidbody2D.velocity.x, 0f); // Eliminar la velocidad vertical actual
            Rigidbody2D.AddForce(new Vector2(0f, fuerzaDeSalto));
            primerSaltoRealizado = false; // Evitar un tercer salto en el aire
            particulas.Play();
        }
        
    }
    private void Girar()
    {
        mirandoDerecha = !mirandoDerecha;
        /*Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;*/
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
        if(enSuelo)
        {
            particulas.Play();
        }  
    }
    public void clicAtaque()
    {
        CombateCuerpoACuerpo combateCuerpo = FindObjectOfType<CombateCuerpoACuerpo>();

        if (ataqueNormal)
        {
            animator.SetTrigger("ataque");
            combateCuerpo.Golpe();
        }
        else
        {
            animator.SetTrigger("ataque2");
            combateCuerpo.Golpe();
        }

        ataqueNormal = !ataqueNormal; // Alternar entre ataqueNormal y ataque2 en cada clic
    }

    public void clicAtaqueDistancia()
    {
        if (bandera)
        {
            DisparoJugador disparoJugador = FindObjectOfType<DisparoJugador>();
            animator.SetTrigger("ataqueBola");
            HealthBar b = FindObjectOfType<HealthBar>();
            b.TakeDamage(10);
            disparoJugador.Disparar();
        }
    }
    private void EstaEnScaleras()
    {
        if(boxCollider.IsTouchingLayers(LayerMask.GetMask("escaleras")))
        {
            enEscaleras = true;        
        }
        else
        {
            enEscaleras = false;
        }
    }
    private void Escalar()
    {
        if ((input.y != 0 || escalando) && enEscaleras)
        {
            Vector2 velocidadSubida = new Vector2(Rigidbody2D.velocity.x, input.y * velocidadEscalar);
            Rigidbody2D.velocity = velocidadSubida;
            Rigidbody2D.gravityScale = 0;
            escalando = true;
        }
        else
        {
            Rigidbody2D.gravityScale = gravedadInicial;
            escalando = false;
        }
        if(enSuelo)
        {
            escalando = false;
        }
    }
    private void desactivarPlataformas()
    {
        Collider2D[] objetos = Physics2D.OverlapBoxAll(controllerSuelo.position, dimensionesCaja, 0f, queEsSuelo);
        foreach (Collider2D item in objetos)
        {
            PlatformEffector2D platformEffector2D = item.GetComponent<PlatformEffector2D>();

            if (platformEffector2D != null)
            {
                Physics2D.IgnoreCollision(GetComponent<Collider2D>(), item.GetComponent<Collider2D>(), true);
            }
        }
    }

    public void rebote(Vector2 puntoGolpe)
    {
        Rigidbody2D.velocity = new Vector2(-velocidadRebote.x * puntoGolpe.x, velocidadRebote.y);
        particulas.Play();
    }

}
