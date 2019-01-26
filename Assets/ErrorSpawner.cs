using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorSpawner : MonoBehaviour
{
    public GameObject[] Errors;
    public GameObject WindowsHomeButton;
    public GameObject ShutDownWindow;
    Vector3 chooselocation;
    int chooseamountoferrors;
    float timer;
    int errorcreated;
    int i;  
    int randomdeathscreen;
    int chooseError;
    int posx;
    int posy;
    public AudioClip ErrorSound;
    float timetowait = 1f;
    float layerdepth= -1;
    // Start is called before the first frame update
    void Start()
    {
        randomdeathscreen = (int)Random.Range(1, 3);
        
    }

    
    // It Just works
    void Update()
    {
        if (randomdeathscreen == 1)
        {
            ErrorDeathScreen();
        }
        if (randomdeathscreen == 2)
        {
            ErrorDeathScreen2();
        }
        
    }

    private void ErrorDeathScreen()
    {
        timer += Time.deltaTime;
        if (WindowsHomeButton.GetComponent<WindowsButton>().health <= 0)
        {
            print("kill meh");
            chooseamountoferrors = Random.Range(5, 30);
            //  for (int y = 0;y<Errors.Length;y++)
            if (timer > timetowait && errorcreated < chooseamountoferrors)
            {
                timetowait -= 0.1f;
                
                posx = Random.Range(-6, 6);
                posy = Random.Range(-4, 5);
                chooselocation = new Vector3(posx, posy, layerdepth);
                layerdepth -= 0.1f;
                chooseError = Random.Range(0, Errors.Length);
                GameObject temp =Instantiate(Errors[chooseError], chooselocation, Quaternion.identity);
                temp.GetComponent<Rigidbody2D>().gravityScale = 0;
                temp.GetComponent<BoxCollider2D>().enabled = false;
                errorcreated++;
                timer = 0f;
                GetComponent<AudioSource>().PlayOneShot(ErrorSound);
            }
            if (errorcreated > chooseamountoferrors && timer > 1f)
            {
                ShutDownWindow.SetActive(true);
            }
        }
        

    }
    void ErrorDeathScreen2()
    {
        timer += Time.deltaTime;
        if (WindowsHomeButton.GetComponent<WindowsButton>().health <= 0)
        {
            print("kill meh");
            chooseamountoferrors = Random.Range(5, 30);
            //  for (int y = 0;y<Errors.Length;y++)
            if (timer > timetowait && errorcreated < chooseamountoferrors)
            {
                timetowait -= 0.1f;

                posx = Random.Range(-6, 6);
                posy = Random.Range(6, 12);
                chooselocation = new Vector3(posx, posy, -1);
                chooseError = Random.Range(0, Errors.Length);
                GameObject temp = Instantiate(Errors[chooseError], chooselocation, Quaternion.identity);
                errorcreated++;
                temp.GetComponent<Rigidbody2D>().gravityScale = 1;
                temp.GetComponent<BoxCollider2D>().enabled = true;
                timer = 0f;
                GetComponent<AudioSource>().PlayOneShot(ErrorSound);
            }
            if (errorcreated > chooseamountoferrors && timer > 1f)
            {
                ShutDownWindow.SetActive(true);
            }
        }
    }
}
