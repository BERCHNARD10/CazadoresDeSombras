using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SabioProgramador : MonoBehaviour
{
    public float rotationSpeed = 10f; // Velocidad de rotación en grados por segundo
    public GameObject Ventana;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {


            LeanTween.scale(Ventana.GetComponent<RectTransform>(), new Vector3(1, 1, 1), 0.5f).setDelay(0.5f).setEase(LeanTweenType.easeOutBack);
            if (Input.GetKeyDown(KeyCode.R))
            {

            }
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {

        LeanTween.scale(Ventana.GetComponent<RectTransform>(), new Vector3(0, 0, 0), 0.5f).setDelay(0.5f).setEase(LeanTweenType.easeOutBack);


    }
}