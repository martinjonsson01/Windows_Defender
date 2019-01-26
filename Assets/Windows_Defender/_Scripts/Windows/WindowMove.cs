using UnityEngine;

public class WindowMove : MonoBehaviour
{
    private Camera _mainCam;

    private Vector3 _offset;
    private Vector3 _screenPoint;

    private Window _window;

    // Start is called before the first frame update
    private void Start()
    {
        _mainCam = Camera.main;
        //_window = transform.parent.GetComponent<Window>();
        _window = GetComponent<Window>();
    }

    // Update is called once per frame
    private void Update()
    {

    }

    private void OnMouseDown()
    {
        _screenPoint = _mainCam.WorldToScreenPoint(transform.position);
        var mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _screenPoint.z);
        //_offset = transform.parent.position - _mainCam.ScreenToWorldPoint(mousePos);
        _offset = transform.position - _mainCam.ScreenToWorldPoint(mousePos);
    }

    private void OnMouseDrag()
    {
        var curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _screenPoint.z);
        Vector3 curPosition = _mainCam.ScreenToWorldPoint(curScreenPoint) + _offset;

        _window.SetPosition(curPosition);
    }
}
