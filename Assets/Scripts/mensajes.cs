using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mensajes : MonoBehaviour
{
    // Start is called before
    // the first frame update

    public Text AndroOPc;
    public Image image;
    public Image cofre;

    public Image btnEspada;



    // Update is called once per frame
    void Update()
    {
#if UNITY_ANDROID
           

#elif UNITY_PC || UNITY_EDITOR || UNITY_WEBGL || UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
        pc();
#endif

    }

    public void pc()
    {
        gameObject.SetActive(true);

        AndroOPc.text ="Oprima Boton de espacio";
        image.gameObject.SetActive(false);
        cofre.gameObject.SetActive(true);
        btnEspada.gameObject.SetActive(false);
    }
}
