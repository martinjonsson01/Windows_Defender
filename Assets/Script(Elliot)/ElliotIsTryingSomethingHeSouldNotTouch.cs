using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElliotIsTryingSomethingHeSouldNotTouch : MonoBehaviour
{
    public float speed = 1;
    public float distance = 1f;
    float dirX;
    float oldY;


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            dirX *= -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x,transform.position.y),new Vector2(dirX,0), distance);
        // Does the ray intersect any objects excluding the player layer

        if (hit.collider != null)
        {

            print(hit.collider.tag);
            //  Debug.DrawRay(transform.position, dir , Color.red);
            Debug.DrawLine(transform.position, new Vector3(dirX,0,0), Color.magenta, 5f, false);
            if (hit.collider.tag == "window")
            {
                print("no");
                dirX *= -1;
            }
          
          
        }

        if ((oldY - transform.position.y > 0.1f))
        {
            dirX = -1;
        }

        transform.position += new Vector3(dirX, 0, 0) * speed * Time.deltaTime;

        oldY = transform.position.y;
    }

}
