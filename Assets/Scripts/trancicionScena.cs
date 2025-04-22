using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class trancicionScena : MonoBehaviour
{
    private Animator animator;

    [SerializeField] private AnimationClip animacionFinal;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public IEnumerator CambiarScena()
    {
        animator.SetTrigger("iniciar");

        yield return new WaitForSeconds(animacionFinal.length);

        SceneManager.LoadScene("LevelSelectionScene");
    }

}
