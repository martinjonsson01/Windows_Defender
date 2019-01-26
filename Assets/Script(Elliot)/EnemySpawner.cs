using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> EnemyList; // dem olifa slags finderna vi har
   
    public float increases = 1.75f; // hur många enemys det ökar per wave
    int AmountOfEnemys = 4; // hur många finder det är
    public int WhatWave; // vilken Wave den är på
    public float timeBeforeSpawn; // hur lång tid det dröjer innan nästa finde kommer

    float timer; // bara en timer

    public static bool nextWave;

    int posX; 
    int randType; // att det blir en random slags finde
    public static int startAmount; // hur många bas finder det är

    void Start()
    {
        //nextWave = true;
        WhatWave = 0;
        startAmount = AmountOfEnemys;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime; // så att det är lika för alla 

       // print(AmountOfEnemys);

        if (AmountOfEnemys <= 0 && nextWave)
        {
            spawnWave();
            WhatWave++;
        }

        if (EnemyList.Count > 0 && timer > timeBeforeSpawn && AmountOfEnemys > 0) // så att det inte spammar finder och att det ska finnas finder programet kan spawna
        {
            spawnBoi();
        }
    }
    
    /// <summary>
    /// ofc it is an bad boiiiii
    /// </summary>
    void spawnBoi()
    {
       
            posX = Random.Range(-(int)Camera.main.orthographicSize, (int)Camera.main.orthographicSize);// så att finderna har en random position i xled
            Instantiate(randomEnemy(WhatWave), new Vector3(posX, (int)Camera.main.orthographicSize + 4, 0), Quaternion.identity);// skapar fienden lite över skärmen
         
            AmountOfEnemys--;

            timer = 0;
    }
    /// <summary>
    /// Så det blir en random fiende
    /// </summary>
    /// <returns>vilken fiende den ska spawna</returns>
    GameObject randomEnemy() 
    {
        randType = Random.Range(0, EnemyList.Count);

        return EnemyList[randType];
    }
    /// <summary>
    /// Så det blir en random fiende beronde på rundan
    /// </summary>
    /// <param name="waveNumber">vilket nummer det är</param>
    /// <returns>vilken fiende den ska skapa</returns>
    GameObject randomEnemy(int waveNumber)
    {
        if (waveNumber - 3 >= 0)
        {
            randType = 0;
        }

        else if (waveNumber -3 <= EnemyList.Count)
        {
            randType = Random.Range(0, waveNumber);
        }
       

        return EnemyList[randType];
       
    }

    void spawnWave()
    {
        AmountOfEnemys += Mathf.RoundToInt(startAmount * increases);
        startAmount = AmountOfEnemys;
        nextWave = false;
    }

}
