using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitTutorialButton : MonoBehaviour
{
    private void OnMouseDown()
    {
        // Load main scene.
        SceneManager.LoadScene(0);
    }
}
