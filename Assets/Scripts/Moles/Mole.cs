using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Mole : MonoBehaviour {
    private Animator animator;

    private readonly String showUpAnimation = "MoleShowUp";
    private readonly String attackAnimationTrigger = "AttackTrigger";
    private readonly String hitAnimationTrigger = "HitTrigger";

    private bool canBeHit = true;

    [SerializeField] private float idleTime;
    [SerializeField] private EventChannel moleHitChannel;
    [SerializeField] private EventChannel playerHitChannel;

    [SerializeField] private AudioClip playerHit;
    [SerializeField] private AudioClip[] damageClips;
    private AudioSource audioSource;

    public event Action OnLeave;

    private void Awake() {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable() {
        animator.enabled = true;
        canBeHit = true;
        animator.CrossFade(showUpAnimation, 0, 0);
        StartCoroutine(AttackWithDelay(idleTime));

    }

    private IEnumerator AttackWithDelay(float idleTime) {
        yield return new WaitForSeconds(idleTime);
        animator.SetTrigger(attackAnimationTrigger);
    }

    private void OnDisable() {
        animator.enabled = false;
        // Call mole leaving the game event
        OnLeave?.Invoke();
    }

    // EVENTS
    public void OnAttackAnimationEvent() {
        // Take one life
        canBeHit = false;
        audioSource.clip = damageClips[Random.Range(0, damageClips.Length - 1)];
        audioSource.Play();
        playerHitChannel.PostEvent();
    }

    public void OnLeaveAnimationEvent() {
        this.gameObject.SetActive(false);
    }

    private void OnMouseDown() {
        if (canBeHit) {
            audioSource.clip = playerHit;
            audioSource.Play();
            canBeHit = false;
            // cancel the scheduled attack
            this.StopAllCoroutines();
            // Hide the mole with an animation
            animator.SetTrigger(hitAnimationTrigger);
            moleHitChannel.PostEvent();
        }
    }
}
