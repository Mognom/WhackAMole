using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour {
    private int score;
    [SerializeField] private EventChannel playerHitEventChannel;
    [SerializeField] private EventChannel gameOverEventChannel;


    [SerializeField] private Image[] hearts;
    private int currentHealth;
    private void Awake() {
        playerHitEventChannel.channel += OnPlayerHit;
        currentHealth = hearts.Length;
    }



    private void OnPlayerHit() {

        Debug.Log("Auch");
        currentHealth -= 1;
        if (currentHealth >= 0) { 
            hearts[currentHealth].gameObject.SetActive(false);
        }
        if (currentHealth <= 0) {
            playerHitEventChannel.channel -= OnPlayerHit;
            gameOverEventChannel.PostEvent();
        }
    }
}
