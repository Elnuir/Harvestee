using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FlyingSoldHayBale : MonoBehaviour
{
    private Vector3 startLocalPosition;
    public bool isTimeToFly;
    bool isBeingSold;
    public Transform barn;
    Stack stack;
    CoinsController coinsController;
    [SerializeField] float jumpPower, duration, stopDistance;
    [SerializeField] int numberOfJumps;

    void SellCheck()
    {
        if (isTimeToFly)
        {
            Jump();
            isTimeToFly = false;
            isBeingSold = true;
        }
        
    }
    void Jump()
    {
        transform.DOJump(barn.transform.position, jumpPower, numberOfJumps, duration);
    }
    void Start()
    {
        coinsController = FindObjectOfType<CoinsController>();
        startLocalPosition = transform.localPosition;
        stack = FindObjectOfType<Stack>();
    }

    void Update()
    {
        SellCheck();
        isDoneCheck();
    }
    void isDoneCheck()
    {
        if (isBeingSold && Vector3.Distance(barn.transform.position, transform.position) <= stopDistance)
        {
            transform.DOComplete();
            gameObject.SetActive(false);
            gameObject.transform.SetParent(stack.gameObject.transform);
            transform.localPosition = startLocalPosition;
            stack.c++;
            coinsController.flyingCoins[stack.c - 1].isTimeToFly = true;
            stack.CheckIfEverythingIsSold();
            isBeingSold = false;


        }
    }

}
