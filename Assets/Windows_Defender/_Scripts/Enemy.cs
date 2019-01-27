using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    public GameObject killParticleEffect;
    public GameObject glitchParticleEffect;
    public Color killParticleColor = Color.red;

    List<GameObject> currentCollidingWindows;

    protected Rigidbody2D rigidbody;
    
    protected float movementSpeed;
    protected float movementScale;

    private float currentAttackPower;
    private float attackPowerScale;
    public float attackPower
    {
        get
        {
            return currentAttackPower * attackPowerScale;
        }
        set
        {
            currentAttackPower = value;
        }
    }

    float originalScale;

    protected float landingShake;

    protected int direction;

    protected bool canFlipSprite = true;

    CameraHandler cam;

    Sprite sprite;

    float time;

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
        attackPowerScale = 1;

        originalScale = transform.localScale.x;

        sprite = GetComponent<SpriteRenderer>().sprite;

        if(killParticleEffect == null)
        {
            killParticleEffect = GetComponent<CensoredEnemy>().killParticleEffect;
            killParticleColor = GetComponent<CensoredEnemy>().killParticleColor;
            glitchParticleEffect = GetComponent<CensoredEnemy>().glitchParticleEffect;
        }
    }

    public void Update()
    {
        // Flytta fienden åt sidorna, samtidigt som den behåller gravitationen
        rigidbody.velocity = (Vector2.right * direction * movementSpeed * movementScale) + (Vector2.up * rigidbody.velocity.y);

        InsideScreen();

        // Uppdatera movementscale
        if(currentCollidingWindows.Count > 0)
            movementScale = currentCollidingWindows[0].GetComponent<Window>().GetMovementSpeedCoefficient;
    }
    
    protected void InsideScreen()
    {
        // Se till att fienden inte går utanför skärmen
        Vector3 leftSide = Camera.main.WorldToViewportPoint(
            transform.position - Vector3.right * (sprite.rect.width / sprite.pixelsPerUnit) / 2
            );
        Vector3 rightSide = Camera.main.WorldToViewportPoint(
            transform.position + Vector3.right * (sprite.rect.width / sprite.pixelsPerUnit) / 2
            );

        if (leftSide.x < 0 || rightSide.x > 1)
        {
            SetDirectionToMiddle();
            attackPowerScale += Time.deltaTime * 2;
        }
        else
            attackPowerScale = 1;

        print(attackPowerScale);

        // "Lock":ar fienden innanför skärmen
        Vector3 tempPos = transform.position;
        Vector3 leftScreenEdge = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 rightScreenEdge = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0));

        tempPos.x = Mathf.Clamp(tempPos.x, leftScreenEdge.x-0.3f, rightScreenEdge.x+0.3f);

        transform.position = tempPos;
    }

    public void SetDirectionToMiddle()
    {
        SetDirection((int)-Mathf.Sign(transform.position.x));
    }

    protected void ChanceForGlitchComponent(float percent)
    {
        // Finns en möjlighet att fienden blir "buggig"
        if (Random.Range(0, 100) < percent)
        {
            gameObject.AddComponent<GlitchMovement>();
            GetComponent<GlitchMovement>().glitchParticleEffect = glitchParticleEffect;

            Color c = killParticleColor;
            c.a = 0.5f;
            GetComponent<GlitchMovement>().glitchEffectColor = c;
        }
    }

    public void SetDirection(int d)
    {
        direction = d;

        // Det är inte alla sprites som behöver flippas
        if (canFlipSprite)
        {
            Vector3 tempScale = transform.localScale;
            tempScale.x = direction * originalScale;
            transform.localScale = tempScale;
        }
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        string t = other.gameObject.tag;
        if (t == WindowTags.TAG1 || t == WindowTags.TAG2)
        {
            float windowTopY = other.gameObject.GetComponent<Window>().GetTop();
            float enemyBottonY = transform.position.y - transform.localScale.y * 0.2f;

            // Colliderar ovanpå, eller på sidan av fönstret
            if(windowTopY > enemyBottonY)
            {
                if (timer())
                {
                    other.gameObject.GetComponent<Window>().Durability -= attackPower;
                    time = 0;
                    print(other.gameObject.GetComponent<Window>().Durability);
                }

                SetDirection( 
                    (int) -Mathf.Sign(other.transform.position.x - transform.position.x) 
                    );
            }
            else
            {
                cam.Shake(landingShake);
            }


            currentCollidingWindows.Add(other.gameObject);
        }
    }

    bool timer()
    {
        time += Time.deltaTime;
        if (time > 0)
            return true;

        return false;
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
        GameObject effect = Instantiate(killParticleEffect, transform.position, Quaternion.identity);
        Sprite s = GetComponent<SpriteRenderer>().sprite;
        ParticleSystem.MainModule psm = effect.GetComponent<ParticleSystem>().main;
        psm.startColor = killParticleColor;
        Destroy(effect, 1);

        Destroy(this.gameObject);
    }

    public int GetDirection() { return direction; }
    public float GetMovementScale() { return movementScale; }
}

public class WindowTags
{
    public static readonly string TAG1 = "Window";
    public static readonly string TAG2 = "Foreground Window";
    public static readonly string[] ALLTAGS = new string[] { TAG1, TAG2 };
}