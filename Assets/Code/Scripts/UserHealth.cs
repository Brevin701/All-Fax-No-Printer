using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UserHealth : MonoBehaviour
{
    [Header("Events")]
    public static UnityEvent onEnemyDestroy = new UnityEvent();

    public static int health;
    public int maxHealth = 100;
    public Slider slider;
    public GameObject gameOver;

    private void Start()
    {
        health = maxHealth;
        slider.maxValue = maxHealth;
    }

    private void Update()
    {
        slider.value = health;
        if (health <= 0)
        {
            Time.timeScale = 0f;
            gameOver.SetActive(true);
            
        }
    }
}
