using System.Collections;
using UnityEngine;

public class ScreenShake : MonoBehaviour {
    [SerializeField] private AnimationCurve shakeIntensity;
    [SerializeField] private float shakeDuration;
    private float currentShakeTime;


    public void StartShake() {
        StartCoroutine(Shake());
    }


    private IEnumerator Shake() {
        Vector3 p0 = transform.position;
        currentShakeTime = 0;
        while (currentShakeTime < shakeDuration) {
            currentShakeTime += Time.deltaTime;
            float intensity = shakeIntensity.Evaluate(currentShakeTime / shakeDuration);
            transform.position = p0 + Random.insideUnitSphere * intensity;
            yield return null;
        }
        transform.position = p0;
    }
}
