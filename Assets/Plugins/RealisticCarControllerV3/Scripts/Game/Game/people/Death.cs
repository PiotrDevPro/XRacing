using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Death : MonoBehaviour
{
    public GameObject ragdoll;

    private void OnTriggerEnter(Collider col)
    {
      if (SceneManager.GetActiveScene().name != "level_lap6")
        {
            if (netManager.manage.DangerSpeed)
            {
                if (col.CompareTag("Player"))
                {
                    Instantiate(ragdoll, transform.position, transform.rotation);
                    Amplitude.Instance.logEvent("DeadPeople");
                    Destroy(gameObject);
                }
            }
        }

        else if (CarSpawnNew.manage.InstantiatedCar.GetComponent<RCC_CarControllerV3>().speed > 30f)
        {
            if (col.CompareTag("Player"))
            {
                Instantiate(ragdoll, transform.position, transform.rotation);
                Amplitude.Instance.logEvent("DeadPeople");
                Destroy(gameObject);
            }
        }

       else if (col.CompareTag("CarAI"))
        {
            Instantiate(ragdoll, transform.position, transform.rotation);
            Amplitude.Instance.logEvent("DeadPeople");
            Destroy(gameObject);
        }
    }
}
