using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveColl : MonoBehaviour
{
    private Collider _col;
    private void Start()
    {
        _col = GetComponent<Collider>();
    }
    private void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "Player")
        {
            _col.isTrigger = false;

        }

        if (coll.tag == "Car")
        {
            _col.isTrigger = true;

        }
    }

    private void OnTriggerExit(Collider coll)
    {
        if (coll.tag == "Player")
        {
            _col.isTrigger = true;

        }
    }
}
