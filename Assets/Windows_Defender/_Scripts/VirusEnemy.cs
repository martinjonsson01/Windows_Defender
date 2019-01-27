using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusEnemy : Enemy
{
    public GameObject arrowPrefab;
    public GameObject holdingLegsPrefab;
    
    GameObject arrowObject;
    GameObject holdingLegsObject;

    GameObject lastHeldWindow;
    
    enum VirusState
    {
        JUMPING,
        HOLDINGWINDOW,
        SHOWINGDIRECTION
    }

    VirusState currentState;

    Animator animator;

    Vector2 dir;

    float stateTimer;
    float idleTime, arrowTime;

    new void Start()
    {
        base.Start();

        attackPower = 20;
        movementSpeed = 10;

        landingShake = 0.5f;
        
        idleTime = 3;
        arrowTime = 1;

        rigidbody.gravityScale = 0;

        GetComponent<BoxCollider2D>().isTrigger = true;

        // Scriptet sitter på censored enemy
        if (GetComponent<Animator>() == null)
        {
            arrowPrefab = GetComponent<CensoredEnemy>().virusArrowPrefab;
            holdingLegsPrefab = GetComponent<CensoredEnemy>().virusHoldingLegsPrefab;
        }
        else
            animator = GetComponent<Animator>();

        currentState = VirusState.JUMPING;
        FindBestWindow();
    }
    
    new void Update()
    {
        // Direction vector
        InsideScreen();
        dir.x = direction * Mathf.Abs(dir.x);
        
        // Animation
        if(animator != null)
            animator.SetBool("isJumping", currentState == VirusState.JUMPING);
        
        // Timer
        if (currentState != VirusState.JUMPING)
            stateTimer += Time.deltaTime;

        switch (currentState)
        {
            case VirusState.JUMPING:

                rigidbody.MovePosition(transform.position + (Vector3) dir * movementSpeed * Time.deltaTime);

                break;
            case VirusState.HOLDINGWINDOW:

                if (stateTimer >= idleTime && CanFindNewWindow())
                {
                    stateTimer = 0;

                    currentState = VirusState.SHOWINGDIRECTION;

                    FindBestWindow();

                    arrowObject = Instantiate(arrowPrefab, gameObject.transform.position, Quaternion.identity);
                    arrowObject.transform.parent = gameObject.transform;
                    arrowObject.transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);
                }

                break;
            case VirusState.SHOWINGDIRECTION:

                if (stateTimer >= arrowTime)
                {
                    stateTimer = 0;

                    currentState = VirusState.JUMPING;

                    transform.parent.GetComponent<Window>().AttributeActive = true;

                    transform.parent = null;
                    
                    Destroy(arrowObject);
                    Destroy(holdingLegsObject);
                }

                break;
        }
    }

    List<GameObject> GetWindows()
    {
        GameObject[] windows1 = GameObject.FindGameObjectsWithTag(WindowTags.TAG1);
        GameObject[] windows2 = GameObject.FindGameObjectsWithTag(WindowTags.TAG2);
        List<GameObject> allWindows = new List<GameObject>();
        for (int i = 0; i < windows1.Length; i++)
        {
            allWindows.Add(windows1[i]);
        }
        for (int i = 0; i < windows2.Length; i++)
        {
            allWindows.Add(windows2[i]);
        }

        return allWindows;
    }

    bool CanFindNewWindow()
    {
        List<GameObject> allWindows = GetWindows();
        
        for (int i = 0; i < allWindows.Count; i++)
        {
            if (allWindows[i].transform.position.y < transform.position.y)
            {
                return true;
            }
        }

        return false;
    }

    // Hittar det fönster som är högst uppe, samtidigt som det är under viruset
    void FindBestWindow()
    {
        List<GameObject> allWindows = GetWindows();

        GameObject current = null;
        for(int i = 0; i < allWindows.Count; i++)
        {
            if(allWindows[i].transform.position.y < transform.position.y)
            {
                if(current == null || allWindows[i].transform.position.y > current.transform.position.y)
                {
                    current = allWindows[i];
                }
            }
        }

        if (current != null)
        {
            dir = current.transform.position - transform.position;
            dir.Normalize();

            SetDirection((int) Mathf.Sign(dir.x));
        }
        else
            dir = Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (currentState == VirusState.JUMPING)
        {
            bool isWindow = collision.gameObject.tag == WindowTags.TAG1 ||
            collision.gameObject.tag == WindowTags.TAG2;

            GameObject collParent = collision.transform.parent == null ? collision.gameObject : collision.transform.parent.gameObject;

            if(isWindow && (lastHeldWindow == null || !lastHeldWindow.Equals(collParent)))
            {
                currentState = VirusState.HOLDINGWINDOW;

                lastHeldWindow = collParent;

                collParent.GetComponent<Window>().AttributeActive = false;

                dir = Vector2.zero;
                transform.position = collision.gameObject.transform.position;
                transform.parent = collParent.transform;

                holdingLegsObject = Instantiate(holdingLegsPrefab, collision.transform.position, Quaternion.identity);
                holdingLegsObject.transform.parent = collision.gameObject.transform;
            }
        }
    }
}
