using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Enemy : MonoBehaviour
{
    protected Rigidbody2D rigidbody;

    protected float movementSpeed;
    protected float attackPower;

    protected float landingShake;

    protected int direction;

    protected bool canFlipSprite = true;

    CameraHandler cam;

    public void Start()
    {
        // Fiender ska inte kunna kollidera med varandra
        Physics2D.IgnoreLayerCollision(8, 8, true);

        rigidbody = GetComponent<Rigidbody2D>();

        cam = Camera.main.GetComponent<CameraHandler>();
    }

    public void Update()
    {
        // Flytta fienden åt sidorna, samtidigt som den behåller gravitationen
        rigidbody.velocity = (Vector2.right * direction * movementSpeed) + (Vector2.up * rigidbody.velocity.y);
    }

    public void SetDirection(int d)
    {
        direction = d;

        // Det är inte alla sprites som behöver flippas
        if (canFlipSprite)
        {
            Vector3 tempScale = transform.localScale;
            tempScale.x = direction;
            transform.localScale = tempScale;
        }
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Window")
        {
            /*
            float windowY = other.GetComponent<Window>().

            if(windowY > transform.position.y)
            {
                SetDirection( 
                    (int) -Mathf.Sign(other.transform.position.x - transform.position.x) 
                    );
            }
            else
            {
                cam.Shake(landingShake);
            }
            */
        }
    }
}
