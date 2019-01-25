using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    protected Rigidbody2D rigidbody;

    protected float movementSpeed;
    protected float attackPower;

    protected int direction;

    public void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        rigidbody.velocity = (Vector2.right * direction * movementSpeed) + (Vector2.up * rigidbody.velocity.y);
    }

    public void SetDirection(int d)
    {
        direction = d;
    }
}
