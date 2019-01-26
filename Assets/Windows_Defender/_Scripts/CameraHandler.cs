using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    Vector3 originalPosition;

    float shakeTimer;
    float maxShake;

    void Start()
    {
        originalPosition = transform.position;

        maxShake = 0.7f;

        Shake(3);
    }

    void Update()
    {
        shakeTimer -= Time.deltaTime;
        shakeTimer = Mathf.Max(shakeTimer, 0);


        float clampedShakeTimer = Mathf.Min(shakeTimer, maxShake);

        transform.position = originalPosition + 
            new Vector3(Random.Range(-clampedShakeTimer, clampedShakeTimer), Random.Range(-clampedShakeTimer, clampedShakeTimer), 0);
    }

    public void Shake(float time)
    {
        shakeTimer = time;
    }
}
