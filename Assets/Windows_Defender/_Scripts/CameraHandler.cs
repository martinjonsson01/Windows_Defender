using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    Vector3 originalPosition;

    float shakeTimer;

    void Start()
    {
        originalPosition = transform.position;

        Shake(3);
    }

    void Update()
    {
        shakeTimer -= Time.deltaTime;
        shakeTimer = Mathf.Max(shakeTimer, 0);

        transform.position = originalPosition + 
            new Vector2(Random.Range(-shakeTimer, shakeTimer), Random.Range(-shakeTimer, shakeTimer));
    }

    public void Shake(float time)
    {
        shakeTimer = time;
    }
}
