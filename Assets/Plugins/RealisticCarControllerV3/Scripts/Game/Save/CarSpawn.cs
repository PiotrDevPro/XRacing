using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawn : MonoBehaviour
{
    public static CarSpawn manage;
    public Transform PlayerCarPoint;
    public Transform spawnPoint;

    public GameObject[] CarsPrefabs;
    

    void Awake()
    {
        manage = this;
    }

    void Start()
    {
        GameObject InstantiatedCar = Instantiate(CarsPrefabs[PlayerPrefs.GetInt("CurrentCar")], spawnPoint.position, spawnPoint.rotation) as GameObject;

        InstantiatedCar.transform.SetParent(PlayerCarPoint);

        //GameObject InstantiatedCar = Lean.Pool.LeanPool.Spawn(CarsPrefabs[PlayerPrefs.GetInt("CurrentCar")], spawnPoint.position, spawnPoint.rotation) as GameObject;

        //RCC_Camera. = InstantiatedCar.transform;
    }
}
