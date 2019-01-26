using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Standard_Enemy_Script : MonoBehaviour
{
    Rigidbody2D myBody;
    public float speed;
    Transform myTrans;
    float myWidth;
    int ChangeSpeed = 2;
    

    


    // Start is called before the first frame update
    void Start()
    {
        myTrans = this.transform;
        myBody = this.GetComponent<Rigidbody2D>();
        myWidth = this.GetComponent<SpriteRenderer>().bounds.extents.x;
        int slump = Random.Range(0, 2);
        // start speeden för standard enenemy som slumpas mellan 1 och 2  
        if (slump == 1)
        {
            speed = ChangeSpeed;
        }
        else 
        {
            speed = -ChangeSpeed;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 lineCastPos = myTrans.position - myTrans.right * myWidth;
        

        //går alltid framåt 
        Vector2 myVel = myBody.velocity;
        myVel.x = speed;
        myBody.velocity = myVel;

       
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Wall")
        {    // ändrar speeden till tvärtom
            if (speed == ChangeSpeed)
            {
                speed = -ChangeSpeed;
            }
           else if (speed == -ChangeSpeed)
            {
                speed = ChangeSpeed;
            }
        }
        if (other.tag == "Lose")
        {
            Destroy(gameObject);
        }
        if (other.tag == "Enemy")
        {

        }




    }
}
