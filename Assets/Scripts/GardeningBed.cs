using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardeningBed : MonoBehaviour
{
    YellowOats[] yellowOats;
    GreenOat[] greenOats;
    [SerializeField] GameObject greenOatsParent;
    HayBaleController hayBaleController;
  //  Animator animator;
    public int hitsLeft = 4;
    int startHitsLeft;
    public bool isEmpty;
    private void Start()
    {
        hayBaleController = GetComponentInChildren<HayBaleController>();
        yellowOats = GetComponentsInChildren<YellowOats>();
        greenOats = greenOatsParent.GetComponentsInChildren<GreenOat>();
        startHitsLeft = hitsLeft;
    }
    private void Update()
    {
        EndGrowCheck();
    }

    public void Cut()
    {
        
        foreach (var ear in yellowOats)
        {
            Vector3 randomVector = new Vector3(Random.Range(-10f, 10f), 30f, Random.Range(-10f, 10f));
            if (hitsLeft >= 1)
            {
                
                GameObject piece = ear.pieces[hitsLeft - 1];
                Vector3 currentTransform = piece.transform.position;
                piece.GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePosition;
                piece.GetComponent<Rigidbody>().AddForce(randomVector * 500f * Time.deltaTime);
                piece.GetComponent<OatPieceDisappear>().enabled = true;
                piece.layer = 7;
                hayBaleController.spawnHayBaleSwitch = true;

            }

        }
        hitsLeft--;
        if (hitsLeft == 0)
        {
            isEmpty = true;
            greenOatsParent.SetActive(true);
        }
        

    }

    void EndGrowCheck()
    {
        foreach (var greenOat in greenOats)
        {
           if(greenOat.isDone)
            {
                EndGrow();
            }
        }
    }    
    public void EndGrow()
    {
        foreach (var greenOat in greenOats)
        {
            greenOat.isDone = false;
        }
        greenOatsParent.SetActive(false);
        hitsLeft = startHitsLeft;
        isEmpty = false;
        foreach (var ear in yellowOats)
        {
            for (int i = 0; i < ear.pieces.Length; i++)
            {
                ear.pieces[i].SetActive(true);
            }
        }
    }
    
}
