using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OatPieceDisappear : MonoBehaviour
{

    [SerializeField] float disappearingRate;
    float startDisappearingRate;
    Vector3 startPosition, startScale, startRotation;
    BoxCollider boxCollider;
    Rigidbody rb;
    private void Start()
    {
        startDisappearingRate = disappearingRate;
        startRotation = transform.rotation.eulerAngles;
        startPosition = transform.position;
        startScale = transform.localScale;
        boxCollider = GetComponent<BoxCollider>();
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        float disRat = disappearingRate * Time.deltaTime;
        transform.localScale = new Vector3(transform.localScale.x - disRat, 
            transform.localScale.y - disRat, transform.localScale.z - disRat);
        if(transform.localScale.x <= 0.1f)
        {
            rb.angularVelocity = Vector3.zero;
            rb.velocity = Vector3.zero;
            transform.position = startPosition;
            transform.rotation = Quaternion.Euler(startRotation);
            transform.localScale = startScale;
            rb.constraints = RigidbodyConstraints.FreezePosition;
            disappearingRate = startDisappearingRate;
            this.enabled = false;
            gameObject.SetActive(false);
        }
    }
}
