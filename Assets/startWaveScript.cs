using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class startWaveScript : MonoBehaviour
{
    public static bool start;

    Vector3 maxScale;
    Vector3 minScale;
    Vector3 scaleIncreese;

    void Start()
    {
        scaleIncreese = new Vector3(0.15f, 0.15f, 0) * Time.deltaTime;
        maxScale = new Vector3(0.4f, 0.4f, 0);
        minScale = new Vector3(0.2f, 0.2f, 0);
    }

    void OnMouseDown()
    {
        EnemySpawner.nextWave = true;
        start = false;
    }

    void bounce()
    {
        transform.localScale += scaleIncreese;
    }

    void Update()
    {
        if (!EnemySpawner.canSpawn)
            start = true;
        else
            start = false;
            

        if (start)
        {
            if (transform.localScale.x >= maxScale.x)
                scaleIncreese *= -1;

            if (transform.localScale.x <= minScale.x)
                scaleIncreese *= -1;

            bounce();
        }
        else
        {
            transform.localScale = new Vector3(0.3f, 0.3f, 0);
        }
            
    }
}
