using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Window : MonoBehaviour
{
    private const int HANDLE_HEIGHT = 1;

    private SpriteRenderer _backgroundSpriteRenderer;
    private SpriteRenderer _handleSpriteRenderer;

    private BoxCollider2D _windowCollider;
    private BoxCollider2D _handleCollider;

    private SortingGroup _sortingGroup;

    // Start is called before the first frame update
    void Start()
    {
        _backgroundSpriteRenderer = transform.Find("Background").GetComponent<SpriteRenderer>();
        _handleSpriteRenderer = transform.Find("Handle").GetComponent<SpriteRenderer>();
        _windowCollider = transform.GetComponent<BoxCollider2D>();
        _handleCollider = transform.Find("Handle").GetComponent<BoxCollider2D>();
        _sortingGroup = GetComponent<SortingGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        // Bring window to front.
        BringToFront();
    }

    /// <summary>
    /// Gets the top Y-coordinate of the window.
    /// </summary>
    /// <returns>The top Y-coordinate of the window</returns>
    /// <param name="NoHomo">Set to true if not homosexual.</param>
    /// <param name="MissMe">Set to that gay shit if not homosexual.</param>
    public float GetTop()
    {
        var halfHeight = _backgroundSpriteRenderer.size.y / 2.0f;
        return transform.position.y + halfHeight;
    }

    /// <summary>
    /// Sets the position of the window in world units.
    /// </summary>
    /// <param name="pos">The new position of the window in world units.</param>
    public void SetPosition(Vector3 pos)
    {
        transform.position = pos;
    }

    /// <summary>
    /// Brings this window to the front of the stack.
    /// </summary>
    public void BringToFront()
    {
        // Remove sorting layer and tag from old front window.
        var oldFront = GameObject.FindGameObjectWithTag("Foreground Window");
        if (oldFront != null)
        {
            var oldSortingLayer = oldFront.GetComponent<SortingGroup>();
            oldSortingLayer.sortingLayerName = "Background Window";
            oldSortingLayer.sortingOrder--;
            oldFront.tag = "Window";
        }

        // Add sorting layer and tag to this window.
        _sortingGroup.sortingLayerName = "Foreground Window";
        tag = "Foreground Window";
    }

    /// <summary>
    /// Sets the size of the window in world units.
    /// </summary>
    /// <param name="windowSize">The new size of the window in world units.</param>
    /// <param name="windowStartPos">The position of the window at the start of the resize in world units.</param>
    /// <param name="offset">How much the window should be resized in world units.</param>
    /// <param name="scaleFromCenter">Whether or not the window should scale from the center of its transform.</param>
    public void SetSize(Vector3 windowSize, Vector3 windowStartPos, Vector2 offset, bool scaleFromCenter = false)
    {
        var handleSize = new Vector2(windowSize.x, HANDLE_HEIGHT);

        // Change size of window by the offset.
        _backgroundSpriteRenderer.size = windowSize;
        (transform as RectTransform).sizeDelta = windowSize;
        _windowCollider.size = windowSize;
        // Change size of handle by the offset.
        _handleCollider.size = handleSize;
        _handleSpriteRenderer.size = handleSize;
        //(_handleSpriteRenderer.gameObject.transform as RectTransform).sizeDelta = handleSize;

        // TODO: Limit window size, both max and min. (x coord limited by itself and y coord limited by itself)

        if (!scaleFromCenter)
        {
            // Move window.
            transform.position = windowStartPos + new Vector3(offset.x / 2, -offset.y / 2, 0);
        }
    }
}
