using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowResize : MonoBehaviour
{
    private Camera _mainCam;
    private SpriteRenderer _backgroundSpriteRenderer;
    
    private Vector3 _offset;
    private Vector3 _mouseStartPos;
    private Vector3 _windowStartPos;
    private Vector2 _windowStartSize;

    private Window _window;

    // Start is called before the first frame update
    void Start()
    {
        _mainCam = Camera.main;
        _window = transform.parent.GetComponent<Window>();
        _backgroundSpriteRenderer = transform.parent.Find("Background").GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        // Record the position of the mouse and the window size at the start of the dragging.
        _mouseStartPos = transform.position;
        _windowStartSize = _backgroundSpriteRenderer.size;
        _windowStartPos = transform.parent.position;
    }

    private void OnMouseDrag()
    {
        // Calculate how many world units mouse has moved.
        var currentMouseWorldPos = _mainCam.ScreenToWorldPoint(Input.mousePosition);
        var offsetX = currentMouseWorldPos.x - _mouseStartPos.x;
        var offsetY = _mouseStartPos.y - currentMouseWorldPos.y;
        
        // Calculate new window and handle size.
        var windowSize = new Vector2(
            _windowStartSize.x + offsetX, 
            _windowStartSize.y + offsetY
        );

        // Set size of window.
        _window.SetSize(windowSize, _windowStartPos, new Vector2(offsetX, offsetY));
    }
}
