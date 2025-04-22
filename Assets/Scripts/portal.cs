using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portal : MonoBehaviour
{
    private animacionesLeanTween animacionesLeanTween = null;
    public GameObject final;
    [SerializeField] private GameObject backgroundUI;

    private void Awake()
    {
        animacionesLeanTween = FindObjectOfType<animacionesLeanTween>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            animacionesLeanTween.mostrarMenuNormal(backgroundUI, final);
        }
    }
  

}
