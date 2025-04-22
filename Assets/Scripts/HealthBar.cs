using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public int maxHealth = 100;  // Vida máxima del jugador
    public int currentHealth;    // Vida actual del jugador

    public Image healthBarImage;  // Referencia al componente Image de la barra de vida

    private void Start()
    {
        currentHealth = maxHealth;  // Inicializar la vida actual con la máxima
        UpdateHealthBar();
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;  // Restar el daño recibido a la vida actual

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();  // Si la vida llega a cero o menos, el jugador muere
        }

        UpdateHealthBar();
    }
    public void sumarEnergia(int energia)
    {
        currentHealth += energia;  // subir energia actual
        Ethan p = FindObjectOfType<Ethan>();
        p.bandera = true;
        if (currentHealth >= 100)
        {
            currentHealth = 100;
            
        }

        UpdateHealthBar();
    }


    void Die()
    {
        // Aquí puedes poner la lógica para lo que sucede cuando el jugador muere
        // Por ejemplo, reiniciar nivel, mostrar pantalla de game over, etc.
        Debug.Log("Player died!");

        Ethan e = FindObjectOfType<Ethan>();
        e.bandera = false;
    }

    void UpdateHealthBar()
    {
        float healthPercentage = (float)currentHealth / maxHealth;
        healthBarImage.fillAmount = healthPercentage;
    }
}
