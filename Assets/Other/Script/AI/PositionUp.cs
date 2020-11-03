using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PositionUp : MonoBehaviour
{
    public GameObject positionDisplay;

     void OnTriggerExit(Collider other)
    {
       if (other.tag == "Player")
        {
            positionDisplay.GetComponent<Text>().text = "1/2";
        } 
    }
}
