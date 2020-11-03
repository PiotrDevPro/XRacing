using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PositionDown : MonoBehaviour
{
    public GameObject positionDisplay;

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            positionDisplay.GetComponent<Text>().text = "2/2";
        }

    }

}
