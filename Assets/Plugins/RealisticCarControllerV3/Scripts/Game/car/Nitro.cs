using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Nitro : MonoBehaviour
{
    private GameObject _playerCar;
    private bool boostInput;
    public Slider boostlvl;

    void Start()
    {
        _playerCar = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        boostlvl.value = _playerCar.GetComponentInChildren<RCC_CarControllerV3>().NoS;
  }
}
