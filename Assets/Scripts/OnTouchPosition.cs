using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnTouchPosition : MonoBehaviour
{
    Animator animator;
    string ISAPPEAR = "isAppear";
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void OnGUI()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            touchPosition.z = 0f;
            switch (touch.phase)
            {
                // Record initial touch position.
                case TouchPhase.Began:
                    transform.position = touch.position;
                    animator.SetBool(ISAPPEAR, true);
                    break;

                // Determine direction by comparing the current touch position with the initial one.
                case TouchPhase.Moved:
                  //  direction = touch.position - startPos;
                    break;

                // Report that a direction has been chosen when the finger is lifted.
                case TouchPhase.Ended:
                    animator.SetBool(ISAPPEAR, false);
                    break;
            }
        }

    }
}
