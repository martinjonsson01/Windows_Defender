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
        TextPro = GetComponent<TextMeshPro>();
        StartCoroutine(ShowText());
    }

    // Update is called once per frame
    void Update()
    {
        /*
        transform.position = new Vector3(
            -transform.parent.parent.Find("Background").GetComponent<SpriteRenderer>().size.x / 4,
            transform.parent.parent.Find("Background").GetComponent<SpriteRenderer>().size.y / 4,
            0
            );
            */

        GetComponent<RectTransform>().sizeDelta = new Vector3(
            transform.parent.parent.GetComponent<Window>().Size.x - 1,
            transform.parent.parent.GetComponent<Window>().Size.y - 1,
            0
            );
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
