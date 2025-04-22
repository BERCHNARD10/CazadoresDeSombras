using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogoMago : MonoBehaviour
{
    public bool bandera = true;
    public TextMeshProUGUI dialogTexto;
    public GameObject magoDialogo;
    public string[] lines;
    public float velocidadTexto;

    public Button Entendido;

    public bool bandera2 = false;
    int index;
    void Start()
    {
        dialogTexto.text = string.Empty;
        magoDialogo.SetActive(false);
        

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        magoDialogo.SetActive(true);
        StartDialogue();

    }

    // Update is called once per frame
    void Update()
    {
        Entendido.onClick.AddListener(sumaar);

        if (bandera == true && bandera2)
        {

            if (dialogTexto.text == lines[index])
            {
                siguienteLinea();
                bandera2 = false;

            }
            else
            {
                StopAllCoroutines();
                dialogTexto.text = lines[index];
                bandera2 = false;

            }
        }



    }
    public void sumaar()
    {
        bandera2 = true;

    }





    public void StartDialogue()
    {
        index = 0;
        Time.timeScale = 0f;
        StartCoroutine(WriteLine());
        bandera = true;
        bandera2 = false;

    }
    IEnumerator WriteLine()
    {
        foreach (char letter in lines[index].ToCharArray())
        {
            dialogTexto.text += letter;
            yield return new WaitForSecondsRealtime(velocidadTexto);

        }
    }

    public void siguienteLinea()
    {
        if (index < lines.Length - 1)
        {
            index++;
            dialogTexto.text = string.Empty;
            StartCoroutine(WriteLine());
        }
        else
        {
            magoDialogo.SetActive(false);
            Time.timeScale = 1f;
            bandera = false;
            Entendido.gameObject.SetActive(false);
        }
    }
}
