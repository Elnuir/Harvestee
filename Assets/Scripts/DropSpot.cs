using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropSpot : MonoBehaviour
{
    Stack stack;
    void Start()
    {
        stack = FindObjectOfType<Stack>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if (!stack.isBusy)
            {
                stack.isGoingToBarn = true;
                stack.isCoolingDown = true;
            }
        }
    }

}
