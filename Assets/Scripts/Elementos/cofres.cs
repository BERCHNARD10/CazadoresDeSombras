using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cofres : MonoBehaviour
{
    private Animator animator;
    public GameObject boladeFuego; // Prefab del objeto de vida
    public GameObject vidaPrefab; // Prefab del objeto de vida
    public GameObject monedasPrefab; // Prefab del objeto monedas
    public Transform spawnPoint; // Punto de generación de los objetos
    public float fuerzaMinima = 10f; // Fuerza de lanzamiento mínima
    public float fuerzaMaxima = 15f; // Fuerza de lanzamiento máxima
    public float gravedad = 9.81f; // Gravedad para la caída

    public bool cofreAbierto = false;
    public int cantidadObjetos; // Cantidad de objetos a entregar
    public float tiempoEspera; // Retraso entre entregas

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E) && !cofreAbierto)
            {
                animator.Play("CofreAbierto");
            }
        }
    }

    public void abrirCofre()
    {
        if(!cofreAbierto)
        {
            animator.Play("CofreAbierto");
        }
    }

    public IEnumerator MostrarItems()
    {
        for (int i = 0; i < cantidadObjetos; i++)
        {

            yield return new WaitForSeconds(tiempoEspera);

            GameObject itemToSpawn;

            // Generar aleatoriamente si se crea una moneda, bola de disparo o un objeto de vida
            if (Random.Range(0, 3) == 0 && i == cantidadObjetos - 1)
            {
                itemToSpawn = Instantiate(vidaPrefab, spawnPoint.position, Quaternion.identity);
            }
            else if (Random.Range(0, 4) == 0)
            {
                itemToSpawn = Instantiate(boladeFuego, spawnPoint.position, Quaternion.identity);
            }
            else
            {
                itemToSpawn = Instantiate(monedasPrefab, spawnPoint.position, Quaternion.identity);
            }

            Rigidbody2D rb = itemToSpawn.GetComponent<Rigidbody2D>();
            // Aplicar fuerza al objeto en una dirección aleatoria
            Vector2 direcciónDeLanzamiento = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
            float fuerzaDeLanzamiento = Random.Range(fuerzaMinima, fuerzaMaxima);
            rb.AddForce(direcciónDeLanzamiento * fuerzaDeLanzamiento, ForceMode2D.Impulse);
            // Establecer la gravedad para que el objeto caiga
            rb.gravityScale = gravedad;
        }
        cofreAbierto = true;
    }
}