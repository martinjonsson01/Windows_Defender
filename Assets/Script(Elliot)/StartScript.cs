using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScript : MonoBehaviour
{
    public List<GameObject> buttons;
    List<int> valuta;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(closestButton() < 0.5f)
            {
                print("nogger");
                //gör skit här
            }
        }

       
    }

    float closestButton()
    {
       
        if(buttons.Count >= 0)
        {
            GameObject oldb = buttons[0];
            float oldl = 10000;

            for (int i = 0; i < buttons.Count; i++)
            {
                if(getLeanght(oldb,buttons[i]) <  oldl)
                {
                    oldl = getLeanght(oldb,buttons[i]);
                }
            }
            return oldl;
        }

        return 10;
    }

    float getLeanght(GameObject ga1,GameObject ga2)
    {
        float leanght = Vector3.Distance(ga1.transform.position, ga2.transform.position);

        return leanght;
    }

    //bool onButton(int i)
    //{
    //    if (i >= 0)
    //    {
    //        float leanght = Vector3.Distance(buttons[i].transform.position, transform.position);

    //        if (leanght < 0.5f)
    //        {
    //            return true;
    //        }
    //    }
    //    return false;

    //}
}
