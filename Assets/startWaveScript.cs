using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class startWaveScript : MonoBehaviour
{
    void OnMouseDown()
    {
        EnemySpawner.nextWave = true;
       
    }

    void Update()
    {
        if (EnemySpawner.canSpawn)
        {
            this.GetComponent<SpriteRenderer>().material.color = Color.red;
        }

        else
        {
            this.GetComponent<SpriteRenderer>().material.color = Color.white;
        }

    }
}
