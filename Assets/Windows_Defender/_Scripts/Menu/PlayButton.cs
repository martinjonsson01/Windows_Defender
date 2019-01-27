using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButton : MonoBehaviour
{
    private MainWindow _mainWindow;
    
    // Start is called before the first frame update
    void Start()
    {
        _mainWindow = GameObject.Find("MainWindow").GetComponent<MainWindow>();
    }

    private void OnMouseDown()
    {
        startWaveScript.start = true;
        _mainWindow.PlayPressed();
    }
}
