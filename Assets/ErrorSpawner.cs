using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorSpawner : MonoBehaviour
{
    public GameObject[] Errors;
    public GameObject WindowsHomeButton;
    public GameObject ShutDownWindow;
    int chooseError;
    int posx;
    int posy;
    Vector3 chooselocation;
    int chooseamountoferrors;
    float timer;
    int errorcreated;
    int i;
    public AudioClip ErrorSound;
    // Start is called before the first frame update
    void Start()
    {

    }

    
    // It Just works
    void Update()
    {
        timer += Time.deltaTime;
        if (WindowsHomeButton.GetComponent<WindowsButton>().health <= 0)
        {
            print("kill meh");
            chooseamountoferrors = Random.Range(5,30);         
                //  for (int y = 0;y<Errors.Length;y++)
                if (timer > 0.2f&&errorcreated<chooseamountoferrors)
                {
                    posx = Random.Range(-6, 6);
                    posy = Random.Range(-4, 5);
                    chooselocation = new Vector3(posx, posy, -1);
                    chooseError = Random.Range(0, Errors.Length);
                    Instantiate(Errors[chooseError], chooselocation, Quaternion.identity);
                     errorcreated++;
                    timer = 0f;
                GetComponent<AudioSource>().PlayOneShot(ErrorSound);
                }
                if (errorcreated>chooseamountoferrors&&timer > 1f)
            {
                ShutDownWindow.SetActive(true);
            }
        }
    }
    


}
