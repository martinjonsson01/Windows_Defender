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
            GameObject oldButton = buttons[0];
            float oldLength = 10000;

            for (int i = 0; i < buttons.Count; i++)
            {
                if(getLength(oldButton,buttons[i]) <  oldLength)
                {
                    oldLength = getLength(oldButton,buttons[i]);
                }
            }
            return oldLength;
        }

        return 10;
    }

    float getLength(GameObject ga1,GameObject ga2)
    {
        float length = Vector3.Distance(ga1.transform.position, ga2.transform.position);

        return length;
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
