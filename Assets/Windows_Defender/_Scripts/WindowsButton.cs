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
    private void OnCollisionEnter(Collision Col)
    {
        if (Col.gameObject.tag == "Enemy")
        {
            health--;
            Destroy(Col.gameObject);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
