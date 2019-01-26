using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    List<GameObject> currentCollidingWindows;

    protected Rigidbody2D rigidbody;

    protected float movementSpeed;
    protected float movementScale;
    protected float attackPower;

    protected float landingShake;

    protected int direction;

    protected bool canFlipSprite = true;

    CameraHandler cam;

    Sprite sprite;

    public void Start()
    {
        // Håller koll på alla fönster som fienden kolliderar med just nu
        currentCollidingWindows = new List<GameObject>();

        // Fiender ska inte kunna kollidera med varandra
        Physics2D.IgnoreLayerCollision(8, 8, true);

        // Se till att fiender inte kan rotera p.g.a fysiken
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.freezeRotation = true;

        cam = Camera.main.GetComponent<CameraHandler>();

        movementScale = 1;

        // Finns en möjlighet att fienden blir "buggig"
        if (Random.Range(0, 100) > 0)
            gameObject.AddComponent<GlitchMovement>();

        sprite = GetComponent<SpriteRenderer>().sprite;
    }

    public void Update()
    {
        // Flytta fienden åt sidorna, samtidigt som den behåller gravitationen
        rigidbody.velocity = (Vector2.right * direction * movementSpeed * movementScale) + (Vector2.up * rigidbody.velocity.y);

        // Se till att fienden inte går utanför skärmen
        Vector3 leftSide = Camera.main.WorldToViewportPoint(
            transform.position - Vector3.right * (sprite.rect.width / sprite.pixelsPerUnit) / 2
            );
        Vector3 rightSide = Camera.main.WorldToViewportPoint(
            transform.position + Vector3.right * (sprite.rect.width / sprite.pixelsPerUnit) / 2
            );

        if (leftSide.x < 0 || rightSide.x > 1)
        {
            SetDirection((int) -Mathf.Sign(transform.position.x));
        }
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
        string t = other.gameObject.tag;
        if (t == WindowTags.TAG1 || t == WindowTags.TAG2 || t == WindowTags.TAG3)
        {
            float windowTopY = other.gameObject.GetComponent<Window>().GetTop();
            float enemyBottonY = transform.position.y - transform.localScale.y * 0.9f;

            // Colliderar ovanpå, eller på sidan av fönstret
            if(windowTopY > enemyBottonY)
            {
                SetDirection( 
                    (int) -Mathf.Sign(other.transform.position.x - transform.position.x) 
                    );
            }
            else
            {
                cam.Shake(landingShake);

                movementScale = other.gameObject.GetComponent<Window>().GetMovementSpeedCoefficient;
            }


            currentCollidingWindows.Add(other.gameObject);
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        currentCollidingWindows.Remove(collision.gameObject);

        if (currentCollidingWindows.Count == 0)
            movementScale = 1;
    }

    public void SetCanFlip(bool flip)
    {
        canFlipSprite = flip;
    }

    public void Kill()
    {
        Destroy(this.gameObject);
    }

    public int GetDirection() { return direction; }
}

public class WindowTags
{
    public static readonly string TAG1 = "Window";
    public static readonly string TAG2 = "Foreground Window";
    public static readonly string TAG3 = "Background Window";
    public static readonly string[] ALLTAGS = new string[] { TAG1, TAG2, TAG3 };
}