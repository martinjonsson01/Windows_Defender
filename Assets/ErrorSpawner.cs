using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class ErrorSpawner : MonoBehaviour
{
    public GameObject[] Errors;
    public GameObject WindowsHomeButton;
    public GameObject ShutDownWindow;
    public GameObject Patrick;
    private Vector3 chooselocation;
    private int chooseamountoferrors;
    private float timer;
    private int errorcreated;
    private int i;
    private int randomdeathscreen;
    private int chooseError;
    private int posx;
    private int posy;
    public AudioClip ErrorSound;
    private float timetowait = 1f;
    private float layerdepth = -1;
    private float liftpatricky = -4;

    // Start is called before the first frame update
    private void Start()
    {
        randomdeathscreen = Random.Range(1, 3);

    }


    // It Just works
    private void Update()
    {

        if (randomdeathscreen == 1)
        {
            ErrorDeathScreen();
        }
        if (randomdeathscreen == 2)
        {
            ErrorDeathScreenBonzo();
        }
        if (WindowsHomeButton.GetComponent<WindowsButton>().health == 2 && liftpatricky < -1)
        {
            Patrick.transform.position = new Vector3(Patrick.transform.position.x, liftpatricky, Patrick.transform.position.z);
            liftpatricky += Time.deltaTime;
        }
        if (WindowsHomeButton.GetComponent<WindowsButton>().health == 1 && liftpatricky < 0)
        {
            Patrick.transform.position = new Vector3(Patrick.transform.position.x, liftpatricky, Patrick.transform.position.z);
            liftpatricky += Time.deltaTime;
        }
        if (WindowsHomeButton.GetComponent<WindowsButton>().health == 0 && liftpatricky < 1.8)
        {
            Patrick.transform.position = new Vector3(Patrick.transform.position.x, liftpatricky, Patrick.transform.position.z);
            liftpatricky += Time.deltaTime;
        }
    }

    private void ErrorDeathScreen()
    {
        timer += Time.deltaTime;
        if (WindowsHomeButton.GetComponent<WindowsButton>().health <= 0)
        {
            //print("kill meh");
            chooseamountoferrors = Random.Range(5, 30);
            //  for (int y = 0;y<Errors.Length;y++)
            if (timer > timetowait && errorcreated < chooseamountoferrors)
            {
                timetowait -= 0.1f;

                posx = Random.Range(-6, 6);
                posy = Random.Range(-4, 5);
                chooselocation = new Vector3(posx, posy, layerdepth);
                layerdepth -= 0.1f;
                chooseError = Random.Range(0, Errors.Length);
                GameObject temp = Instantiate(Errors[chooseError], chooselocation, Quaternion.identity);
                temp.GetComponent<Rigidbody2D>().gravityScale = 0;
                temp.GetComponent<BoxCollider2D>().enabled = false;
                errorcreated++;
                timer = 0f;
                GetComponent<AudioSource>().PlayOneShot(ErrorSound);
            }
            if (errorcreated > chooseamountoferrors && timer > 1f)
            {
                ShutDownWindow.SetActive(true);
            }
        }


    }

    private void ErrorDeathScreen2()
    {
        timer += Time.deltaTime;
        if (WindowsHomeButton.GetComponent<WindowsButton>().health <= 0)
        {
            //print("kill meh");
            chooseamountoferrors = Random.Range(5, 30);
            //  for (int y = 0;y<Errors.Length;y++)
            if (timer > timetowait && errorcreated < chooseamountoferrors)
            {
                timetowait -= 0.1f;

                posx = Random.Range(-6, 6);
                posy = Random.Range(6, 12);
                chooselocation = new Vector3(posx, posy, -1);
                chooseError = Random.Range(0, Errors.Length);
                GameObject temp = Instantiate(Errors[chooseError], chooselocation, Quaternion.identity);
                errorcreated++;
                temp.GetComponent<Rigidbody2D>().gravityScale = 1;
                temp.GetComponent<BoxCollider2D>().enabled = true;
                timer = 0f;
                GetComponent<AudioSource>().PlayOneShot(ErrorSound);
            }
            if (errorcreated > chooseamountoferrors && timer > 1f)
            {
                ShutDownWindow.SetActive(true);
            }
        }
    }

    private void ErrorDeathScreenBonzo()
    {
        timer += Time.deltaTime;
        if (WindowsHomeButton.GetComponent<WindowsButton>().health <= 0)
        {
            //print("kill meh");
            chooseamountoferrors = Random.Range(30, 100);
            //  for (int y = 0;y<Errors.Length;y++)
            if (timer > timetowait && errorcreated < chooseamountoferrors)
            {
                timetowait -= 0.1f;

                posx = Random.Range(-6, 6);
                posy = Random.Range(6, 12);
                chooselocation = new Vector3(posx, posy, -1);
                chooseError = Random.Range(0, Errors.Length);
                GameObject temp = Instantiate(Errors[chooseError], chooselocation, Quaternion.identity);
                errorcreated++;
                temp.GetComponent<Rigidbody2D>().gravityScale = 1;
                temp.GetComponent<BoxCollider2D>().enabled = true;
                timer = 0f;
                GetComponent<AudioSource>().PlayOneShot(ErrorSound);
            }
            if (errorcreated > chooseamountoferrors && timer > 1f)
            {
                ShutDownWindow.SetActive(true);

                StartCoroutine(ShutDownComputer(5.0f));
            }
        }
    }

    /// <summary>
    /// Shuts down the player's computer after a specified amount of seconds.
    /// </summary>
    /// <param name="delay">The amount of seconds to delay before shutting down.</param>
    /// <returns></returns>
    private IEnumerator ShutDownComputer(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (Application.isEditor)
        {
            UnityEngine.Debug.LogWarning("!!! COMPUTER WOULD HAVE SHUT DOWN NOW IF NOT IN UNITY EDITOR !!!");
        }
        else
        {
            UnityEngine.Debug.Log("Shutting down computer...");
            Process.Start("shutdown", "/s /t 0");
        }

        yield break;
    }
}
