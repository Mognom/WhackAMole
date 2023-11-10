using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour {
    [SerializeField] private EventChannel playerHitEventChannel;
    [SerializeField] private EventChannel gameOverEventChannel;

    [SerializeField] private Image[] hearts;

    [SerializeField] private ScreenShake cameraShake;
    private int currentHealth;
    private void Awake() {
        playerHitEventChannel.channel += OnPlayerHit;
        currentHealth = hearts.Length;
    }

    private void OnPlayerHit() {
        cameraShake.StartShake();
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
