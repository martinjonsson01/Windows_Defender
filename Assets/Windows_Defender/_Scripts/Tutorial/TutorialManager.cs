using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    private MainWindow _mainWindow;

    // Start is called before the first frame update
    void Start()
    {
        _mainWindow = GameObject.Find("MainWindow").GetComponent<MainWindow>();
        StartCoroutine(DelayedResize(0.5f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private IEnumerator DelayedResize(float delay)
    {
        yield return new WaitForSeconds(delay);

        _mainWindow.PlayPressed();

        yield break;
    }
}
