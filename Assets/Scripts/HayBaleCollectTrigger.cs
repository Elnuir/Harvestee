using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HayBaleCollectTrigger : MonoBehaviour
{
    // Start is called before the first frame upd
    Vector3 startPosition, startRotation;
    Stack stack;

    void Start()
    {
        startPosition = transform.parent.position;//transform.position;
        startRotation = transform.parent.rotation.eulerAngles;//transform.rotation.eulerAngles;
        stack = FindObjectOfType<Stack>();
    }
    private void OnTriggerEnter(Collider other)
    {
       // (other.gameObject.TryGetComponent<PlayerController>(out PlayerController a))
        if (other.gameObject.TryGetComponent<PlayerController>(out PlayerController a))
        {
            // isCollected = true;
            if (!stack.isBusy)
            {
                stack.GetABlock();

                if (!stack.isFull)
                {
                    transform.parent.position = startPosition;
                    transform.parent.rotation = Quaternion.Euler(startRotation);

                    //     print("yp");
                    transform.parent.gameObject.SetActive(false);
                }
            }
            
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
