using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CensoredEnemy : MonoBehaviour
{
    public GameObject virusArrowPrefab;
    public GameObject virusHoldingLegsPrefab;

    public Enemy[] enemies;
    
    void Start()
    {
        Enemy e = enemies[Random.Range(0, enemies.Length)];
        gameObject.AddComponent(e.GetType());
        //gameObject.AddComponent(typeof(VirusEnemy));

        GetComponent<Enemy>().SetCanFlip(false);
    }
}
