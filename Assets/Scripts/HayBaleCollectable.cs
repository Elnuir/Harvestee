using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HayBaleCollectable : MonoBehaviour
{
    Rigidbody rb;
    public bool isFlying;
    PlayerController playerController;
    Stack stack;
    [SerializeField] float parabollaCoefficient, force;
    Vector3 startPosition, startRotation;
    bool isCollected;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerController = FindObjectOfType<PlayerController>();
        stack = FindObjectOfType<Stack>();
        startPosition = transform.position;
        startRotation = transform.rotation.eulerAngles;
        
    }
    void Fly()
    {
        if(isFlying)
        {
            Vector3 direction = new Vector3(playerController.transform.position.x - transform.position.x, 
                parabollaCoefficient, playerController.transform.position.z - transform.position.z);

            rb.AddForce(direction * force);
            isFlying = false;
        }
    }
    
    

    // Update is called once per frame
    void Update()
    {
        Fly();

    }
}
