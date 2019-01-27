using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CensoredEnemy : MonoBehaviour
{
    public Color killParticleColor;
    public GameObject killParticleEffect;
    public GameObject glitchParticleEffect;
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
