using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundScript : MonoBehaviour
{
    public GameObject WindowsHome;
    public AudioClip[] DeathSounds;
    public AudioClip[] StartUpSounds;
       int randomSound;
    bool playdeath;
    // Start is called before the first frame update
    private void Start()
    {
        PlayStartSound();
        playdeath = true;
    }
    private void Update()
    {
        if (WindowsHome == null&&playdeath == true)
        {
            PlayDeathSound();
            playdeath = false;
        }
    }

    public void PlayDeathSound()
    {
        randomSound = Random.Range(0,DeathSounds.Length);
        GetComponent<AudioSource>().PlayOneShot(DeathSounds[randomSound]);
    }
    public void PlayStartSound()
    {
        randomSound = Random.Range(0, StartUpSounds.Length);
        GetComponent<AudioSource>().PlayOneShot(StartUpSounds[randomSound]);
    }
}
