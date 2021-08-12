using UnityEngine;
using System.Collections.Generic;
using Photon.Pun;

public class PoolPrefab : MonoBehaviour
{
    
    private void Start()
    {
        DefaultPool pool = PhotonNetwork.PrefabPool as DefaultPool;
         if (pool != null)
         {
                pool.ResourceCache.Add(CathingLoadFiles.manage.hot_rodd_ab.name, CathingLoadFiles.manage.hot_rodd_ab);
                print("Prefabs  " + CathingLoadFiles.manage.hot_rodd_ab.name);
                pool.ResourceCache.Add(CathingLoadFiles.manage.buggy_gtr_ab.name, CathingLoadFiles.manage.buggy_gtr_ab);
                print("Prefabs  " + CathingLoadFiles.manage.buggy_gtr_ab.name);
                pool.ResourceCache.Add(CathingLoadFiles.manage.gt500_ab.name, CathingLoadFiles.manage.gt500_ab);
                print("Prefabs  " + CathingLoadFiles.manage.gt500_ab.name);
                pool.ResourceCache.Add(CathingLoadFiles.manage.modelT_ab.name, CathingLoadFiles.manage.modelT_ab);
                print("Prefabs  " + CathingLoadFiles.manage.modelT_ab.name);
                pool.ResourceCache.Add(CathingLoadFiles.manage.townCar_ab.name, CathingLoadFiles.manage.townCar_ab);
                print("Prefabs  " + CathingLoadFiles.manage.townCar_ab.name);
                pool.ResourceCache.Add(CathingLoadFiles.manage.ikarus.name, CathingLoadFiles.manage.ikarus);
                print("Prefabs  " + CathingLoadFiles.manage.ikarus.name);

        }

    }

    private void Update()
    {
        
    }
}
