using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class PlayerNeeds : MonoBehaviour
{
    public float maxHunger = 100f;
    public float maxSleepiness = 100f;
    public float maxHealth = 50;

    public float hungerRate = 1f; // Adjust as drainrate
    public float sleepinessRate = 0.5f; // Adjust as drainrate

    private float currentHunger;
    private float currentSleepiness;
    private float currentHealth;

    void Start()
    {
        // Initialize needs to their maximum values
        currentHunger = maxHunger;
        currentSleepiness = maxSleepiness;
        currentHealth = maxHealth;
    }

    //UI
    [SerializeField] public TextMeshProUGUI hungerText;
    public TextMeshProUGUI sleepinessText;
    public TextMeshProUGUI healthText;

    void Update()
    {
        // Drain hunger and sleepiness over time
        DrainNeedsOverTime();

        // Check if player is alive
        if (currentHealth <= 0)
        {
            // Implement game over or respawn logic here
            Debug.Log("Game Over!");
        }
        // Update the UI text with the current values
        hungerText.text = currentHunger.ToString("0");
        sleepinessText.text = currentSleepiness.ToString("0");
        healthText.text =  currentHealth.ToString("0");

    }

    void DrainNeedsOverTime()
    {
        // Drain hunger and sleepiness over time
        currentHunger -= hungerRate * Time.deltaTime;
        currentSleepiness -= sleepinessRate * Time.deltaTime;

        // Ensure needs don't go below zero
        currentHunger = Mathf.Clamp(currentHunger, 0f, maxHunger);
        currentSleepiness = Mathf.Clamp(currentSleepiness, 0f, maxSleepiness);

        // If hunger is zero, start draining health
        if (currentHunger <= 0)
        {
            DrainHealthOverTime();
        }
    }

    void DrainHealthOverTime()
    {
        // Implement health draining logic when hunger is zero
        currentHealth -= Time.deltaTime;

        // Ensure health doesn't go below zero
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
    }

}
