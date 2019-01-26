using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Article13Enemy : Enemy
{
    void Start()
    {
        base.Start();

        movementSpeed = 3;

        canFlipSprite = false;
        SetDirection(-1);
    }
}
