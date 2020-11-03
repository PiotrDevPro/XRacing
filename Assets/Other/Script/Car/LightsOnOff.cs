using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightsOnOff : MonoBehaviour
{
    public GameObject[] objects;
    private int l_CurrentActiveObject;

    public void lightsTurn()
    {
        int nextactiveobject = l_CurrentActiveObject + 1 >= objects.Length ? 0 : l_CurrentActiveObject + 1;

        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].SetActive(i == nextactiveobject);

        }

        l_CurrentActiveObject = nextactiveobject;
    }
}
