using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Window : MonoBehaviour
{
    protected const int HANDLE_HEIGHT = 1;

    protected SpriteRenderer _backgroundSpriteRenderer;
    protected SpriteRenderer _handleSpriteRenderer;

    protected BoxCollider2D _windowCollider;
    protected BoxCollider2D _handleCollider;

    protected SortingGroup _sortingGroup;

    /// <summary>
    /// Whether or not the attribute of this window is active or not.
    /// </summary>
    public bool AttributeActive = true;

    /// <summary>
    /// Gets the movement speed coefficient of this window.
    /// </summary>
    /// <returns>The movement speed coefficient.</returns>
    public virtual float GetMovementSpeedCoefficient => 1.0f;

    // Start is called before the first frame update
    public virtual void Start()
    {
        _backgroundSpriteRenderer = transform.Find("Background").GetComponent<SpriteRenderer>();
        _handleSpriteRenderer = transform.Find("Handle").GetComponent<SpriteRenderer>();
        _windowCollider = transform.GetComponent<BoxCollider2D>();
        _handleCollider = transform.Find("Handle").GetComponent<BoxCollider2D>();
        _sortingGroup = GetComponent<SortingGroup>();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }

    public virtual void OnMouseDown()
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
    public virtual float GetTop()
    {
        var halfHeight = _backgroundSpriteRenderer.size.y / 2.0f;
        return transform.position.y + halfHeight;
    }

    /// <summary>
    /// Sets the position of the window in world units.
    /// </summary>
    /// <param name="pos">The new position of the window in world units.</param>
    public virtual void SetPosition(Vector3 pos)
    {
        transform.position = pos;
    }

    /// <summary>
    /// Brings this window to the front of the stack.
    /// </summary>
    public virtual void BringToFront()
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
    public virtual void SetSize(Vector3 windowSize, Vector3 windowStartPos, Vector2 offset, bool scaleFromCenter = false)
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
