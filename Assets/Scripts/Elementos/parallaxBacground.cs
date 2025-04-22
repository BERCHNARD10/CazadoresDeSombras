using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallaxBacground : MonoBehaviour
{
    [SerializeField] private Vector2 velocidadMovimiento;
    private Vector2 offset;
    private Material material;
    private Rigidbody2D jugador;
    public bool isPlayerActive = true;


    private void Awake()
    {
        material = GetComponent<SpriteRenderer>().material;
        jugador = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (jugador != null)
        {
            offset = (jugador.velocity.x * 0.1f) * velocidadMovimiento * Time.deltaTime;
        }
        if (gameObject.CompareTag("nubes"))
        {
            offset = velocidadMovimiento * Time.deltaTime;

        }
        material.mainTextureOffset += offset;
    }
}
