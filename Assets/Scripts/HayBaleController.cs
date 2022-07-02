using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HayBaleController : MonoBehaviour
{
    List<HayBaleCollectable> haybales = new List<HayBaleCollectable>();
    [SerializeField] GameObject ethalon;
    [SerializeField] Transform BackUpPoint;
    public bool spawnHayBaleSwitch;
    void Start()
    {
        haybales.AddRange(GetComponentsInChildren<HayBaleCollectable>());
        foreach(var haybale in haybales)
        {
            haybale.gameObject.SetActive(false);
        }
    }


    void Update()
    {
        if(spawnHayBaleSwitch)
        {
            if (haybales.TrueForAll(a => a.gameObject.activeSelf))
            {
                GameObject haybaleNew = Instantiate(ethalon, BackUpPoint.position, Quaternion.identity);
                haybaleNew.GetComponent<HayBaleCollectable>().isFlying = true;
                haybales.Add(haybaleNew.GetComponent<HayBaleCollectable>());
            }
            else
            {
                var hayBaleCollectable = haybales.FirstOrDefault(a => !a.gameObject.activeSelf);
                hayBaleCollectable.gameObject.SetActive(true);
                hayBaleCollectable.isFlying = true;
            }
            spawnHayBaleSwitch = false;
        }
        

    }
}
