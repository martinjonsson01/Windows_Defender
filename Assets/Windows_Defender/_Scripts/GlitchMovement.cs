using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlitchMovement : MonoBehaviour
{
    public GameObject glitchParticleEffect;
    public Color glitchEffectColor;

    Enemy enemy;

    Rigidbody2D rigidbody;

    float glitchTimer;
    float currentRange;

    void Start()
    {
        enemy = GetComponent<Enemy>();
        rigidbody = GetComponent<Rigidbody2D>();

        NewTime();
    }

    void Update()
    {
        glitchTimer -= Time.deltaTime;

        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.right * enemy.GetDirection(), currentRange * enemy.GetMovementScale());

        if(glitchTimer <= 0 && !RaycastContainsTag(hits, WindowTags.ALLTAGS) && 
            RaycastIsInside((Vector2) transform.position + Vector2.right * enemy.GetDirection() * currentRange * enemy.GetMovementScale()))
        {
            rigidbody.MovePosition(
                transform.position + Vector3.right * enemy.GetDirection() * currentRange
                );

            GameObject effect = Instantiate(glitchParticleEffect, transform.position, glitchParticleEffect.transform.rotation);
            Sprite s = GetComponent<SpriteRenderer>().sprite;
            ParticleSystem.MainModule psm = effect.GetComponent<ParticleSystem>().main;
            psm.startColor = Color.magenta;
            Destroy(effect, 1);

            NewTime();
        }
    }

    bool RaycastIsInside(Vector2 newPos)
    {
        Vector3 screenCoords = Camera.main.WorldToViewportPoint(newPos);
        return screenCoords.x >= 0 && screenCoords.x <= 1;
    }

    bool RaycastContainsTag(RaycastHit2D[] hits, string[] tags)
    {
        for (int i = 0; i < hits.Length; i++)
            for(int j = 0; j < tags.Length; j++)
                if (hits[i].collider.gameObject.tag == tags[j])
                    return true;

        return false;
    }

    void NewTime()
    {
        glitchTimer = Random.Range(2, 5);
        currentRange = Random.Range(2, 4);
    }
}
