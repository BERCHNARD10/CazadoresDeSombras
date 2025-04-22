using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class preguntasProgramacion : MonoBehaviour
{
    public  new GameObject canvaPregunta;
    [SerializeField] Text text;
    [SerializeField] new  Button  boton1;
    [SerializeField] new Button boton2;
    [SerializeField] new Button boton3;
    [SerializeField] new Button boton4;
    [SerializeField]new  Button cerrar;
    [SerializeField] new Text textoBtn1;
    [SerializeField] new Text textoBtn2;
    [SerializeField] new Text textoBtn3;
    [SerializeField] new Text textoBtn4;
    [Header("Rangos")]
    public int rangoMenor;
    public int rangoMayor;
    [SerializeField] Button ok;
    public AudioSource correcto;
    public GameObject animacionEstrella;
    private bool bandera= true; //esta se ocupara para saber si el usuario ya contesto la pregunta , si ya la contesto ya no dejara abrirla
    [SerializeField] Text respuesta;
    private string preguntaSeleccionada;
    private string respuestaSeleccionada;
    private int numero; //esta variable almacenara un valor del random
    private animacionesLeanTween animacionesLeanTween = null;
    [SerializeField] private GameObject backgroundUI;


    //Random.Range(1, 4);
    // Start is called before the first frame update
    string[] preguntasPrograma = { 
        "Cuales son los dos principales grupos de datos",//preguntas de nivel 1
        "variable que solo acepta valores verdadero o falso",
        "Son tipos de operadores", 
        "Ejemplo de operadores aritmeticos",
        "La sentencia case se utiliza para",
        "Que es un dato constante?",
        "Son ejemplos de software de sistema:",
        "Emula un modelo del mundo real, donde los objetos realizan acciones e interactúan conotros objetos",
        "Es un intérprete que espera órdenes escritas por el usuario en el teclado, las interpreta y lasentrega al sistema operativo para su ejecución. La respuesta del sistema operativo se muestra alusuario en la misma ventana",
        "Símbolo para finalizar un diagrama de flujo",
        "¿A qué tipo de operadores pertenecen los símbolos +, -, *, /, %?",
        "¿A qué tipo de operadores pertenecen los símbolos =, +=, -=, *=, /=, %=?",
        "¿Por qué se considera una variable, constante?",
        //preguntas nivel 2
        "El ordenador es una máquina eléctrica y solo entiende el llamado código",
        "¿Qué es un Algoritmo?",
        "Es una representación gráfica del algoritmo.",
        "Es una forma de escribir los pasos, pero de la forma más cercana al lenguaje de programación que vamos a utilizar, es como un falso lenguaje, pero en nuestro idioma",
        "¿si queremos que la variable se use en todo el programa deberemos nombrarla como?",
        "Estas estructuras son instrucciones que se repiten formando un bucle (algo que se repite una y otra vez)",
        "Es un conjunto de instrucciones que se agrupan para realizar una tarea concreta y que se pueden reutilizar fácilmente.",
        "Es un dispositivo electrónico utilizado para procesar información y obtener resultados",
        "sirven para introducir datos (información) en la computadora para su proceso",
        "Es un chip (un circuito integrado) que controla y realiza las funciones y operaciones con los datos",
        "Es aquella en la que una acción sigue a otra en secuencia. Las operaciones se suceden de tal modo que la salida de una es la entrada de la siguiente y así sucesivamente hasta el fin del proceso.",
        //preguntas de nivel 3
        "Una combinación de 8 ceros y unos ( 8 bits = byte) mediante un código que se llama:",
        "Conjunto de pasos o instrucciones finitas y bien definidas que resuelven un problema o realizan una tarea.",
        "Es una estructura de control que permite ejecutar un bloque de código repetidamente mientras se cumple una condición.",
        "Son estructuras de selección",
        " Es un espacio en la memoria de la computadora que se utiliza para almacenar y representar valores, como números o cadenas de texto.",
        "Es un bloque de código reutilizable que realiza una tarea específica. Puede recibir argumentos, procesarlos y devolver un resultado.",
        "Es cuando una función se llama a sí misma para resolver un problema",
        "Es una estructura de datos que almacena una colección de elementos del mismo tipo, accesibles mediante un índice.",
        "Es utilizado para realizar operaciones lógicas entre valores booleanos. Ejemplos incluyen AND (&&), OR (||) y NOT (!).",
        "Permite ejecutar un bloque de código solo si una expresión booleana es verdadera.",
        "Es una herramienta que proporciona un conjunto de características como editor de código, depuración, compilación y otras utilidades para facilitar el desarrollo de software."
    };
    
    string[] respuestas = {
        "Constantes y variables",  //respuestas  de nivel 1
        "Bool", 
        "Aritmeticos, de relacion y logicos",
        "Multiplicacion",
        "Elegir entre diferentes alternativas",
        "Que su valor no puede cambiar durante la ejecucion de un programa",
        "Sistema operativo y lenguajes",
        "Programación orientada a objetos",
        "Consola",
        "Ovalo",
        "Aritméticos",
        "Asignación",
        "Porque su valor no puede cambiar en la ejecución del programa",
        //Respuestas de nivel 2
        "Binario",
        "Un algoritmo es una secuencia de PASOS a seguir para resolver un problema",
        "Diagrama de Flujo",
        "El Pseudocódigo",
        "Variable Global",
        "Estructuras Repetitivas",
        "Funciones",
        "Computadora",
        "Dispositivos de Entrada",
        "El microprocesador",
        "Estructuras secuenciales",
        //Respuestas de nivel 3
        "ASCII",
        "Algoritmo",
        "un bucle o loop",
        "if y switch",
        "variable",
        "Función o método",
        "La recursividad",
        "Un array o arreglo",
        "un operador lógico",
        " un condicional if",
        "IDE de programación"



    };


    string[] respuestasErroneas = { 
        "Software y hardware",
        "Activado y Desactivado", 
        "for",
        "variable",
        "arreglo", 
        "7", 
        "entero", 
        "no recibe nada", 
        "x=5", 
        "no devulve nada", 
        "25", 
        "string", 
        "y", 
        "Ninguna de las anteriores", 
        "Matriz",
        "if",
        "Siempre vale 0",
        "Visual basic",
        "Pseint",
        "lenguajes de programación"
    };
    private void Awake()
    {
        animacionesLeanTween = FindObjectOfType<animacionesLeanTween>();
    }
    void Start()
    {
        respuesta.text = "";
        ok.gameObject.SetActive(false);
        cerrar.onClick.AddListener(this.cerrarV);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {    
        if (bandera)
        {
            boton1.GetComponentInChildren<Text>().text = "";
            boton2.GetComponentInChildren<Text>().text = "";
            boton3.GetComponentInChildren<Text>().text = "";
            boton4.GetComponentInChildren<Text>().text = "";
            respuesta.text = "";

            if (collision.gameObject.CompareTag("Player") )
            {
                ok.gameObject.SetActive(true);
                boton1.interactable = true;
                boton2.interactable = true;
                boton3.interactable = true;
                boton4.interactable = true;
                ok.onClick.AddListener(mostrar);
                generarPreGunta();
            }
            boton1.onClick.AddListener(validarRespuesta1);
            boton2.onClick.AddListener(validarRespuesta2);
            boton3.onClick.AddListener(validarRespuesta3);
            boton4.onClick.AddListener(validarRespuesta4);
        }
    }

    public void mostrar()
    {
        Ethan e = FindObjectOfType<Ethan>();
        e.pasos.Pause();
        animacionEstrella.SetActive(false);
        bandera = false;
        ok.gameObject.SetActive(false);
        animacionesLeanTween.mostrarMenuConPausa(backgroundUI, canvaPregunta);
    }

    public void cerrarV()
    {
        animacionesLeanTween.ocultarMenu(backgroundUI, canvaPregunta);
        Time.timeScale = 1f;
    }

    public void generarPreGunta()
    {
        numero = Random.Range(rangoMenor, rangoMayor-1);
        text.text = preguntasPrograma[numero];
        preguntaSeleccionada= preguntasPrograma[numero];
        respuestaSeleccionada = respuestas[numero];
        colocarRespuesta(respuestaSeleccionada, Random.Range(0,4),numero);
    }
   

    public void colocarRespuesta(string respuesta, int numero, int numeroRespuesta)
    {

        if (numero == 0)
        {
            boton1.GetComponentInChildren<Text>().text = respuesta;

        }
        else
        {
            boton1.GetComponentInChildren<Text>().text = respuestasErroneas[Random.Range(0, respuestasErroneas.Length -1)];
           
        }


         if (numero == 1)
        {
            boton2.GetComponentInChildren<Text>().text = respuesta;
           
        }
        else
        {
            boton2.GetComponentInChildren<Text>().text = respuestasErroneas[Random.Range(0, respuestasErroneas.Length - 1)];

        }


        if (numero == 2)
        {
            boton3.GetComponentInChildren<Text>().text = respuesta;
            
        }
        else
        {
            boton3.GetComponentInChildren<Text>().text = respuestasErroneas[Random.Range(0, respuestasErroneas.Length - 1)];

        }


        if (numero == 3)
        {
            boton4.GetComponentInChildren<Text>().text = respuesta;
           
        }
        else
        {
            boton4.GetComponentInChildren<Text>().text = respuestasErroneas[Random.Range(0, respuestasErroneas.Length - 1)];

        }

    }

    public void validarRespuesta1( ) 
    {
        
        if (textoBtn1.text == respuestaSeleccionada)
        {
            Debug.Log("correcta");
            respuesta.text = "!Correcto!";
            puntaje puntaje = FindObjectOfType<puntaje>();
            puntaje.sumarPuntos(5);
            animacionEstrella.SetActive(true);
            correcto.Play();
            bandera = false;
            boton1.interactable = false;
            boton2.interactable = false;
            boton3.interactable = false;
            boton4.interactable = false;




        }
        else
        {
            Debug.Log("mal mi chavo");
            respuesta.text = "!Incorrecto!\n"+"La respuesta correcta es: "+respuestaSeleccionada;
            bandera = false;
            boton1.interactable=false;
            boton2.interactable = false;
            boton3.interactable = false;
            boton4.interactable = false;



        }
    }
    public void validarRespuesta2()
    {
        if (textoBtn2.text == respuestaSeleccionada)
        {
            Debug.Log("correcta");
            respuesta.text = "!Correcto!";
            puntaje puntaje = FindObjectOfType<puntaje>();
            animacionEstrella.SetActive(true);
            correcto.Play();
           
            puntaje.sumarPuntos(5);
            bandera = false;
            boton1.interactable = false;
            boton2.interactable = false;
            boton3.interactable = false;
            boton4.interactable = false;
        }
        else
        {
            respuesta.text = "!Incorrecto!\n" + "La respuesta correcta es: " + respuestaSeleccionada;
            bandera = false;
            boton1.interactable = false;
            boton2.interactable = false;
            boton3.interactable = false;
            boton4.interactable = false;
        }

    }
    public void validarRespuesta3()
    {
        if (textoBtn3.text == respuestaSeleccionada)
        {
            Debug.Log("correcta");
            respuesta.text = "!Correcto!";
            puntaje puntaje = FindObjectOfType<puntaje>();
            puntaje.sumarPuntos(5);
            animacionEstrella.SetActive(true);
            correcto.Play();
            
            bandera = false;
            boton1.interactable = false;
            boton2.interactable = false;
            boton3.interactable = false;
            boton4.interactable = false;

        }
        else
        {
            Debug.Log("mal mi chavo");
            respuesta.text = "!Incorrecto!\n" + "La respuesta correcta es: " + respuestaSeleccionada;
            bandera = false;
            boton1.interactable = false;
            boton2.interactable = false;
            boton3.interactable = false;
            boton4.interactable = false;
        }
    }
    public void validarRespuesta4()
    {
        if (textoBtn4.text == respuestaSeleccionada)
        {
            Debug.Log("correcta");
            respuesta.text = "!Correcto!";
            puntaje puntaje = FindObjectOfType<puntaje>();
            puntaje.sumarPuntos(5);
            animacionEstrella.SetActive(true);
            correcto.Play();
            
            bandera = false;
            boton1.interactable = false;
            boton2.interactable = false;
            boton3.interactable = false;
            boton4.interactable = false;

        }
        else
        {
            Debug.Log("mal mi chavo");
            respuesta.text = "!Incorrecto!\n" + "La respuesta correcta es: " + respuestaSeleccionada;
            bandera = false;
            boton1.interactable = false;
            boton2.interactable = false;
            boton3.interactable = false;
            boton4.interactable = false;
        } 

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        ok.gameObject.SetActive(false);
    }

}