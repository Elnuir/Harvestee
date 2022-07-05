using System.Collections.Generic;
using UnityEngine;

public class Stack : MonoBehaviour
{
    public int maxCapacity = 40;
    public int currentCapacity = 0;
    List<Transform> hayBalesTransforms = new List<Transform>();
    List<FlyingSoldHayBale> soldHayBales = new List<FlyingSoldHayBale>();
    PlayerController playerController;
    float characterSpeed;
    Animator animator;
    string PLAYERSPEED = "playerSpeed";
    public bool isFull, isGoingToBarn;
    Transform barn;
    [SerializeField] float startDelta = 0.5f;
    float delta;
    public bool isBusy;

    bool canBeCollectedNewOnes;

    public int childAmount;
    int a = 1;
    int b;
    public int c;

    [SerializeField] float startDontCollectCoolDown;
    float dontCollectCoolDown;
    public bool isCoolingDown;
    //TempStack tempStack;
    void Start()
    {
        playerController = GetComponentInParent<PlayerController>();
        barn = FindObjectOfType<DropSpot>().transform;
        delta = startDelta;
        dontCollectCoolDown = startDontCollectCoolDown;
        // tempStack = FindObjectOfType<TempStack>();
        animator = GetComponent<Animator>();
        hayBalesTransforms.AddRange(GetComponentsInChildren<Transform>());
        hayBalesTransforms.RemoveAt(0);
        maxCapacity = hayBalesTransforms.Count;
        foreach (var hayBalesTransform in hayBalesTransforms)
        {
            hayBalesTransform.gameObject.GetComponent<FlyingSoldHayBale>().barn = barn;
            soldHayBales.Add(hayBalesTransform.gameObject.GetComponent<FlyingSoldHayBale>());
            hayBalesTransform.gameObject.SetActive(false);
        }


    }

    public void GetABlock()
    {

        if (currentCapacity < maxCapacity)
        {
            hayBalesTransforms[currentCapacity].gameObject.SetActive(true);
            currentCapacity++;
        }

        else if (currentCapacity == maxCapacity)
        {
            isFull = true;
        }

    }

    void SellEverithing()
    {
        if (isGoingToBarn)// && !isBusy)
        {
            isBusy = true;
            animator.enabled = false;
            delta -= Time.deltaTime;
            if (currentCapacity == 0)
            {
                isGoingToBarn = false;
                isBusy = false;

            }
            else if (delta <= 0 && soldHayBales[currentCapacity - 1].gameObject.activeSelf)
            {


                Vector3 positionBefore = soldHayBales[currentCapacity - 1].transform.position;
                soldHayBales[currentCapacity - 1].gameObject.transform.parent = null;
                soldHayBales[currentCapacity - 1].transform.position = positionBefore;

                soldHayBales[currentCapacity - 1].isTimeToFly = true;
                delta = startDelta;
                currentCapacity--; ;
                b++;
            }


        }
    }

    // Update is called once per frame
    void Update()
    {
        CoolDownCheck();
        AnimSetter();
        
        SellEverithing();
    }

    public void CheckIfEverythingIsSold()
    {
        if (c == b)
        {
            c = 0;
            b = 0;
            isBusy = false;
            isFull = false;
            animator.enabled = true;
        }
    }

    void AnimSetter()
    {
        characterSpeed = playerController.move.magnitude;
        animator.SetFloat(PLAYERSPEED, characterSpeed);
    }
    void CoolDownCheck()
    {
        if(isCoolingDown)
        {
            dontCollectCoolDown -= Time.deltaTime;
            if(dontCollectCoolDown <= 0)
            {
                dontCollectCoolDown = startDontCollectCoolDown;
                isCoolingDown = false;
            }
        }
    }
}
