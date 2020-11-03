using System.Collections;
using UnityEngine;

public class DriverCam : MonoBehaviour
{
    void Start()
    {
        StartCoroutine("FixShakeDelayedDriver");
    }

    public void FixShakeZero()
    {

        StartCoroutine("FixShakeDelayedDriver");

    }

    IEnumerator FixShakeDelayedDriver()
    {

        if (!GetComponent<Rigidbody>())
            yield return null;

        yield return new WaitForFixedUpdate();
        GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.None;
        yield return new WaitForFixedUpdate();
        GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.Interpolate;

    }
}
