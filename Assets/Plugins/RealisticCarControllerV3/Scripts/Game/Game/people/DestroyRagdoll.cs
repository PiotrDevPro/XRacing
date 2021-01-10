using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyRagdoll : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 60f);
    }

    

}
