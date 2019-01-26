using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlitchMovement : MonoBehaviour
{
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

        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.right * enemy.GetDirection(), currentRange);

        if(glitchTimer <= 0 && !RaycastContainsTag(hits, WindowTags.ALLTAGS))
        {
            rigidbody.MovePosition(
                transform.position + Vector3.right * enemy.GetDirection() * currentRange
                );

            NewTime();
        }
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
