using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YoutubeWindow : Window
{
    public override float GetMovementSpeedCoefficient
    {
        get => AttributeActive ? 0 : 1;
    }
}
