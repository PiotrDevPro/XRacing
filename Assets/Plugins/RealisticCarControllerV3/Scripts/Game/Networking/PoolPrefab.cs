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
                pool.ResourceCache.Add(CathingLoadFiles.manage.buggy_gtr_ab.name, CathingLoadFiles.manage.buggy_gtr_ab);
                pool.ResourceCache.Add(CathingLoadFiles.manage.gt500_ab.name, CathingLoadFiles.manage.gt500_ab);
                pool.ResourceCache.Add(CathingLoadFiles.manage.modelT_ab.name, CathingLoadFiles.manage.modelT_ab);
                pool.ResourceCache.Add(CathingLoadFiles.manage.townCar_ab.name, CathingLoadFiles.manage.townCar_ab);
                pool.ResourceCache.Add(CathingLoadFiles.manage.ikarus.name, CathingLoadFiles.manage.ikarus);
                pool.ResourceCache.Add(CathingLoadFiles.manage.lambo_ab.name, CathingLoadFiles.manage.lambo_ab);
                pool.ResourceCache.Add(CathingLoadFiles.manage.i8_ab.name, CathingLoadFiles.manage.i8_ab);
                pool.ResourceCache.Add(CathingLoadFiles.manage.rr_ab.name, CathingLoadFiles.manage.rr_ab);
               // pool.ResourceCache.Add(CathingLoadFiles.manage.vetty_ab.name, CathingLoadFiles.manage.vetty_ab);
               // pool.ResourceCache.Add(CathingLoadFiles.manage.rasta_ab.name, CathingLoadFiles.manage.rasta_ab);
        }

    }
}
