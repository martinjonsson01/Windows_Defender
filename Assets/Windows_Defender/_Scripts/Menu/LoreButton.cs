using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoreButton : MonoBehaviour
{
    [SerializeField]
    private GameObject _notepadWindow;

    private bool alreadySpawned;

    private void OnMouseDown()
    {
        if(!alreadySpawned)
        {
            Instantiate(_notepadWindow, Vector3.zero, Quaternion.identity);
            alreadySpawned = true;
        }
    }
}
