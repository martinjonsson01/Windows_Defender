using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Window : MonoBehaviour
{
    protected const int HANDLE_HEIGHT = 1;
    protected const int MIN_SIZE = 1;
    protected const int MAX_SIZE = 20;

    protected SpriteRenderer backgroundSpriteRenderer;
    protected SpriteRenderer handleSpriteRenderer;

    protected BoxCollider2D windowCollider;
    protected BoxCollider2D handleCollider;

    protected SortingGroup sortingGroup;

    private GameObject _resizeHandle;
    
    /// <summary>
    /// Whether or not the attribute of this window is active or not.
    /// </summary>
    public bool AttributeActive = true;
    
    /// <summary>
    /// Whether or not this window is resizable.
    /// </summary>
    public bool Resizable = false;

    /// <summary>
    /// The durability of this window.
    /// </summary>
    public float Durability = 200;

    /// <summary>
    /// Gets the movement speed coefficient of this window.
    /// </summary>
    /// <returns>The movement speed coefficient.</returns>
    public virtual float GetMovementSpeedCoefficient => 1.0f;

    public virtual Vector2 Size => (transform as RectTransform).sizeDelta;

    // Start is called before the first frame update
    public virtual void Start()
    {
        backgroundSpriteRenderer = transform.Find("Background").GetComponent<SpriteRenderer>();
        handleSpriteRenderer = transform.Find("Handle").GetComponent<SpriteRenderer>();
        windowCollider = transform.GetComponent<BoxCollider2D>();
        handleCollider = transform.Find("Handle").GetComponent<BoxCollider2D>();
        sortingGroup = GetComponent<SortingGroup>();
        _resizeHandle = transform.Find("ResizeHandle").gameObject;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        // Make resize handle hidden if window not resizable.
        if (_resizeHandle.activeSelf != Resizable)
            _resizeHandle.SetActive(Resizable);
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
        var halfHeight = backgroundSpriteRenderer.size.y / 2.0f;
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
        sortingGroup.sortingLayerName = "Foreground Window";
        tag = "Foreground Window";
    }

    /// <summary>
    /// Sets the size of the window in world units.
    /// </summary>
    /// <param name="windowSize">The new size of the window in world units.</param>
    /// <param name="windowStartPos">The position of the window at the start of the resize in world units.</param>
    /// <param name="offset">How much the window should be resized in world units.</param>
    /// <param name="scaleFromCenter">Whether or not the window should scale from the center of its transform.</param>
    public virtual void SetSize(Vector3 windowSize, Vector3 windowStartPos, Vector2 offset, bool scaleFromCenter = false, bool ignoreSizeLimits = false)
    {
        float sizeX;
        float sizeY;
        if (!ignoreSizeLimits)
        {
            // Limit window size, both max and min. (x coord limited by itself and y coord limited by itself)
            sizeX = Mathf.Min(Mathf.Max(windowSize.x, MIN_SIZE), MAX_SIZE);
            sizeY = Mathf.Min(Mathf.Max(windowSize.y, MIN_SIZE), MAX_SIZE);
        }
        else
        {
            sizeX = windowSize.x;
            sizeY = windowSize.y;
        }
        windowSize = new Vector3(sizeX, sizeY, 1);

        var handleSize = new Vector2(windowSize.x, HANDLE_HEIGHT);

        // Change size of window by the offset.
        backgroundSpriteRenderer.size = windowSize;
        (transform as RectTransform).sizeDelta = windowSize;
        windowCollider.size = windowSize;
        // Change size of handle by the offset.
        handleCollider.size = handleSize;
        handleSpriteRenderer.size = handleSize;

        // TODO: 

        if (!scaleFromCenter)
        {
            // Move window.
            transform.position = windowStartPos + new Vector3(offset.x / 2, -offset.y / 2, 0);
        }
    }
}
