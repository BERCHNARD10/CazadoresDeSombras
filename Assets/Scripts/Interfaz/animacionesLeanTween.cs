using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class animacionesLeanTween : MonoBehaviour
{
    public void animacionLogoHome(RectTransform logo)
    {
        LeanTween.moveY(logo, 90, 2f).setLoopPingPong();
    }

     public void animacionBotones(GameObject boton, float tamañoRebote, float duracionAnimacion, float tiempoRetraso)
    {
        LeanTween.scale(boton.GetComponent<RectTransform>(), Vector3.one * tamañoRebote, duracionAnimacion)
        .setLoopType(LeanTweenType.pingPong).setDelay(tiempoRetraso);
    }

    public void DesvanecimientoImagen(GameObject image)
    {
        LeanTween.alphaCanvas(image.GetComponent<CanvasGroup>(), 0f, 1f);
    }

    public void mostrarMenuNormal(GameObject backgroundUI, GameObject Menu)
    {
        LeanTween.scale(Menu.GetComponent<RectTransform>(), new Vector3(1, 1, 1), 0.5f).setDelay(0.5f).setEase(LeanTweenType.easeOutBack);
        LeanTween.alpha(backgroundUI.GetComponent<RectTransform>(), 0.5f, 1f);
    }

    public void mostrarMenuConPausa(GameObject backgroundUI, GameObject Menu)
    {
        LeanTween.scale(Menu.GetComponent<RectTransform>(), new Vector3(1, 1, 1), 0.5f).setDelay(0.5f).setEase(LeanTweenType.easeOutBack)
        .setOnComplete(() => { Time.timeScale = 0f; });
        LeanTween.alpha(backgroundUI.GetComponent<RectTransform>(), 0.5f, 1f);
    }

    public void ocultarMenu(GameObject backgroundUI, GameObject Menu)
    {
        LeanTween.scale(Menu.GetComponent<RectTransform>(), new Vector3(0, 0, 0), 0.5f);
        LeanTween.alpha(backgroundUI.GetComponent<RectTransform>(), 0f, 0.5f);
    }

    public void AnimarEstrella(GameObject estrella)
    {
        LeanTween.scale(estrella, Vector3.one, 0.5f).setEase(LeanTweenType.easeOutBounce);
    }

    private void OnDisable()
    {
        // Cancela todos los tweens en este GameObject
        LeanTween.cancel(gameObject);
    }

}
