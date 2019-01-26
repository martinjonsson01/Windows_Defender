using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusLegs : MonoBehaviour
{
    RectTransform parentRect;

    void Start()
    {
        parentRect = transform.parent.gameObject.GetComponent<RectTransform>();
    }

    void Update()
    {
        transform.localScale = new Vector3(parentRect.rect.width / 4, parentRect.rect.height / 4);
    }
}
