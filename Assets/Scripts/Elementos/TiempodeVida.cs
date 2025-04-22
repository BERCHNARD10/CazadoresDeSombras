using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiempodeVida : MonoBehaviour
{
    [SerializeField] private float tiempodeVida;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, tiempodeVida);
    }

}
