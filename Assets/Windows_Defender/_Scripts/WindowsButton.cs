using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowsButton : MonoBehaviour
{
    public int health = 20;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            health--;
            Destroy(collision.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
