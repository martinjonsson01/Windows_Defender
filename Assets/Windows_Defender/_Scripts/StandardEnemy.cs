using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class StandardEnemy : Enemy
{
    new void Start()
    {
        base.Start();

        movementSpeed = 5;

        SetDirection(-1);
    }
}
