using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RollingTextScript : MonoBehaviour
{
    public float delay = 0.1f;
    public string fullText;
    private string currentText = "";
    TextMeshPro TextPro;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MeshRenderer>().sortingLayerName = "TextLayer";
        GetComponent<MeshRenderer>().sortingOrder = -10;

        TextPro = GetComponent<TextMeshPro>();
        StartCoroutine(ShowText());
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = transform.parent.position + transform.parent.GetComponent<SpriteRenderer>().sprite.bounds.size / 4;
    }

    IEnumerator ShowText()
    {
        for (int i = 0; i <= fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i);
            TextPro.text = currentText;
            yield return new WaitForSeconds(delay);
        }

        yield return new WaitForSeconds(1);
    }
}
