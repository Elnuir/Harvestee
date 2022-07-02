using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardeningBedTrigger : MonoBehaviour
{
    PlayerController playerController;
    [SerializeField] GameObject scythe;
    void Start()
    {
     playerController = GetComponentInParent<PlayerController>();   
    }


    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.CompareTag("GardeningBed") && !other.GetComponent<GardeningBed>().isEmpty)
        {

            playerController.isHarvesting = true;
            scythe.SetActive(true);
            if (playerController.isTouchingWheat)
            {
                other.GetComponent<GardeningBed>().Cut();
                print("cut");
                playerController.isTouchingWheat = false;
                scythe.SetActive(false);
            }
        }
        else if(other.gameObject.CompareTag("GardeningBed") && other.GetComponent<GardeningBed>().isEmpty)
        {
            playerController.isHarvesting = false;
            scythe.SetActive(false);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("GardeningBed"))
        {
            playerController.isHarvesting = false;
            scythe.SetActive(false);
        }
    }
}
