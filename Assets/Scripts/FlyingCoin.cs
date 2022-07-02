using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FlyingCoin : MonoBehaviour
{
    MoneyDisplay score;
    public bool isTimeToFly, isBeingSold;
    Vector3 direction, startLocalPosition;
    CoinsController coinsController;
    [SerializeField] float startDelta;
    float delta;
    Animator animator;
    string COINAPPEAR = "Appear";
    [SerializeField] int worth = 15;
    [SerializeField] float flyDuration;
    void Start()
    {
        delta = startDelta;
        animator = GetComponent<Animator>();
        startLocalPosition = transform.localPosition;
        score = FindObjectOfType<MoneyDisplay>();
        coinsController = GetComponentInParent<CoinsController>();
        coinsController.flyingCoins.Add(this);
        gameObject.GetComponent<Image>().enabled = false;
    }
    void Jump()
    {
            direction = new Vector3(score.transform.position.x, score.transform.position.y, 0);
            transform.DOMove(direction, flyDuration).SetEase(Ease.InSine);
    }

    // Update is called once per frame
    void Update()
    {
        SellCheck();
        isDoneCheck();
    }
    void SellCheck()
    {
        if (isTimeToFly)
        {
            delta -= Time.deltaTime;
            if (delta <= 0)
            {
                Jump();
                gameObject.GetComponent<Image>().enabled = true;
                animator.SetTrigger(COINAPPEAR);
                isTimeToFly = false;
                delta = startDelta;
                isBeingSold = true;
            }
        }

    }
    void isDoneCheck()
    {
        if (isBeingSold && Vector3.Distance(direction, transform.position) <= 1f)
        {
            transform.DOComplete();

            score.Money = score.Money + worth;
            gameObject.GetComponent<Image>().enabled = false;
            transform.localPosition = startLocalPosition;
            isBeingSold = false;
            


        }
    }
}

