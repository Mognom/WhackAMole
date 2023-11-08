using System;
using System.Collections;
using UnityEngine;

public class Mole : MonoBehaviour {
    private Animator animator;

    private readonly String showUpAnimation = "MoleShowUp";
    private readonly String attackAnimationTrigger = "AttackTrigger";
    private readonly String hitAnimationTrigger = "HitTrigger";

    private bool canBeHit = true;

    [SerializeField] private float idleTime;
    [SerializeField] private EventChannel moleHitChannel;
    [SerializeField] private EventChannel playerHitChannel;

    public event Action OnLeave;

    private void Awake() {
        animator = GetComponent<Animator>();
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
        playerHitChannel.PostEvent();
    }

    public void OnLeaveAnimationEvent() {
        // TODO pool this object
        this.gameObject.SetActive(false);
    }

    private void OnMouseDown() {
        if (canBeHit) {
            canBeHit = false;
            // cancel the scheduled attack
            this.StopAllCoroutines();
            // Hide the mole with an animation
            animator.SetTrigger(hitAnimationTrigger);
            moleHitChannel.PostEvent();
        }
    }
}
