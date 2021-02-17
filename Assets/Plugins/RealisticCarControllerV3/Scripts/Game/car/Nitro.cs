using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Nitro : MonoBehaviour
{
    private GameObject _playerCar;
    private bool boostInput;
    public Slider boostlvl;
    int count = 0;

    private void Update()
    {
        count += 1;
        if (count == 1){
        _playerCar = GameObject.FindGameObjectWithTag("Player");
        }
        boostlvl.value = _playerCar.GetComponentInChildren<RCC_CarControllerV3>().NoS;
  }
}
