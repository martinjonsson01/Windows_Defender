using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainWindow : Window
{
    [SerializeField]
    private GameObject GameStateManager;
    [SerializeField]
    private GameObject _windowSprite;
    [SerializeField]
    private GameObject _quitButton;
    [SerializeField]
    private GameObject _startButton;

    public Vector2 ResizeTarget = new Vector2(3, 2);
    public Vector2 TranslateTarget = new Vector2(0, 0);

    public float AnimationDuration = 2.0f;
    
    private bool _animateWindow;
    private bool _gameStateManagerEnabled;

    private float _animatedForSeconds;

    private Vector2 _startSize;
    private Vector2 _startPos;

    public override void Start()
    {
        base.Start();

        _startSize = Size;
        _startPos = transform.position;
    }

    public override void Update()
    {
        base.Update();

        // Stop animating when target size has been reached.
        if (Size == ResizeTarget) _animateWindow = false;

        if (_animateWindow)
        {
            // Enable window resizing.
            Movable = true;

            // Interpolate between start position and target position.
            var newPosX = Mathf.Lerp(_startPos.x, TranslateTarget.x, _animatedForSeconds / AnimationDuration);
            var newPosY = Mathf.Lerp(_startPos.y, TranslateTarget.y, _animatedForSeconds / AnimationDuration);
            var newPos = new Vector2(newPosX, newPosY);
            // Interpolate between start size and target size.
            var newSizeX = Mathf.Lerp(_startSize.x, ResizeTarget.x, _animatedForSeconds / AnimationDuration);
            var newSizeY = Mathf.Lerp(_startSize.y, ResizeTarget.y, _animatedForSeconds / AnimationDuration);
            var newSize = new Vector2(newSizeX, newSizeY);
            // Resize window.
            SetSize(newSize, Vector3.zero, Vector2.zero, true, true);
            // Resize window content.
            var newScaleX = Mathf.Lerp(1, 0.35f, _animatedForSeconds / AnimationDuration);
            var newScaleY = Mathf.Lerp(1, 0.35f, _animatedForSeconds / AnimationDuration);
            var newScale = new Vector3(newScaleX, newScaleY, 1);
            
            transform.localScale = newScale;
            // Update window position.
            SetPosition(newPos, true);

            // Keep track of how long animation has lasted.
            _animatedForSeconds += Time.deltaTime;

            // Enable GameStateManager if not already enabled.
            if (!_gameStateManagerEnabled)
            {
                // Enable it after animation has finshed.
                StartCoroutine(EnableGameStateManager(AnimationDuration));
                // Make sure it is only enabled once.
                _gameStateManagerEnabled = true;
            }
        }
    }

    public void PlayPressed()
    {
        _animateWindow = true;
    }

    IEnumerator EnableGameStateManager(float delay)
    {
        yield return new WaitForSeconds(delay);

        _startButton.SetActive(true);
        if (GameStateManager != null)
            GameStateManager.SetActive(true);
    }
}
