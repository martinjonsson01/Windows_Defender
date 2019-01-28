using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> EnemyList; // dem olifa slags finderna vi har

    public GameObject[] Windows = new GameObject[4];

    public float increases = 1.75f; // hur många enemys det ökar per wave
    int AmountOfEnemys = 0; // hur många finder det är
    public int WhatWave; // vilken Wave den är på
    public float timeBeforeSpawn; // hur lång tid det dröjer innan nästa finde kommer

    float timer; // bara en timer

    public static bool nextWave;

    int posX; 
    int randType; // att det blir en random slags finde

    public static bool canSpawn;

    public static int startAmount; // hur många bas finder det är

    void Start()
    {
        WhatWave = 0;
        startAmount = 3;
    }

    int enemyCountWhenWindowSpawned;

    // Update is called once per frame
    void Update()
    {
        print(WhatWave);

        timer += Time.deltaTime; // så att det är lika för alla 

        if (AmountOfEnemys <= 0 || (AmountOfEnemys == Mathf.FloorToInt(startAmount / 2) && enemyCountWhenWindowSpawned != AmountOfEnemys))
        {
            spawnWave();
            WhatWave++;

            if (WhatWave != 1)
            {
                // Spawn new window.
                var window = Instantiate(Windows[(WhatWave - 2) % Windows.Length]).GetComponent<Window>();
                enemyCountWhenWindowSpawned = GameObject.FindGameObjectsWithTag("Enemy").Length;
            }
        }

        if (EnemyList.Count > 0 && timer > timeBeforeSpawn && AmountOfEnemys > 0 && canSpawn) // så att det inte spammar finder och att det ska finnas finder programet kan spawna
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

        if (AmountOfEnemys == 0)
            canSpawn = false;
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
        /*
        if (waveNumber - 1 <= 0)
        {
            randType = 0;
        }

        else if (waveNumber -2 <= EnemyList.Count)
        {
            randType = Random.Range(0, waveNumber-2);
        }
        */
        randType = Random.Range(0, EnemyList.Count);

        return EnemyList[randType];
       
    }

    void spawnWave()
    {
        AmountOfEnemys += Mathf.RoundToInt(startAmount * increases);
        startAmount = AmountOfEnemys;
        canSpawn = true;
        nextWave = false;
    }

}
