using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PappersKorgsScript : MonoBehaviour
{
    public int Storage = 10;
    public Sprite Full,Emty;
    public float TimerBeforerNewPos = 4;

    float time;
    float waveTime;
    public static int EnemysInStorage;
    int randx;

    Vector3 randPos; // papperskorgens nya position

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Destroy(other.gameObject);
            EnemysInStorage++;
        }
            
    }

    void Update()
    {
        time += Time.deltaTime;

        //print("enemy in storage " + EnemysInStorage);
        //print("enemy to spawn " + EnemySpawner.startAmount);



        if (EnemySpawner.startAmount > 0 && EnemysInStorage == EnemySpawner.startAmount) // så att den mängd fiender som spawnas ska också dö.
        {                                                // Btw glöm inte att göra så ifall en fiende går in i windos knappen ska EnemysInStorage -1.
            print("fel");
            this.GetComponent<SpriteRenderer>().sprite = Full;//Annars kommer inte nästa runda spawna
            waveTime += Time.deltaTime;

            if (waveTime > 4 && !WindowsButton.IsDead) // Det dröjer ca 4 sec mellan varje runda
            {

                EnemysInStorage = 0; //så att rundan börjar om
                //EnemySpawner.nextWave = true;
                this.GetComponent<SpriteRenderer>().sprite = Emty; // byter bild på sopptunnan(du rensar ju den mellan varje runda)
                waveTime = 0;
            }
        }
    

        if (newPos())
        {
            transform.position = randPos; 
        }
    }
    /// <summary>
    /// Kollar om papperskorgen ska få en ny position eller inte
    /// </summary>
    /// <returns>om papperksorgen ska få en ny position eller ej</returns>
    bool newPos()
    {
        if (time > TimerBeforerNewPos)
        {
           

            time = 0;
            randx = Random.Range(-(int)Camera.main.orthographicSize, (int)Camera.main.orthographicSize); // så att det blir en random position i Xled

            randPos = new Vector3(randx, -(int)Camera.main.orthographicSize + 1.3f, -1.1f); // respawnar sopptunnan

            return true;
        }
        else
            return false;
    }
}
