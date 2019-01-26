using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorSpawner : MonoBehaviour
{
    public GameObject[] Errors;
    public GameObject WindowsHomeButton;
    int chooseError;
    int posx;
    int posy;
    Vector3 chooselocation;
    int chooseamountoferrors;
    bool Isdone = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    
    // It Just works
    void Update()
    {
        if (WindowsHomeButton.GetComponent<WindowsButton>().health <= 0&&Isdone == true)
        {
            print("kill meh");
            chooseamountoferrors = Random.Range(5,30);
            for (int i = 0; i<chooseamountoferrors;i++)
            {
                for (int y = 0;y<Errors.Length;y++)
                {
                    posx = Random.Range(-9, 9);
                    posy = Random.Range(-4,6);
                    chooselocation = new Vector3(posx, posy);
                    chooseError = Random.Range(0,Errors.Length);
                    Instantiate(Errors[chooseError],chooselocation,Quaternion.identity);
                }
            }
            Isdone = false;
        }
    }
    


}
