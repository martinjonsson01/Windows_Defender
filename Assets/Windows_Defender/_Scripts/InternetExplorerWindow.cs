using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InternetExplorerWindow : Window
{
    public Sprite chromeSprite;

    public override float GetMovementSpeedCoefficient
    {
        get => AttributeActive ? base.GetMovementSpeedCoefficient : 1;
        set => base.GetMovementSpeedCoefficient = value;
    }

    float normalSlowdownSpeed;
    float chromeSpeedup;

    new void Start()
    {
        base.Start();

        normalSlowdownSpeed = 0.5f;
        chromeSpeedup = 2;

        IsChrome(false);
    }

    public void IsChrome(bool b)
    {
        if (b)
        {
            GetMovementSpeedCoefficient = chromeSpeedup;

            
            transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = chromeSprite;
        }
        else
            GetMovementSpeedCoefficient = normalSlowdownSpeed;
    }
}
