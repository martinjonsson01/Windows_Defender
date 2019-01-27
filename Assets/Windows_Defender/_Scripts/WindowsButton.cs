using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowsButton : MonoBehaviour
{
    [SerializeField]
    private AudioClip _damagedSound;

    private AudioSource _audioSource;

    public int health = 20;
    public static bool IsDead = false;
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            _audioSource.PlayOneShot(_damagedSound);
            health--;
            PappersKorgsScript.EnemysInStorage++;
            Destroy(collision.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(health == 0)
        {
            IsDead = true;
        }
    }
}
