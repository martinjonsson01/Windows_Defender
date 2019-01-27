using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class ChromeEnemy : Enemy
{
    float rotationSpeed;
    float rotation;

    new void Start()
    {
        base.Start();

        attackPower = 20;
        movementSpeed = 8;
        rotationSpeed = (transform.localScale.x * 2) * Mathf.PI * movementSpeed * movementSpeed;

        canFlipSprite = false;
        SetDirection(1);

        ChanceForGlitchComponent(50);
    }

    new void Update()
    {
        base.Update();

        rotation += rotationSpeed * -direction * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, 0, rotation);
    }

    new void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);

        // Enable chrome in Internet explorer
        Window w = collision.gameObject.GetComponent<Window>();
        if (w != null && w.GetType().Equals(typeof(InternetExplorerWindow)))
            collision.gameObject.GetComponent<InternetExplorerWindow>().IsChrome(true);
    }
}
