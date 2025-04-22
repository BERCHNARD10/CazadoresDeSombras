using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameGuion : MonoBehaviour
{
    private bool scene0Shown = false;

    // Start is called before the first frame update
    void Start()
    {
        if (!scene0Shown)
        {
            // Mostrar la escena 0
            scene0Shown = true;
        }
        else
        {
            // Cambiar a otra escena, ya que la escena 0 ya se ha mostrado
            SceneManager.LoadScene("Game");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
