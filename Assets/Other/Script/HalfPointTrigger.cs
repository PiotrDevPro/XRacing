using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfPointTrigger : MonoBehaviour
{
    public GameObject lapCompleteTrig;
    public GameObject HalfLapTrig;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            lapCompleteTrig.SetActive(true);
            HalfLapTrig.SetActive(false);
        }
    }
}
  
         
