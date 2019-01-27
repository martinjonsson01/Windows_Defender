using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButton : MonoBehaviour
{
    [SerializeField]
    private GameObject GameStateManager;
    [SerializeField]
    private GameObject _windowSprite;
    [SerializeField]
    private GameObject _quitButton;

    private const float WINDOW_RESIZE_TO_X = 3.0f;
    private const float WINDOW_RESIZE_TO_Y = 2.0f;
    
    private const float ANIMATION_DURATION = 5.0f;

    private Window _mainWindow;

    private bool _animateWindow;
    private bool _gameStateManagerEnabled;

    private float _animatedForSeconds;

    private Vector2 _windowStartSize;

    // Start is called before the first frame update
    void Start()
    {
        _mainWindow = GameObject.Find("MainWindow").GetComponent<Window>();
        _windowStartSize = _mainWindow.Size;
    }

    // Update is called once per frame
    void Update()
    {
        if (_mainWindow.Size.y <= 3) _animateWindow = false;

        if (_animateWindow)
        {
            // Interpolate between start size and goal size.
            var newSizeX = Mathf.Lerp(_windowStartSize.x, WINDOW_RESIZE_TO_X, _animatedForSeconds / ANIMATION_DURATION);
            var newSizeY = Mathf.Lerp(_windowStartSize.y, WINDOW_RESIZE_TO_Y, _animatedForSeconds / ANIMATION_DURATION);
            var newSize = new Vector2(newSizeX, newSizeY);
            // Resize window.
            _mainWindow.SetSize(newSize, Vector3.zero, Vector2.zero, true, true);
            // Resize window content.
            var newScaleX = Mathf.Lerp(1, 0.35f, _animatedForSeconds / ANIMATION_DURATION);
            var newScaleY = Mathf.Lerp(1, 0.35f, _animatedForSeconds / ANIMATION_DURATION);
            var newScale = new Vector3(newScaleX, newScaleY, 1);
            _windowSprite.transform.localScale = newScale;
            _quitButton.transform.localScale = newScale;
            transform.localScale = newScale;

            // Keep track of how long animation has lasted.
            _animatedForSeconds += Time.deltaTime;

            // Enable GameStateManager if not already enabled.
            if (!_gameStateManagerEnabled)
            {
                // Enable it after 5 seconds.
                StartCoroutine(EnableGameStateManager(ANIMATION_DURATION));
                // Make sure it is only enabled once.
                _gameStateManagerEnabled = true;
            }
        }
            
    }

    private void OnMouseDown()
    {
        _animateWindow = true;
    }

    IEnumerator EnableGameStateManager(float delay)
    {
        yield return new WaitForSeconds(delay);

        GameStateManager.SetActive(true);
    }
}
