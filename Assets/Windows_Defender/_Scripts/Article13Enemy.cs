﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class Article13Enemy : Enemy
{
    new void Start()
    {
        base.Start();

        attackPower = 20;
        movementSpeed = 3;

        landingShake = 0.5f;

        canFlipSprite = false;
        SetDirection(1);

        ChanceForGlitchComponent(50);
    }
}
