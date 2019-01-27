using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Rendering;

public class Window : MonoBehaviour
{
    protected const int HANDLE_HEIGHT = 1;
    protected const int MIN_SIZE = 1;
    protected const int MAX_SIZE = 20;

    protected SpriteRenderer backgroundSpriteRenderer;
    protected SpriteRenderer handleSpriteRenderer;
    protected SpriteRenderer crackRenderer;

    protected BoxCollider2D windowCollider;
    protected BoxCollider2D handleCollider;

    protected SortingGroup sortingGroup;

    [SerializeField]
    private Material _cropMaterial;
    private Material _materialInstance;

    [SerializeField]
    private Sprite[] _crackSprites = new Sprite[5];

    private Camera _mainCam;

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
    /// Whether or not this window is movable.
    /// </summary>
    public bool Movable = true;

    /// <summary>
    /// The current durability of this window.
    /// </summary>
    public float Durability = 200;

    /// <summary>
    /// The maximum durability of this window.
    /// </summary>
    public float MaxDurability = 200;

    /// <summary>
    /// Gets the movement speed coefficient of this window.
    /// </summary>
    /// <returns>The movement speed coefficient.</returns>
    public virtual float GetMovementSpeedCoefficient { get; set; } = 1;

    /// <summary>
    /// Gets the size of the window in world units.
    /// </summary>
    public virtual Vector2 Size => (transform as RectTransform).sizeDelta;

    // Start is called before the first frame update
    public virtual void Start()
    {
        backgroundSpriteRenderer = transform.Find("Background").GetComponent<SpriteRenderer>();
        crackRenderer = transform.Find("Cracks").GetComponent<SpriteRenderer>();
        handleSpriteRenderer = transform.Find("Handle").GetComponent<SpriteRenderer>();
        windowCollider = transform.GetComponent<BoxCollider2D>();
        handleCollider = transform.Find("Handle").GetComponent<BoxCollider2D>();
        sortingGroup = GetComponent<SortingGroup>();
        _resizeHandle = transform.Find("ResizeHandle").gameObject;
        _mainCam = Camera.main;

        // Apply instance of crop material to window renderers.
        _materialInstance = Instantiate(_cropMaterial);
        for(var i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
            var childRenderer = child.GetComponent<Renderer>();
            if (childRenderer != null)
            {
                childRenderer.sharedMaterial = _materialInstance;
            }
        }
    }

    // Update is called once per frame
    public virtual void Update()
    {
        // Check if at zero durability.
        DamageDone();

        // Make resize handle hidden if window not resizable.
        if (_resizeHandle.activeSelf != Resizable)
            _resizeHandle.SetActive(Resizable);

        // If not movable then disable collider.
        windowCollider.enabled = Movable;

        // Resize shader crop.
        UpdateShaderCrop();

        // Update window cracks.
        UpdateWindowCracks();
    }
    
    private void UpdateWindowCracks()
    {
        var unroundedIndex = Durability / MaxDurability * 5.0f;
        var crackIndex = ((int)Math.Round(unroundedIndex, 0)) - 1;
        crackIndex = Math.Max(0, crackIndex);

        if (crackIndex > 4 || crackIndex < 0) Debugger.Break();
        var currentCrack = _crackSprites[crackIndex];
        if (crackRenderer.sprite != currentCrack)
            crackRenderer.sprite = currentCrack;
    }

    private void UpdateShaderCrop()
    {
        var rect = transform as RectTransform;

        var minPos = _mainCam.WorldToViewportPoint(rect.offsetMin);
        var maxPos = _mainCam.WorldToViewportPoint(rect.offsetMax);

        _materialInstance.SetFloat("_MinX", minPos.x);
        _materialInstance.SetFloat("_MinY", minPos.y);
        _materialInstance.SetFloat("_MaxX", maxPos.x);
        _materialInstance.SetFloat("_MaxY", maxPos.y);
    }

    void DamageDone()
    {
        if (Durability <= 0)
            Destroy(this.gameObject);
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
    /// <param name="ignoreLimits">Whether or not position limits should be ignored.</param>
    public virtual void SetPosition(Vector3 pos, bool ignoreLimits = false)
    {
        if (Movable)
        {
            if (!ignoreLimits)
            {
                float xx = -5.8f;
                float yy = -1.3f;

                if(pos.y <= yy)
                    pos.x = Mathf.Max(pos.x , xx);

                if(pos.x <= xx)
                    pos.y = Mathf.Max(pos.y, yy);
            }
            transform.position = pos;
        }
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
        crackRenderer.size = windowSize;
        (transform as RectTransform).sizeDelta = windowSize;
        windowCollider.size = windowSize;
        // Change size of handle by the offset.
        handleCollider.size = handleSize;
        handleSpriteRenderer.size = handleSize;

        // TODO: ur mom

        if (!scaleFromCenter)
        {
            // Move window.
            transform.position = windowStartPos + new Vector3(offset.x / 2, -offset.y / 2, 0);
        }
    }
}
