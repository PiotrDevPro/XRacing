﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class CarSpawnNew : MonoBehaviour
{
    public static CarSpawnNew manage;

    private RCC_PhotonNetwork photonNetwork;
    public Transform PlayerCarPoint;
    public Transform spawnPoint;
    public GameObject[] CarsPrefabs;
    public AudioSource[] PlayerVoice;
    public Text maxspd;
    int count = 0;
    int cnt = 0;

    public GameObject nosButton;
    public Slider noslevel;
    public GameObject InstantiatedCar;

    private GameObject carMirror;


    void Awake()
    {
        manage = this;
        

    }

    void Start()
    {
        print(PlayerPrefs.GetInt("CurrentCar"));
        Application.targetFrameRate = 300;
        
        if (SceneManager.GetActiveScene().name == "level_lap6" && MainMenuManager.manage.isAllvsYou)
        {
            PlayerPrefs.SetInt("Rating", PlayerPrefs.GetInt("Rating") + 2);
            //InsideOutsideCar.manage.OutFromCarButton.SetActive(false);
            
            if (PlayerPrefs.GetInt("CurrentCar") == 0 
                || PlayerPrefs.GetInt("CurrentCar") == 5 ||  PlayerPrefs.GetInt("CurrentCar") == 8
                || PlayerPrefs.GetInt("CurrentCar") == 12)
            {
                spawnPoint.position = new Vector3(53, 3.95f, 205f);
                InstantiatedCar = Instantiate(CarsPrefabs[PlayerPrefs.GetInt("CurrentCar")], spawnPoint.position, spawnPoint.rotation);
                InstantiatedCar.AddComponent<RCC_EnterExitCar>();
            }
            if (PlayerPrefs.GetInt("CurrentCar") == 1)
            {
                spawnPoint.position = new Vector3(53, 3.95f, 205f);
                InstantiatedCar = Instantiate(CathingLoadFiles.manage.hot_rodd_ab, spawnPoint.position, spawnPoint.rotation);
                InstantiatedCar.GetComponent<Rigidbody>().isKinematic = false;
                InstantiatedCar.AddComponent<RCC_EnterExitCar>();
            }

            if (PlayerPrefs.GetInt("CurrentCar") == 2)
            {

                spawnPoint.position = new Vector3(53, 3.95f, 205f);
                InstantiatedCar = Instantiate(CathingLoadFiles.manage.buggy_gtr_ab, spawnPoint.position, spawnPoint.rotation);
                InstantiatedCar.GetComponent<Rigidbody>().isKinematic = false;
                InstantiatedCar.AddComponent<RCC_EnterExitCar>();
            }
            if (PlayerPrefs.GetInt("CurrentCar") == 3)
            {

                spawnPoint.position = new Vector3(53, 3.95f, 205f);
                InstantiatedCar = Instantiate(CathingLoadFiles.manage.gt500_ab, spawnPoint.position, spawnPoint.rotation);
                InstantiatedCar.GetComponent<Rigidbody>().isKinematic = false;
                InstantiatedCar.AddComponent<RCC_EnterExitCar>();
            }

            if (PlayerPrefs.GetInt("CurrentCar") == 4)
            {

                spawnPoint.position = new Vector3(53, 3.95f, 205f);
                InstantiatedCar = Instantiate(CarsPrefabs[PlayerPrefs.GetInt("CurrentCar")], spawnPoint.position, spawnPoint.rotation);
                //InstantiatedCar = Instantiate(CathingLoadFiles.manage.rasta_ab, spawnPoint.position, spawnPoint.rotation);
                InstantiatedCar.GetComponent<Rigidbody>().isKinematic = false;
                InstantiatedCar.AddComponent<RCC_EnterExitCar>();
            }
            if (PlayerPrefs.GetInt("CurrentCar") == 6)
            {
                spawnPoint.position = new Vector3(53, 3.95f, 205f);
                InstantiatedCar = Instantiate(CathingLoadFiles.manage.i8_ab, spawnPoint.position, spawnPoint.rotation);
                InstantiatedCar.GetComponent<Rigidbody>().isKinematic = false;
                InstantiatedCar.AddComponent<RCC_EnterExitCar>();
            }
            if (PlayerPrefs.GetInt("CurrentCar") == 7)
            {

                spawnPoint.position = new Vector3(53, 3.95f, 205f);
                InstantiatedCar = Instantiate(CathingLoadFiles.manage.lambo_ab, spawnPoint.position, spawnPoint.rotation);
                InstantiatedCar.AddComponent<RCC_EnterExitCar>();
            }
            if (PlayerPrefs.GetInt("CurrentCar") == 9)
            {

                spawnPoint.position = new Vector3(53, 3.95f, 205f);
                InstantiatedCar = Instantiate(CathingLoadFiles.manage.modelT_ab, spawnPoint.position, spawnPoint.rotation);
                InstantiatedCar.AddComponent<RCC_EnterExitCar>();
            }
            if (PlayerPrefs.GetInt("CurrentCar") == 10)
            {

                spawnPoint.position = new Vector3(53, 3.95f, 205f);
                InstantiatedCar = Instantiate(CathingLoadFiles.manage.townCar_ab, spawnPoint.position, spawnPoint.rotation);
                InstantiatedCar.GetComponent<Rigidbody>().isKinematic = false;
                InstantiatedCar.AddComponent<RCC_EnterExitCar>();
            }

            if (PlayerPrefs.GetInt("CurrentCar") == 11)
            {
                spawnPoint.position = new Vector3(53, 5.10f, 205f);
                InstantiatedCar = Instantiate(CathingLoadFiles.manage.ikarus, spawnPoint.position, spawnPoint.rotation);
                InstantiatedCar.AddComponent<RCC_EnterExitCar>();
            }

            if (PlayerPrefs.GetInt("CurrentCar") == 13)
            {
                spawnPoint.position = new Vector3(53, 5.10f, 205f);
                InstantiatedCar = Instantiate(CathingLoadFiles.manage.rr_ab, spawnPoint.position, spawnPoint.rotation);
                InstantiatedCar.AddComponent<RCC_EnterExitCar>();
            }

       //     if (PlayerPrefs.GetInt("CurrentCar") == 14)
       //     {
       //         spawnPoint.position = new Vector3(53, 5.10f, 205f);
       //         InstantiatedCar = Instantiate(CathingLoadFiles.manage.vetty_ab, spawnPoint.position, spawnPoint.rotation);
       //         InstantiatedCar.AddComponent<RCC_EnterExitCar>();
       //     }
        }

        if (SceneManager.GetActiveScene().name == "level_lap6" && !MainMenuManager.manage.isAllvsYou)
        {
            PlayerPrefs.SetInt("Rating", PlayerPrefs.GetInt("Rating") + 2);
            if (PlayerPrefs.GetInt("CurrentCar") == 0 
                 || PlayerPrefs.GetInt("CurrentCar") == 5 || PlayerPrefs.GetInt("CurrentCar") == 8 || PlayerPrefs.GetInt("CurrentCar") == 12
                 )
            {
                spawnPoint.position = new Vector3(53, 3.95f, 205f);
                InstantiatedCar = Lean.Pool.LeanPool.Spawn(CarsPrefabs[PlayerPrefs.GetInt("CurrentCar")], spawnPoint.position, spawnPoint.rotation);
                InstantiatedCar.AddComponent<RCC_EnterExitCar>();
            }
            if (PlayerPrefs.GetInt("CurrentCar") == 1)
            {
                spawnPoint.position = new Vector3(53, 3.95f, 205f);
                InstantiatedCar = Lean.Pool.LeanPool.Spawn(CathingLoadFiles.manage.hot_rodd_ab, spawnPoint.position, spawnPoint.rotation);
                InstantiatedCar.GetComponent<Rigidbody>().isKinematic = false;
                InstantiatedCar.AddComponent<RCC_EnterExitCar>();
            }
            if (PlayerPrefs.GetInt("CurrentCar") == 2)
            {
                spawnPoint.position = new Vector3(53, 3.95f, 205f);
                InstantiatedCar = Lean.Pool.LeanPool.Spawn(CathingLoadFiles.manage.buggy_gtr_ab, spawnPoint.position, spawnPoint.rotation);
                InstantiatedCar.GetComponent<Rigidbody>().isKinematic = false;
                InstantiatedCar.AddComponent<RCC_EnterExitCar>();
            }
            if (PlayerPrefs.GetInt("CurrentCar") == 3)
            {
                spawnPoint.position = new Vector3(53, 3.95f, 205f);
                InstantiatedCar = Lean.Pool.LeanPool.Spawn(CathingLoadFiles.manage.gt500_ab, spawnPoint.position, spawnPoint.rotation);
                InstantiatedCar.GetComponent<Rigidbody>().isKinematic = false;
                InstantiatedCar.AddComponent<RCC_EnterExitCar>();
            }

            if (PlayerPrefs.GetInt("CurrentCar") == 4)
            {
                spawnPoint.position = new Vector3(53, 3.95f, 205f);
                //InstantiatedCar = Lean.Pool.LeanPool.Spawn(CathingLoadFiles.manage.rasta_ab, spawnPoint.position, spawnPoint.rotation);
                InstantiatedCar = Lean.Pool.LeanPool.Spawn(CarsPrefabs[PlayerPrefs.GetInt("CurrentCar")], spawnPoint.position, spawnPoint.rotation);
                InstantiatedCar.GetComponent<Rigidbody>().isKinematic = false;
                InstantiatedCar.AddComponent<RCC_EnterExitCar>();
            }

            if (PlayerPrefs.GetInt("CurrentCar") == 6)
            {
                spawnPoint.position = new Vector3(53, 3.95f, 205f);
                InstantiatedCar = Lean.Pool.LeanPool.Spawn(CathingLoadFiles.manage.i8_ab, spawnPoint.position, spawnPoint.rotation);
                InstantiatedCar.GetComponent<Rigidbody>().isKinematic = false;
                InstantiatedCar.AddComponent<RCC_EnterExitCar>();
            }
            if (PlayerPrefs.GetInt("CurrentCar") == 7)
            {

                spawnPoint.position = new Vector3(53, 3.95f, 205f);
                InstantiatedCar = Lean.Pool.LeanPool.Spawn(CathingLoadFiles.manage.lambo_ab, spawnPoint.position, spawnPoint.rotation);
                InstantiatedCar.AddComponent<RCC_EnterExitCar>();
            }


            if (PlayerPrefs.GetInt("CurrentCar") == 9)
            {

                spawnPoint.position = new Vector3(53, 3.95f, 205f);
                InstantiatedCar = Lean.Pool.LeanPool.Spawn(CathingLoadFiles.manage.modelT_ab, spawnPoint.position, spawnPoint.rotation);
                InstantiatedCar.AddComponent<RCC_EnterExitCar>();
            }

            if (PlayerPrefs.GetInt("CurrentCar") == 10)
            {

                spawnPoint.position = new Vector3(53, 3.95f, 205f);
                InstantiatedCar = Lean.Pool.LeanPool.Spawn(CathingLoadFiles.manage.townCar_ab, spawnPoint.position, spawnPoint.rotation);
                InstantiatedCar.GetComponent<Rigidbody>().isKinematic = false;
                InstantiatedCar.AddComponent<RCC_EnterExitCar>();
            }

            if (PlayerPrefs.GetInt("CurrentCar") == 11)
            {
                spawnPoint.position = new Vector3(53, 5.10f, 205f);
                InstantiatedCar = Lean.Pool.LeanPool.Spawn(CathingLoadFiles.manage.ikarus, spawnPoint.position, spawnPoint.rotation);
                InstantiatedCar.AddComponent<RCC_EnterExitCar>();
            }

            if (PlayerPrefs.GetInt("CurrentCar") == 13)
            {
                spawnPoint.position = new Vector3(53, 5.10f, 205f);
                InstantiatedCar = Instantiate(CathingLoadFiles.manage.rr_ab, spawnPoint.position, spawnPoint.rotation);
                InstantiatedCar.AddComponent<RCC_EnterExitCar>();
            }

           // if (PlayerPrefs.GetInt("CurrentCar") == 14)
          //  {
           //     spawnPoint.position = new Vector3(53, 5.10f, 205f);
           //     InstantiatedCar = Instantiate(CathingLoadFiles.manage.vetty_ab, spawnPoint.position, spawnPoint.rotation);
           //     InstantiatedCar.AddComponent<RCC_EnterExitCar>();
           // }
        }

        if (SceneManager.GetActiveScene().name == "level_top_speed_test")
        {
            PlayerPrefs.SetInt("Rating", PlayerPrefs.GetInt("Rating") + 2);
            if (PlayerPrefs.GetInt("CurrentCar") == 1)
            {
                if (MainMenuManager.manage.isQuickRace)
                {
                    if (PlayerPrefs.GetInt("myCar") == 1)
                    {
                        spawnPoint.position = new Vector3(-97.56f, 0, 2171.69f);
                        InstantiatedCar = Lean.Pool.LeanPool.Spawn(CathingLoadFiles.manage.hot_rodd_ab, spawnPoint.position, spawnPoint.rotation);
                        InstantiatedCar.GetComponent<Rigidbody>().isKinematic = false;
                        InstantiatedCar.AddComponent<RCC_EnterExitCar>();
                    }
                    else
                    {
                        spawnPoint.position = new Vector3(-97.39f, 0.5f, 2171.69f);
                        InstantiatedCar = Lean.Pool.LeanPool.Spawn(CarsPrefabs[0], spawnPoint.position, spawnPoint.rotation);
                        InstantiatedCar.AddComponent<RCC_EnterExitCar>();
                    }
                } else
                {
                    spawnPoint.position = new Vector3(-97.56f, 0, 2171.69f);
                    InstantiatedCar = Lean.Pool.LeanPool.Spawn(CathingLoadFiles.manage.hot_rodd_ab, spawnPoint.position, spawnPoint.rotation);
                    InstantiatedCar.GetComponent<Rigidbody>().isKinematic = false;
                    InstantiatedCar.AddComponent<RCC_EnterExitCar>();
                }
            }
            if (PlayerPrefs.GetInt("CurrentCar") == 2)
            {
                if (MainMenuManager.manage.isQuickRace)
                {

                    if (PlayerPrefs.GetInt("myCar") == 1)
                    {
                        spawnPoint.position = new Vector3(-97.39f, 2f, 2171.69f);
                        InstantiatedCar = Lean.Pool.LeanPool.Spawn(CathingLoadFiles.manage.buggy_gtr_ab, spawnPoint.position, spawnPoint.rotation);
                        InstantiatedCar.GetComponent<Rigidbody>().isKinematic = false;
                        InstantiatedCar.AddComponent<RCC_EnterExitCar>();
                    }
                    else
                    {
                        spawnPoint.position = new Vector3(-97.39f, 0.5f, 2171.69f);
                        InstantiatedCar = Lean.Pool.LeanPool.Spawn(CarsPrefabs[0], spawnPoint.position, spawnPoint.rotation);
                        InstantiatedCar.AddComponent<RCC_EnterExitCar>();
                    }
                } else
                {
                    spawnPoint.position = new Vector3(-97.39f, 2f, 2171.69f);
                    InstantiatedCar = Lean.Pool.LeanPool.Spawn(CathingLoadFiles.manage.buggy_gtr_ab, spawnPoint.position, spawnPoint.rotation);
                    InstantiatedCar.GetComponent<Rigidbody>().isKinematic = false;
                    InstantiatedCar.AddComponent<RCC_EnterExitCar>();
                }
            }
            if (PlayerPrefs.GetInt("CurrentCar") == 3)
            {
                if (MainMenuManager.manage.isQuickRace)
                {
                    if (PlayerPrefs.GetInt("myCar") == 1)
                    {
                        spawnPoint.position = new Vector3(-97.39f, 2f, 2171.69f);
                        InstantiatedCar = Lean.Pool.LeanPool.Spawn(CathingLoadFiles.manage.gt500_ab, spawnPoint.position, spawnPoint.rotation);
                        InstantiatedCar.GetComponent<Rigidbody>().isKinematic = false;
                        InstantiatedCar.AddComponent<RCC_EnterExitCar>();
                    } else
                    {
                        spawnPoint.position = new Vector3(-97.39f, 0.5f, 2171.69f);
                        InstantiatedCar = Lean.Pool.LeanPool.Spawn(CarsPrefabs[0], spawnPoint.position, spawnPoint.rotation);
                        InstantiatedCar.AddComponent<RCC_EnterExitCar>();
                    } 

                } else
                {
                    spawnPoint.position = new Vector3(-97.39f, 2f, 2171.69f);
                    InstantiatedCar = Lean.Pool.LeanPool.Spawn(CathingLoadFiles.manage.gt500_ab, spawnPoint.position, spawnPoint.rotation);
                    InstantiatedCar.GetComponent<Rigidbody>().isKinematic = false;
                    InstantiatedCar.AddComponent<RCC_EnterExitCar>();
                } 
            }

          //  if (PlayerPrefs.GetInt("CurrentCar") == 4)
           // {
           //     if (MainMenuManager.manage.isQuickRace)
            //    {
            //        if (PlayerPrefs.GetInt("myCar") == 1)
               //     {
                //        spawnPoint.position = new Vector3(-97.39f, 2f, 2171.69f);
                //        InstantiatedCar = Lean.Pool.LeanPool.Spawn(CathingLoadFiles.manage.rasta_ab, spawnPoint.position, spawnPoint.rotation);
                //        InstantiatedCar.GetComponent<Rigidbody>().isKinematic = false;
                //        InstantiatedCar.AddComponent<RCC_EnterExitCar>();
                //    }
                 //   else
                 //   {
                 //       spawnPoint.position = new Vector3(-97.39f, 0.5f, 2171.69f);
                 //       InstantiatedCar = Lean.Pool.LeanPool.Spawn(CarsPrefabs[0], spawnPoint.position, spawnPoint.rotation);
                 //       InstantiatedCar.AddComponent<RCC_EnterExitCar>();
                  //  }

               // }
               // else
              //  {
               //     spawnPoint.position = new Vector3(-97.39f, 2f, 2171.69f);
               //     InstantiatedCar = Lean.Pool.LeanPool.Spawn(CathingLoadFiles.manage.rasta_ab, spawnPoint.position, spawnPoint.rotation);
               //     InstantiatedCar.GetComponent<Rigidbody>().isKinematic = false;
               //     InstantiatedCar.AddComponent<RCC_EnterExitCar>();
               // }
           // }

            if (PlayerPrefs.GetInt("CurrentCar") == 6)
            {
                if (MainMenuManager.manage.isQuickRace)
                {
                    if (PlayerPrefs.GetInt("myCar") == 1)
                    {
                        spawnPoint.position = new Vector3(-97.39f, 2.80f, 2171.69f);
                        InstantiatedCar = Lean.Pool.LeanPool.Spawn(CathingLoadFiles.manage.i8_ab, spawnPoint.position, spawnPoint.rotation);
                        InstantiatedCar.AddComponent<RCC_EnterExitCar>();
                    }

                    else
                    {
                        spawnPoint.position = new Vector3(-97.39f, 0.5f, 2171.69f);
                        InstantiatedCar = Lean.Pool.LeanPool.Spawn(CarsPrefabs[0], spawnPoint.position, spawnPoint.rotation);
                        InstantiatedCar.AddComponent<RCC_EnterExitCar>();
                    }
                }
                else
                {
                    spawnPoint.position = new Vector3(-97.39f, 2.80f, 2171.69f);
                    InstantiatedCar = Lean.Pool.LeanPool.Spawn(CathingLoadFiles.manage.i8_ab, spawnPoint.position, spawnPoint.rotation);
                    InstantiatedCar.AddComponent<RCC_EnterExitCar>();
                }
            }

            if (PlayerPrefs.GetInt("CurrentCar") == 7)
            {
                if (MainMenuManager.manage.isQuickRace)
                {
                    if (PlayerPrefs.GetInt("myCar") == 1)
                    {
                        spawnPoint.position = new Vector3(-97.39f, 2.80f, 2171.69f);
                        InstantiatedCar = Lean.Pool.LeanPool.Spawn(CathingLoadFiles.manage.lambo_ab, spawnPoint.position, spawnPoint.rotation);
                        InstantiatedCar.AddComponent<RCC_EnterExitCar>();
                    }

                    else
                    {
                        spawnPoint.position = new Vector3(-97.39f, 0.5f, 2171.69f);
                        InstantiatedCar = Lean.Pool.LeanPool.Spawn(CarsPrefabs[0], spawnPoint.position, spawnPoint.rotation);
                        InstantiatedCar.AddComponent<RCC_EnterExitCar>();
                    }
                }
                else
                {
                    spawnPoint.position = new Vector3(-97.39f, 2.80f, 2171.69f);
                    InstantiatedCar = Lean.Pool.LeanPool.Spawn(CathingLoadFiles.manage.lambo_ab, spawnPoint.position, spawnPoint.rotation);
                    InstantiatedCar.AddComponent<RCC_EnterExitCar>();
                }
            }

            if (PlayerPrefs.GetInt("CurrentCar") == 9)
            {
                if (MainMenuManager.manage.isQuickRace)
                {
                    if (PlayerPrefs.GetInt("myCar") == 1)
                    {
                        spawnPoint.position = new Vector3(-97.39f, 2.80f, 2171.69f);
                        InstantiatedCar = Lean.Pool.LeanPool.Spawn(CathingLoadFiles.manage.modelT_ab, spawnPoint.position, spawnPoint.rotation);
                        InstantiatedCar.AddComponent<RCC_EnterExitCar>();
                    }

                    else
                    {
                        spawnPoint.position = new Vector3(-97.39f, 0.5f, 2171.69f);
                        InstantiatedCar = Lean.Pool.LeanPool.Spawn(CarsPrefabs[0], spawnPoint.position, spawnPoint.rotation);
                        InstantiatedCar.AddComponent<RCC_EnterExitCar>();
                    }
                } else
                {
                    spawnPoint.position = new Vector3(-97.39f, 2.80f, 2171.69f);
                    InstantiatedCar = Lean.Pool.LeanPool.Spawn(CathingLoadFiles.manage.modelT_ab, spawnPoint.position, spawnPoint.rotation);
                    InstantiatedCar.AddComponent<RCC_EnterExitCar>();
                }
            }

            if (PlayerPrefs.GetInt("CurrentCar") == 10)
            {
                if (MainMenuManager.manage.isQuickRace)
                {
                    if (PlayerPrefs.GetInt("myCar") == 1)
                    {
                        InstantiatedCar = Lean.Pool.LeanPool.Spawn(CathingLoadFiles.manage.townCar_ab, spawnPoint.position, spawnPoint.rotation);
                        InstantiatedCar.GetComponent<Rigidbody>().isKinematic = false;
                        InstantiatedCar.AddComponent<RCC_EnterExitCar>();
                    } else
                    {
                        spawnPoint.position = new Vector3(-97.39f, 2.80f, 2171.69f);
                        InstantiatedCar = Lean.Pool.LeanPool.Spawn(CarsPrefabs[0], spawnPoint.position, spawnPoint.rotation);
                        InstantiatedCar.AddComponent<RCC_EnterExitCar>();
                    }
                } else
                {
                    InstantiatedCar = Lean.Pool.LeanPool.Spawn(CathingLoadFiles.manage.townCar_ab, spawnPoint.position, spawnPoint.rotation);
                    InstantiatedCar.GetComponent<Rigidbody>().isKinematic = false;
                    InstantiatedCar.AddComponent<RCC_EnterExitCar>();
                }
            }
            if (PlayerPrefs.GetInt("CurrentCar") == 11)
            {
                if (MainMenuManager.manage.isQuickRace)
                {
                    if (PlayerPrefs.GetInt("myCar") == 1)
                    {
                        InstantiatedCar = Lean.Pool.LeanPool.Spawn(CathingLoadFiles.manage.ikarus, spawnPoint.position, spawnPoint.rotation);
                        InstantiatedCar.AddComponent<RCC_EnterExitCar>();
                    } else
                    {
                        InstantiatedCar = Lean.Pool.LeanPool.Spawn(CarsPrefabs[0], spawnPoint.position, spawnPoint.rotation);
                        InstantiatedCar.AddComponent<RCC_EnterExitCar>();
                    }
                } else
                {
                    InstantiatedCar = Lean.Pool.LeanPool.Spawn(CathingLoadFiles.manage.ikarus, spawnPoint.position, spawnPoint.rotation);
                    InstantiatedCar.AddComponent<RCC_EnterExitCar>();
                }
            }

            if (PlayerPrefs.GetInt("CurrentCar") == 13)
            {
                if (MainMenuManager.manage.isQuickRace)
                {
                    if (PlayerPrefs.GetInt("myCar") == 1)
                    {
                        InstantiatedCar = Lean.Pool.LeanPool.Spawn(CathingLoadFiles.manage.rr_ab, spawnPoint.position, spawnPoint.rotation);
                        InstantiatedCar.AddComponent<RCC_EnterExitCar>();
                    }
                    else
                    {
                        InstantiatedCar = Lean.Pool.LeanPool.Spawn(CarsPrefabs[0], spawnPoint.position, spawnPoint.rotation);
                        InstantiatedCar.AddComponent<RCC_EnterExitCar>();
                    }
                }
                else
                {
                    InstantiatedCar = Lean.Pool.LeanPool.Spawn(CathingLoadFiles.manage.rr_ab, spawnPoint.position, spawnPoint.rotation);
                    InstantiatedCar.AddComponent<RCC_EnterExitCar>();
                }
            }

            if (PlayerPrefs.GetInt("CurrentCar") == 14)
            {
                if (MainMenuManager.manage.isQuickRace)
                {
                    if (PlayerPrefs.GetInt("myCar") == 1)
                    {
                        InstantiatedCar = Lean.Pool.LeanPool.Spawn(CathingLoadFiles.manage.vetty_ab, spawnPoint.position, spawnPoint.rotation);
                        InstantiatedCar.AddComponent<RCC_EnterExitCar>();
                    }
                    else
                    {
                        InstantiatedCar = Lean.Pool.LeanPool.Spawn(CarsPrefabs[0], spawnPoint.position, spawnPoint.rotation);
                        InstantiatedCar.AddComponent<RCC_EnterExitCar>();
                    }
                }
                else
                {
                    InstantiatedCar = Lean.Pool.LeanPool.Spawn(CathingLoadFiles.manage.vetty_ab, spawnPoint.position, spawnPoint.rotation);
                    InstantiatedCar.AddComponent<RCC_EnterExitCar>();
                }
            }

            else if (PlayerPrefs.GetInt("CurrentCar") == 0 || PlayerPrefs.GetInt("CurrentCar") == 4
                || PlayerPrefs.GetInt("CurrentCar") == 5 || PlayerPrefs.GetInt("CurrentCar") == 8
                || PlayerPrefs.GetInt("CurrentCar") == 12)
            {
                if (MainMenuManager.manage.isQuickRace)
                {
                    if (PlayerPrefs.GetInt("myCar") == 1)
                    {
                        spawnPoint.position = new Vector3(-97.39f, 0.5f, 2171.69f);
                        InstantiatedCar = Lean.Pool.LeanPool.Spawn(CarsPrefabs[PlayerPrefs.GetInt("CurrentCar")], spawnPoint.position, spawnPoint.rotation);
                        InstantiatedCar.AddComponent<RCC_EnterExitCar>();
                    } else
                    {
                        spawnPoint.position = new Vector3(-97.39f, 0.5f, 2171.69f);
                        InstantiatedCar = Lean.Pool.LeanPool.Spawn(CarsPrefabs[0], spawnPoint.position, spawnPoint.rotation);
                        InstantiatedCar.AddComponent<RCC_EnterExitCar>();
                    }
                } else
                {
                    spawnPoint.position = new Vector3(-97.39f, 0.5f, 2171.69f);
                    InstantiatedCar = Lean.Pool.LeanPool.Spawn(CarsPrefabs[PlayerPrefs.GetInt("CurrentCar")], spawnPoint.position, spawnPoint.rotation);
                    InstantiatedCar.AddComponent<RCC_EnterExitCar>();
                }
            }
        }

        if (SceneManager.GetActiveScene().name == "city_single")
        {
            PlayerPrefs.SetInt("Rating", PlayerPrefs.GetInt("Rating") + 2);
                if (PlayerPrefs.GetInt("CurrentCar") == 0 || PlayerPrefs.GetInt("CurrentCar") == 4
                     || PlayerPrefs.GetInt("CurrentCar") == 5 || PlayerPrefs.GetInt("CurrentCar") == 8 || PlayerPrefs.GetInt("CurrentCar") == 12)
                {
                    InstantiatedCar = Lean.Pool.LeanPool.Spawn(CarsPrefabs[PlayerPrefs.GetInt("CurrentCar")], new Vector3(Random.Range(-755f, -757f), Random.Range(2f, 4f), Random.Range(-165f, -184f)), spawnPoint.rotation);
                    InstantiatedCar.AddComponent<RCC_EnterExitCar>();
                }
                if (PlayerPrefs.GetInt("CurrentCar") == 1)
                {
                    InstantiatedCar = Lean.Pool.LeanPool.Spawn(CathingLoadFiles.manage.hot_rodd_ab, new Vector3(Random.Range(-755f, -757f), Random.Range(2f, 4f), Random.Range(-165f, -184f)), spawnPoint.rotation);
                    InstantiatedCar.GetComponent<Rigidbody>().isKinematic = false;
                    InstantiatedCar.AddComponent<RCC_EnterExitCar>();
                }
                if (PlayerPrefs.GetInt("CurrentCar") == 2)
                {
                    InstantiatedCar = Lean.Pool.LeanPool.Spawn(CathingLoadFiles.manage.buggy_gtr_ab, new Vector3(Random.Range(-755f, -757f), Random.Range(2f, 4f), Random.Range(-165f, -184f)), spawnPoint.rotation);
                    InstantiatedCar.GetComponent<Rigidbody>().isKinematic = false;
                    InstantiatedCar.AddComponent<RCC_EnterExitCar>();
                }
                if (PlayerPrefs.GetInt("CurrentCar") == 3)
                {
                    InstantiatedCar = Lean.Pool.LeanPool.Spawn(CathingLoadFiles.manage.gt500_ab, new Vector3(Random.Range(-755f, -757f), Random.Range(2f, 4f), Random.Range(-165f, -184f)), spawnPoint.rotation);
                    InstantiatedCar.GetComponent<Rigidbody>().isKinematic = false;
                    InstantiatedCar.AddComponent<RCC_EnterExitCar>();
                }

       //     if (PlayerPrefs.GetInt("CurrentCar") == 4)
      //      {
       //         InstantiatedCar = Lean.Pool.LeanPool.Spawn(CathingLoadFiles.manage.rasta_ab, new Vector3(Random.Range(-755f, -757f), Random.Range(2f, 4f), Random.Range(-165f, -184f)), spawnPoint.rotation);
       //         InstantiatedCar.GetComponent<Rigidbody>().isKinematic = false;
       //         InstantiatedCar.AddComponent<RCC_EnterExitCar>();
        //    }
        //

            if (PlayerPrefs.GetInt("CurrentCar") == 6)
                {

                    InstantiatedCar = Lean.Pool.LeanPool.Spawn(CathingLoadFiles.manage.i8_ab, new Vector3(Random.Range(-755f, -757f), Random.Range(2f, 4f), Random.Range(-165f, -184f)), spawnPoint.rotation);
                    InstantiatedCar.GetComponent<Rigidbody>().isKinematic = false;
                    InstantiatedCar.AddComponent<RCC_EnterExitCar>();
                }
                if (PlayerPrefs.GetInt("CurrentCar") == 7)
                {

                    InstantiatedCar = Lean.Pool.LeanPool.Spawn(CathingLoadFiles.manage.lambo_ab, new Vector3(Random.Range(-755f, -757f), Random.Range(2f, 4f), Random.Range(-165f, -184f)), spawnPoint.rotation);
                    InstantiatedCar.AddComponent<RCC_EnterExitCar>();
                }


                if (PlayerPrefs.GetInt("CurrentCar") == 9)
                {

                    InstantiatedCar = Lean.Pool.LeanPool.Spawn(CathingLoadFiles.manage.modelT_ab, new Vector3(Random.Range(-755f, -757f), Random.Range(2f, 4f), Random.Range(-165f, -184f)), spawnPoint.rotation);
                    InstantiatedCar.AddComponent<RCC_EnterExitCar>();
                }

                if (PlayerPrefs.GetInt("CurrentCar") == 10)
                {

                    InstantiatedCar = Lean.Pool.LeanPool.Spawn(CathingLoadFiles.manage.townCar_ab, new Vector3(Random.Range(-755f, -757f), Random.Range(2f, 4f), Random.Range(-165f, -184f)), spawnPoint.rotation);
                    InstantiatedCar.GetComponent<Rigidbody>().isKinematic = false;
                    InstantiatedCar.AddComponent<RCC_EnterExitCar>();
                }

                if (PlayerPrefs.GetInt("CurrentCar") == 11)
                {

                    InstantiatedCar = Lean.Pool.LeanPool.Spawn(CathingLoadFiles.manage.ikarus, new Vector3(Random.Range(-755f, -757f), Random.Range(2f, 4f), Random.Range(-165f, -184f)), spawnPoint.rotation);
                    InstantiatedCar.AddComponent<RCC_EnterExitCar>();
                }

            if (PlayerPrefs.GetInt("CurrentCar") == 13)
            {

                InstantiatedCar = Lean.Pool.LeanPool.Spawn(CathingLoadFiles.manage.rr_ab, new Vector3(Random.Range(-755f, -757f), Random.Range(2f, 4f), Random.Range(-165f, -184f)), spawnPoint.rotation);
                InstantiatedCar.AddComponent<RCC_EnterExitCar>();
            }

            //if (PlayerPrefs.GetInt("CurrentCar") == 14)
           // {

             //   InstantiatedCar = Lean.Pool.LeanPool.Spawn(CathingLoadFiles.manage.vetty_ab, new Vector3(Random.Range(-755f, -757f), Random.Range(2f, 4f), Random.Range(-165f, -184f)), spawnPoint.rotation);
             //   InstantiatedCar.AddComponent<RCC_EnterExitCar>();
            //}
        }
           
        if (SceneManager.GetActiveScene().name == "_arena_1")
        {
            PlayerPrefs.SetInt("Rating", PlayerPrefs.GetInt("Rating") + 2);
            //InsideOutsideCar.manage.OutFromCarButton.SetActive(false);

            if (PlayerPrefs.GetInt("CurrentCar") == 0
                || PlayerPrefs.GetInt("CurrentCar") == 5 || PlayerPrefs.GetInt("CurrentCar") == 8
                || PlayerPrefs.GetInt("CurrentCar") == 12)
            {
                spawnPoint.position = new Vector3(-66.5f, 3.95f, -66.9f);
                InstantiatedCar = Instantiate(CarsPrefabs[PlayerPrefs.GetInt("CurrentCar")], spawnPoint.position, spawnPoint.rotation);
                InstantiatedCar.AddComponent<RCC_EnterExitCar>();
            }
            if (PlayerPrefs.GetInt("CurrentCar") == 1)
            {
                spawnPoint.position = new Vector3(-66.5f, 3.95f, -66.9f);
                InstantiatedCar = Instantiate(CathingLoadFiles.manage.hot_rodd_ab, spawnPoint.position, spawnPoint.rotation);
                InstantiatedCar.GetComponent<Rigidbody>().isKinematic = false;
                InstantiatedCar.AddComponent<RCC_EnterExitCar>();
            }

            if (PlayerPrefs.GetInt("CurrentCar") == 2)
            {

                spawnPoint.position = new Vector3(-66.5f, 3.95f, -66.9f);
                InstantiatedCar = Instantiate(CathingLoadFiles.manage.buggy_gtr_ab, spawnPoint.position, spawnPoint.rotation);
                InstantiatedCar.GetComponent<Rigidbody>().isKinematic = false;
                InstantiatedCar.AddComponent<RCC_EnterExitCar>();
            }
            if (PlayerPrefs.GetInt("CurrentCar") == 3)
            {

                spawnPoint.position = new Vector3(-66.5f, 3.95f, -66.9f);
                InstantiatedCar = Instantiate(CathingLoadFiles.manage.gt500_ab, spawnPoint.position, spawnPoint.rotation);
                InstantiatedCar.GetComponent<Rigidbody>().isKinematic = false;
                InstantiatedCar.AddComponent<RCC_EnterExitCar>();
            }

            if (PlayerPrefs.GetInt("CurrentCar") == 4)
            {

                spawnPoint.position = new Vector3(-66.5f, 3.95f, -66.9f);
                InstantiatedCar = Instantiate(CarsPrefabs[PlayerPrefs.GetInt("CurrentCar")], spawnPoint.position, spawnPoint.rotation);
                //InstantiatedCar = Instantiate(CathingLoadFiles.manage.rasta_ab, spawnPoint.position, spawnPoint.rotation);
                InstantiatedCar.GetComponent<Rigidbody>().isKinematic = false;
                InstantiatedCar.AddComponent<RCC_EnterExitCar>();
            }
            if (PlayerPrefs.GetInt("CurrentCar") == 6)
            {
                spawnPoint.position = new Vector3(-66.5f, 3.95f, -66.9f);
                InstantiatedCar = Instantiate(CathingLoadFiles.manage.i8_ab, spawnPoint.position, spawnPoint.rotation);
                InstantiatedCar.GetComponent<Rigidbody>().isKinematic = false;
                InstantiatedCar.AddComponent<RCC_EnterExitCar>();
            }
            if (PlayerPrefs.GetInt("CurrentCar") == 7)
            {

                spawnPoint.position = new Vector3(-66.5f, 3.95f, -66.9f);
                InstantiatedCar = Instantiate(CathingLoadFiles.manage.lambo_ab, spawnPoint.position, spawnPoint.rotation);
                InstantiatedCar.AddComponent<RCC_EnterExitCar>();
            }
            if (PlayerPrefs.GetInt("CurrentCar") == 9)
            {

                spawnPoint.position = new Vector3(-66.5f, 3.95f, -66.9f);
                InstantiatedCar = Instantiate(CathingLoadFiles.manage.modelT_ab, spawnPoint.position, spawnPoint.rotation);
                InstantiatedCar.AddComponent<RCC_EnterExitCar>();
            }
            if (PlayerPrefs.GetInt("CurrentCar") == 10)
            {

                spawnPoint.position = new Vector3(-66.5f, 3.95f, -66.9f);
                InstantiatedCar = Instantiate(CathingLoadFiles.manage.townCar_ab, spawnPoint.position, spawnPoint.rotation);
                InstantiatedCar.GetComponent<Rigidbody>().isKinematic = false;
                InstantiatedCar.AddComponent<RCC_EnterExitCar>();
            }

            if (PlayerPrefs.GetInt("CurrentCar") == 11)
            {
                spawnPoint.position = new Vector3(-66.5f, 3.95f, -66.9f);
                InstantiatedCar = Instantiate(CathingLoadFiles.manage.ikarus, spawnPoint.position, spawnPoint.rotation);
                InstantiatedCar.AddComponent<RCC_EnterExitCar>();
            }

            if (PlayerPrefs.GetInt("CurrentCar") == 13)
            {
                spawnPoint.position = new Vector3(-66.5f, 3.95f, -66.9f);
                InstantiatedCar = Instantiate(CathingLoadFiles.manage.rr_ab, spawnPoint.position, spawnPoint.rotation);
                InstantiatedCar.AddComponent<RCC_EnterExitCar>();
            }

            //     if (PlayerPrefs.GetInt("CurrentCar") == 14)
            //     {
            //         spawnPoint.position = new Vector3(53, 5.10f, 205f);
            //         InstantiatedCar = Instantiate(CathingLoadFiles.manage.vetty_ab, spawnPoint.position, spawnPoint.rotation);
            //         InstantiatedCar.AddComponent<RCC_EnterExitCar>();
            //     }
        }

        if (SceneManager.GetActiveScene().name == "_arena_2")
        {
            PlayerPrefs.SetInt("Rating", PlayerPrefs.GetInt("Rating") + 2);
            //InsideOutsideCar.manage.OutFromCarButton.SetActive(false);

            if (PlayerPrefs.GetInt("CurrentCar") == 0
                || PlayerPrefs.GetInt("CurrentCar") == 5 || PlayerPrefs.GetInt("CurrentCar") == 8
                || PlayerPrefs.GetInt("CurrentCar") == 12)
            {
                spawnPoint.position = new Vector3(-29.8f, 4f, -50.21f);
                InstantiatedCar = Instantiate(CarsPrefabs[PlayerPrefs.GetInt("CurrentCar")], spawnPoint.position, spawnPoint.rotation);
                InstantiatedCar.AddComponent<RCC_EnterExitCar>();
            }
            if (PlayerPrefs.GetInt("CurrentCar") == 1)
            {
                spawnPoint.position = new Vector3(-29.8f, 4f, -50.21f);
                InstantiatedCar = Instantiate(CathingLoadFiles.manage.hot_rodd_ab, spawnPoint.position, spawnPoint.rotation);
                InstantiatedCar.GetComponent<Rigidbody>().isKinematic = false;
                InstantiatedCar.AddComponent<RCC_EnterExitCar>();
            }

            if (PlayerPrefs.GetInt("CurrentCar") == 2)
            {

                spawnPoint.position = new Vector3(-29.8f, 4f, -50.21f);
                InstantiatedCar = Instantiate(CathingLoadFiles.manage.buggy_gtr_ab, spawnPoint.position, spawnPoint.rotation);
                InstantiatedCar.GetComponent<Rigidbody>().isKinematic = false;
                InstantiatedCar.AddComponent<RCC_EnterExitCar>();
            }
            if (PlayerPrefs.GetInt("CurrentCar") == 3)
            {

                spawnPoint.position = new Vector3(-29.8f, 4f, -50.21f);
                InstantiatedCar = Instantiate(CathingLoadFiles.manage.gt500_ab, spawnPoint.position, spawnPoint.rotation);
                InstantiatedCar.GetComponent<Rigidbody>().isKinematic = false;
                InstantiatedCar.AddComponent<RCC_EnterExitCar>();
            }

            if (PlayerPrefs.GetInt("CurrentCar") == 4)
            {

                spawnPoint.position = new Vector3(-29.8f, 4f, -50.21f);
                InstantiatedCar = Instantiate(CarsPrefabs[PlayerPrefs.GetInt("CurrentCar")], spawnPoint.position, spawnPoint.rotation);
                //InstantiatedCar = Instantiate(CathingLoadFiles.manage.rasta_ab, spawnPoint.position, spawnPoint.rotation);
                InstantiatedCar.GetComponent<Rigidbody>().isKinematic = false;
                InstantiatedCar.AddComponent<RCC_EnterExitCar>();
            }
            if (PlayerPrefs.GetInt("CurrentCar") == 6)
            {
                spawnPoint.position = new Vector3(-29.8f, 4f, -50.21f);
                InstantiatedCar = Instantiate(CathingLoadFiles.manage.i8_ab, spawnPoint.position, spawnPoint.rotation);
                InstantiatedCar.GetComponent<Rigidbody>().isKinematic = false;
                InstantiatedCar.AddComponent<RCC_EnterExitCar>();
            }
            if (PlayerPrefs.GetInt("CurrentCar") == 7)
            {

                spawnPoint.position = new Vector3(-29.8f, 4f, -50.21f);
                InstantiatedCar = Instantiate(CathingLoadFiles.manage.lambo_ab, spawnPoint.position, spawnPoint.rotation);
                InstantiatedCar.AddComponent<RCC_EnterExitCar>();
            }
            if (PlayerPrefs.GetInt("CurrentCar") == 9)
            {

                spawnPoint.position = new Vector3(-29.8f, 4f, -50.21f);
                InstantiatedCar = Instantiate(CathingLoadFiles.manage.modelT_ab, spawnPoint.position, spawnPoint.rotation);
                InstantiatedCar.AddComponent<RCC_EnterExitCar>();
            }
            if (PlayerPrefs.GetInt("CurrentCar") == 10)
            {

                spawnPoint.position = new Vector3(-29.8f, 4f, -50.21f);
                InstantiatedCar = Instantiate(CathingLoadFiles.manage.townCar_ab, spawnPoint.position, spawnPoint.rotation);
                InstantiatedCar.GetComponent<Rigidbody>().isKinematic = false;
                InstantiatedCar.AddComponent<RCC_EnterExitCar>();
            }

            if (PlayerPrefs.GetInt("CurrentCar") == 11)
            {
                spawnPoint.position = new Vector3(-29.8f, 4f, -50.21f);
                InstantiatedCar = Instantiate(CathingLoadFiles.manage.ikarus, spawnPoint.position, spawnPoint.rotation);
                InstantiatedCar.AddComponent<RCC_EnterExitCar>();
            }

            if (PlayerPrefs.GetInt("CurrentCar") == 13)
            {
                spawnPoint.position = new Vector3(-29.8f, 4f, -50.21f);
                InstantiatedCar = Instantiate(CathingLoadFiles.manage.rr_ab, spawnPoint.position, spawnPoint.rotation);
                InstantiatedCar.AddComponent<RCC_EnterExitCar>();
            }

            //     if (PlayerPrefs.GetInt("CurrentCar") == 14)
            //     {
            //         spawnPoint.position = new Vector3(53, 5.10f, 205f);
            //         InstantiatedCar = Instantiate(CathingLoadFiles.manage.vetty_ab, spawnPoint.position, spawnPoint.rotation);
            //         InstantiatedCar.AddComponent<RCC_EnterExitCar>();
            //     }
        }

        if (SceneManager.GetActiveScene().name == "_arena_3")
        {
            PlayerPrefs.SetInt("Rating", PlayerPrefs.GetInt("Rating") + 2);
            //InsideOutsideCar.manage.OutFromCarButton.SetActive(false);

            if (PlayerPrefs.GetInt("CurrentCar") == 0
                || PlayerPrefs.GetInt("CurrentCar") == 5 || PlayerPrefs.GetInt("CurrentCar") == 8
                || PlayerPrefs.GetInt("CurrentCar") == 12)
            {
                spawnPoint.position = new Vector3(-1.94f, 1.2f, 13.96f);
                InstantiatedCar = Instantiate(CarsPrefabs[PlayerPrefs.GetInt("CurrentCar")], spawnPoint.position, spawnPoint.rotation);
                InstantiatedCar.AddComponent<RCC_EnterExitCar>();
            }
            if (PlayerPrefs.GetInt("CurrentCar") == 1)
            {
                spawnPoint.position = new Vector3(-1.94f, 1.2f, 13.96f);
                InstantiatedCar = Instantiate(CathingLoadFiles.manage.hot_rodd_ab, spawnPoint.position, spawnPoint.rotation);
                InstantiatedCar.GetComponent<Rigidbody>().isKinematic = false;
                InstantiatedCar.AddComponent<RCC_EnterExitCar>();
            }

            if (PlayerPrefs.GetInt("CurrentCar") == 2)
            {

                spawnPoint.position = new Vector3(-1.94f, 1.2f, 13.96f);
                InstantiatedCar = Instantiate(CathingLoadFiles.manage.buggy_gtr_ab, spawnPoint.position, spawnPoint.rotation);
                InstantiatedCar.GetComponent<Rigidbody>().isKinematic = false;
                InstantiatedCar.AddComponent<RCC_EnterExitCar>();
            }
            if (PlayerPrefs.GetInt("CurrentCar") == 3)
            {

                spawnPoint.position = new Vector3(-1.94f, 1.2f, 13.96f);
                InstantiatedCar = Instantiate(CathingLoadFiles.manage.gt500_ab, spawnPoint.position, spawnPoint.rotation);
                InstantiatedCar.GetComponent<Rigidbody>().isKinematic = false;
                InstantiatedCar.AddComponent<RCC_EnterExitCar>();
            }

            if (PlayerPrefs.GetInt("CurrentCar") == 4)
            {

                spawnPoint.position = new Vector3(-1.94f, 1.2f, 13.96f);
                InstantiatedCar = Instantiate(CarsPrefabs[PlayerPrefs.GetInt("CurrentCar")], spawnPoint.position, spawnPoint.rotation);
                //InstantiatedCar = Instantiate(CathingLoadFiles.manage.rasta_ab, spawnPoint.position, spawnPoint.rotation);
                InstantiatedCar.GetComponent<Rigidbody>().isKinematic = false;
                InstantiatedCar.AddComponent<RCC_EnterExitCar>();
            }
            if (PlayerPrefs.GetInt("CurrentCar") == 6)
            {
                spawnPoint.position = new Vector3(-1.94f, 1.2f, 13.96f);
                InstantiatedCar = Instantiate(CathingLoadFiles.manage.i8_ab, spawnPoint.position, spawnPoint.rotation);
                InstantiatedCar.GetComponent<Rigidbody>().isKinematic = false;
                InstantiatedCar.AddComponent<RCC_EnterExitCar>();
            }
            if (PlayerPrefs.GetInt("CurrentCar") == 7)
            {

                spawnPoint.position = new Vector3(-1.94f, 1.2f, 13.96f);
                InstantiatedCar = Instantiate(CathingLoadFiles.manage.lambo_ab, spawnPoint.position, spawnPoint.rotation);
                InstantiatedCar.AddComponent<RCC_EnterExitCar>();
            }
            if (PlayerPrefs.GetInt("CurrentCar") == 9)
            {

                spawnPoint.position = new Vector3(-1.94f, 1.2f, 13.96f);
                InstantiatedCar = Instantiate(CathingLoadFiles.manage.modelT_ab, spawnPoint.position, spawnPoint.rotation);
                InstantiatedCar.AddComponent<RCC_EnterExitCar>();
            }
            if (PlayerPrefs.GetInt("CurrentCar") == 10)
            {

                spawnPoint.position = new Vector3(-1.94f, 1.2f, 13.96f);
                InstantiatedCar = Instantiate(CathingLoadFiles.manage.townCar_ab, spawnPoint.position, spawnPoint.rotation);
                InstantiatedCar.GetComponent<Rigidbody>().isKinematic = false;
                InstantiatedCar.AddComponent<RCC_EnterExitCar>();
            }

            if (PlayerPrefs.GetInt("CurrentCar") == 11)
            {
                spawnPoint.position = new Vector3(-1.94f, 1.2f, 13.96f);
                InstantiatedCar = Instantiate(CathingLoadFiles.manage.ikarus, spawnPoint.position, spawnPoint.rotation);
                InstantiatedCar.AddComponent<RCC_EnterExitCar>();
            }

            if (PlayerPrefs.GetInt("CurrentCar") == 13)
            {
                spawnPoint.position = new Vector3(-1.94f, 1.2f, 13.96f);
                InstantiatedCar = Instantiate(CathingLoadFiles.manage.rr_ab, spawnPoint.position, spawnPoint.rotation);
                InstantiatedCar.AddComponent<RCC_EnterExitCar>();
            }

            //     if (PlayerPrefs.GetInt("CurrentCar") == 14)
            //     {
            //         spawnPoint.position = new Vector3(53, 5.10f, 205f);
            //         InstantiatedCar = Instantiate(CathingLoadFiles.manage.vetty_ab, spawnPoint.position, spawnPoint.rotation);
            //         InstantiatedCar.AddComponent<RCC_EnterExitCar>();
            //     }
        }

        if (SceneManager.GetActiveScene().name == "_arena_4")
        {
            PlayerPrefs.SetInt("Rating", PlayerPrefs.GetInt("Rating") + 2);
            //InsideOutsideCar.manage.OutFromCarButton.SetActive(false);

            if (PlayerPrefs.GetInt("CurrentCar") == 0
                || PlayerPrefs.GetInt("CurrentCar") == 5 || PlayerPrefs.GetInt("CurrentCar") == 8
                || PlayerPrefs.GetInt("CurrentCar") == 12)
            {
                InstantiatedCar = Instantiate(CarsPrefabs[PlayerPrefs.GetInt("CurrentCar")], spawnPoint.position, spawnPoint.rotation);
                InstantiatedCar.AddComponent<RCC_EnterExitCar>();
            }
            if (PlayerPrefs.GetInt("CurrentCar") == 1)
            {
                InstantiatedCar = Instantiate(CathingLoadFiles.manage.hot_rodd_ab, spawnPoint.position, spawnPoint.rotation);
                InstantiatedCar.GetComponent<Rigidbody>().isKinematic = false;
                InstantiatedCar.AddComponent<RCC_EnterExitCar>();
            }

            if (PlayerPrefs.GetInt("CurrentCar") == 2)
            {

                InstantiatedCar = Instantiate(CathingLoadFiles.manage.buggy_gtr_ab, spawnPoint.position, spawnPoint.rotation);
                InstantiatedCar.GetComponent<Rigidbody>().isKinematic = false;
                InstantiatedCar.AddComponent<RCC_EnterExitCar>();
            }
            if (PlayerPrefs.GetInt("CurrentCar") == 3)
            {

                InstantiatedCar = Instantiate(CathingLoadFiles.manage.gt500_ab, spawnPoint.position, spawnPoint.rotation);
                InstantiatedCar.GetComponent<Rigidbody>().isKinematic = false;
                InstantiatedCar.AddComponent<RCC_EnterExitCar>();
            }

            if (PlayerPrefs.GetInt("CurrentCar") == 4)
            {

                InstantiatedCar = Instantiate(CarsPrefabs[PlayerPrefs.GetInt("CurrentCar")], spawnPoint.position, spawnPoint.rotation);
                //InstantiatedCar = Instantiate(CathingLoadFiles.manage.rasta_ab, spawnPoint.position, spawnPoint.rotation);
                InstantiatedCar.GetComponent<Rigidbody>().isKinematic = false;
                InstantiatedCar.AddComponent<RCC_EnterExitCar>();
            }
            if (PlayerPrefs.GetInt("CurrentCar") == 6)
            {
                InstantiatedCar = Instantiate(CathingLoadFiles.manage.i8_ab, spawnPoint.position, spawnPoint.rotation);
                InstantiatedCar.GetComponent<Rigidbody>().isKinematic = false;
                InstantiatedCar.AddComponent<RCC_EnterExitCar>();
            }
            if (PlayerPrefs.GetInt("CurrentCar") == 7)
            {

                InstantiatedCar = Instantiate(CathingLoadFiles.manage.lambo_ab, spawnPoint.position, spawnPoint.rotation);
                InstantiatedCar.AddComponent<RCC_EnterExitCar>();
            }
            if (PlayerPrefs.GetInt("CurrentCar") == 9)
            {
                InstantiatedCar = Instantiate(CathingLoadFiles.manage.modelT_ab, spawnPoint.position, spawnPoint.rotation);
                InstantiatedCar.AddComponent<RCC_EnterExitCar>();
            }
            if (PlayerPrefs.GetInt("CurrentCar") == 10)
            {

                InstantiatedCar = Instantiate(CathingLoadFiles.manage.townCar_ab, spawnPoint.position, spawnPoint.rotation);
                InstantiatedCar.GetComponent<Rigidbody>().isKinematic = false;
                InstantiatedCar.AddComponent<RCC_EnterExitCar>();
            }

            if (PlayerPrefs.GetInt("CurrentCar") == 11)
            {
                InstantiatedCar = Instantiate(CathingLoadFiles.manage.ikarus, spawnPoint.position, spawnPoint.rotation);
                InstantiatedCar.AddComponent<RCC_EnterExitCar>();
            }

            if (PlayerPrefs.GetInt("CurrentCar") == 13)
            {
                InstantiatedCar = Instantiate(CathingLoadFiles.manage.rr_ab, spawnPoint.position, spawnPoint.rotation);
                InstantiatedCar.AddComponent<RCC_EnterExitCar>();
            }

            //     if (PlayerPrefs.GetInt("CurrentCar") == 14)
            //     {
            //         
            //         InstantiatedCar = Instantiate(CathingLoadFiles.manage.vetty_ab, spawnPoint.position, spawnPoint.rotation);
            //         InstantiatedCar.AddComponent<RCC_EnterExitCar>();
            //     }
        }


        #region Car Lights
        if (PlayerPrefs.GetInt("CurrentCar") == 4 || PlayerPrefs.GetInt("CurrentCar") == 6)
        {
            if (PlayerPrefs.GetInt("myCar") == 1)
            {
                GameObject light = GameObject.Find("Ilum");
                light.SetActive(false);
            }
        }

        if (PlayerPrefs.GetInt("CurrentCar") == 12 )
        {
            if (PlayerPrefs.GetInt("myCar") == 1)
            {
                GameObject light = GameObject.Find("rearGroundFlares");
                light.SetActive(false);
            }

        }

        #endregion

        #region Load Wheel Susspens
        if (PlayerPrefs.GetInt("WheelSussp" + PlayerPrefs.GetInt("CurrentCar")) == 1)
        {
            InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().FrontLeftWheelCollider.wheelCollider.suspensionDistance = PlayerPrefs.GetFloat("WheelSusspFront" + PlayerPrefs.GetInt("CurrentCar"));
            InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().FrontRightWheelCollider.wheelCollider.suspensionDistance = PlayerPrefs.GetFloat("WheelSusspFront" + PlayerPrefs.GetInt("CurrentCar"));

            InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().RearLeftWheelCollider.wheelCollider.suspensionDistance = PlayerPrefs.GetFloat("WheelSusspRear" + PlayerPrefs.GetInt("CurrentCar"));
            InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().RearRightWheelCollider.wheelCollider.suspensionDistance = PlayerPrefs.GetFloat("WheelSusspRear" + PlayerPrefs.GetInt("CurrentCar"));
        }
        if (PlayerPrefs.GetInt("WheelSussp" + PlayerPrefs.GetInt("CurrentCar")) == 0)
        {
            // if    (PlayerPrefs.GetInt("CurrentCar") == 0 || PlayerPrefs.GetInt("CurrentCar") == 1 ||
            //   PlayerPrefs.GetInt("CurrentCar") == 2 || PlayerPrefs.GetInt("CurrentCar") == 3 ||
            //   PlayerPrefs.GetInt("CurrentCar") == 4 || PlayerPrefs.GetInt("CurrentCar") == 5 ||
            //   PlayerPrefs.GetInt("CurrentCar") == 6 || PlayerPrefs.GetInt("CurrentCar") == 7 ||
            //   PlayerPrefs.GetInt("CurrentCar") == 9 || PlayerPrefs.GetInt("CurrentCar") == 11 ||
            //   PlayerPrefs.GetInt("CurrentCar") == 12)
            //{
            InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().FrontLeftWheelCollider.wheelCollider.suspensionDistance = 0.2f;
            InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().FrontRightWheelCollider.wheelCollider.suspensionDistance = 0.2f;

            InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().RearLeftWheelCollider.wheelCollider.suspensionDistance = 0.2f;
            InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().RearRightWheelCollider.wheelCollider.suspensionDistance = 0.2f;
        }
        if (PlayerPrefs.GetInt("CurrentCar") == 8)
        {
            InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().FrontLeftWheelCollider.wheelCollider.suspensionDistance = 0.02f;
            InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().FrontRightWheelCollider.wheelCollider.suspensionDistance = 0.02f;

            InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().RearLeftWheelCollider.wheelCollider.suspensionDistance = 0.02f;
            InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().RearRightWheelCollider.wheelCollider.suspensionDistance = 0.02f;
        }

        if (PlayerPrefs.GetInt("CurrentCar") == 10)
        {
            InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().FrontLeftWheelCollider.wheelCollider.suspensionDistance = 0.05f;
            InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().FrontRightWheelCollider.wheelCollider.suspensionDistance = 0.05f;

            InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().RearLeftWheelCollider.wheelCollider.suspensionDistance = 0.05f;
            InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().RearRightWheelCollider.wheelCollider.suspensionDistance = 0.05f;
        }


            #endregion

        #region TCS 
        if (PlayerPrefs.GetInt("TCS" + PlayerPrefs.GetInt("CurrentCar")) == 1 && PlayerPrefs.GetInt("selectTCS" + PlayerPrefs.GetInt("CurrentCar")) == 1)
            {
                InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().TCSThreshold = PlayerPrefs.GetFloat("TCSsetup" + PlayerPrefs.GetInt("CurrentCar"));
            }
            else
            {
                InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().TCS = false;
            }

            #endregion

        #region Traction 
            if (PlayerPrefs.GetInt("Traction" + PlayerPrefs.GetInt("CurrentCar")) == 1 && PlayerPrefs.GetInt("selectTraction" + PlayerPrefs.GetInt("CurrentCar")) == 1)
            {
                InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().tractionHelperStrength = PlayerPrefs.GetFloat("TractionSetup" + PlayerPrefs.GetInt("CurrentCar"));
                InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().tractionHelper = true;
            }
            else
            {
                InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().tractionHelper = false;
            }

            #endregion

        #region ESP
            if (PlayerPrefs.GetInt("ESP" + PlayerPrefs.GetInt("CurrentCar")) == 1 && PlayerPrefs.GetInt("selectESP" + PlayerPrefs.GetInt("CurrentCar")) == 1)
            {
                InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().ESPThreshold = PlayerPrefs.GetFloat("ESPsetup" + PlayerPrefs.GetInt("CurrentCar"));
            }
            else
            {
                InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().ESP = false;
            }
            #endregion

        #region load Car Config
            if (!MainMenuManager.NOSisChecked)
            {
                InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().useNOS = false;
                nosButton.SetActive(false);
                noslevel.interactable = false;
                noslevel.maxValue = 0;

            }
            else
            {
                InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().useNOS = true;
                InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().maxspeed += 35f;
                nosButton.SetActive(true);
                noslevel.interactable = true;
            }
            if (!MainMenuManager.TurboisChecked)
            {
                InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().useTurbo = false;
                InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().useExhaustFlame = false;

            }
            else
            {
                InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().useTurbo = true;
                InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().maxspeed += 10f;
                InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().useExhaustFlame = true;
            }
            if (!MainMenuManager.ABSisChecked)
            {
                InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().ABS = false;
            }
            else
            {
                InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().ABS = true;
                InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().ABSThreshold += 0.1f;
            }

            #endregion

        #region Load EngineUpdate
            if (MainMenuManager.manage.svChecked.engine0)
            {
                InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().maxspeed = InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().defMaxSpeed;
                InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().engineTorque = InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().engineTorque;
            }
            if (MainMenuManager.manage.svChecked.engine1)
            {
                InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().maxspeed += 15f;
                InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().engineTorque += 250f;
            }
            if (MainMenuManager.manage.svChecked.engine2)
            {
                InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().maxspeed += 30f;
                InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().engineTorque += 350f;
            }

            if (MainMenuManager.manage.svChecked.engine3)
            {
                InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().maxspeed += 50f;
                InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().engineTorque += 500f;
            }

            if (MainMenuManager.manage.svChecked.engine4)
            {
                InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().maxspeed += 70f;
                InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().engineTorque += 600f;
            }

            if (MainMenuManager.manage.svChecked.engine5)
            {
                InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().maxspeed += 80f;
                InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().engineTorque += 700f;
            }
            if (MainMenuManager.manage.svChecked.engine6)
            {
                InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().maxspeed += 90f;
                InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().engineTorque += 800f;
            }

            if (MainMenuManager.manage.svChecked.engine7)
            {
                InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().maxspeed += 100f;
                InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().engineTorque += 900f;
            }

            if (MainMenuManager.manage.svChecked.engine8)
            {
                InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().maxspeed += 110f;
                InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().engineTorque += 1200f;
            }
            if (MainMenuManager.manage.svChecked.engine9)
            {
                InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().maxspeed += 115f;
                InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().engineTorque += 1300f;
            }

            if (MainMenuManager.manage.svChecked.engine10)
            {
                InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().maxspeed += 125f;
                InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().engineTorque += 1400f;
            }

            if (MainMenuManager.manage.svChecked.engine11)
            {
                InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().maxspeed += 135f;
                InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().engineTorque += 1500f;
            }
            if (MainMenuManager.manage.svChecked.engine12)
            {
                InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().maxspeed += 145f;
                InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().engineTorque += 1600f;
            }
            if (MainMenuManager.manage.svChecked.engine13)
            {
                InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().maxspeed += 155f;
                InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().engineTorque += 1700f;
            }

            if (MainMenuManager.manage.svChecked.engine14)
            {
                InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().maxspeed += 175f;
                InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().engineTorque += 1850f;
            }
            if (MainMenuManager.manage.svChecked.engine15)
            {
                InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().maxspeed += 195f;
                InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().engineTorque += 2150f;
            }
            #endregion

        #region Load HandlingUpdate
            if (MainMenuManager.manage.svChecked.handling0)
            {
                //InstantiatedCar.GetComponent<RCC_CarControllerV3>().steerHelperLinearVelStrength += 0.05f;
                //InstantiatedCar.GetComponent<RCC_CarControllerV3>().steerHelperAngularVelStrength += 0.05f;
            }
            if (MainMenuManager.manage.svChecked.handling1)
            {
                InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().steerHelperLinearVelStrength += 0.05f;
                InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().steerHelperAngularVelStrength += 0.05f;
            }
            if (MainMenuManager.manage.svChecked.handling2)
            {
                InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().steerHelperLinearVelStrength += 0.1f;
                InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().steerHelperAngularVelStrength += 0.1f;
            }
            if (MainMenuManager.manage.svChecked.handling3)
            {
                InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().steerHelperLinearVelStrength += 0.15f;
                InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().steerHelperAngularVelStrength += 0.15f;
            }
            if (MainMenuManager.manage.svChecked.handling4)
            {
                InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().steerHelperLinearVelStrength += 0.2f;
                InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().steerHelperAngularVelStrength += 0.2f;
            }
            if (MainMenuManager.manage.svChecked.handling5)
            {
                InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().steerHelperLinearVelStrength += 0.25f;
                InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().steerHelperAngularVelStrength += 0.25f;
            }
            if (MainMenuManager.manage.svChecked.handling6)
            {
                InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().steerHelperLinearVelStrength += 0.28f;
                InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().steerHelperAngularVelStrength += 0.28f;
            }
            if (MainMenuManager.manage.svChecked.handling7)
            {
                InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().steerHelperLinearVelStrength += 0.35f;
                InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().steerHelperAngularVelStrength += 0.35f;
            }
            if (MainMenuManager.manage.svChecked.handling8)
            {
                InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().steerHelperLinearVelStrength += 0.45f;
                InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().steerHelperAngularVelStrength += 0.45f;
            }
            if (MainMenuManager.manage.svChecked.handling9)
            {
                InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().steerHelperLinearVelStrength += 0.6f;
                InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().steerHelperAngularVelStrength += 0.6f;
            }
            if (MainMenuManager.manage.svChecked.handling10)
            {
                InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().steerHelperLinearVelStrength += 0.7f;
                InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().steerHelperAngularVelStrength += 0.7f;
            }
            if (MainMenuManager.manage.svChecked.handling11)
            {
                InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().steerHelperLinearVelStrength += 0.8f;
                InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().steerHelperAngularVelStrength += 0.8f;
            }
            if (MainMenuManager.manage.svChecked.handling12)
            {
                InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().steerHelperLinearVelStrength += 0.9f;
                InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().steerHelperAngularVelStrength += 0.9f;
            }
            #endregion

        #region BrakeUpdate
            if (MainMenuManager.manage.svChecked.brake0)
            {
                InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().ABSThreshold = 0.1f;
                // InstantiatedCar.GetComponent<RCC_CarControllerV3>().brakeTorque = 1200f;

            }
            if (MainMenuManager.manage.svChecked.brake1)
            {
                InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().ABSThreshold += 0.025f;
                InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().brakeTorque += 100f;
            }
            if (MainMenuManager.manage.svChecked.brake2)
            {
                InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().ABSThreshold += 0.05f;
                InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().brakeTorque += 200f;
            }
            if (MainMenuManager.manage.svChecked.brake3)
            {
                InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().ABSThreshold += 0.075f;
                InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().brakeTorque += 300f;
            }
            if (MainMenuManager.manage.svChecked.brake4)
            {
                InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().ABSThreshold += 0.125f;
                InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().brakeTorque += 400f;
            }
            if (MainMenuManager.manage.svChecked.brake5)
            {
                InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().ABSThreshold += 0.2f;
                InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().brakeTorque += 500f;
            }
            if (MainMenuManager.manage.svChecked.brake6)
            {
                InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().ABSThreshold += 0.225f;
                InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().brakeTorque += 600f;
            }
            if (MainMenuManager.manage.svChecked.brake7)
            {
                InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().ABSThreshold += 0.3f;
                InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().brakeTorque += 700f;
            }
            if (MainMenuManager.manage.svChecked.brake8)
            {
                InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().ABSThreshold += 0.35f;
                InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().brakeTorque += 800f;
            }
            if (MainMenuManager.manage.svChecked.brake9)
            {
                InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().ABSThreshold += 0.4f;
                InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().brakeTorque += 900f;
            }
            if (MainMenuManager.manage.svChecked.brake10)
            {
                InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().ABSThreshold += 0.45f;
                InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().brakeTorque += 1000f;

                #endregion

        #region Wheel Drive Mode
                if (MainMenuManager.WheelDriveisChecked)
                {
                    switch (PlayerPrefs.GetString("DriveMode"))
                    {
                        case "":
                            InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>()._wheelTypeChoise = RCC_CarControllerV3.WheelType.RWD;
                            break;
                        case "RWD":
                            InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>()._wheelTypeChoise = RCC_CarControllerV3.WheelType.RWD;
                            break;
                        case "FWD":
                            InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>()._wheelTypeChoise = RCC_CarControllerV3.WheelType.FWD;
                            break;
                        case "AWD":
                            InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>()._wheelTypeChoise = RCC_CarControllerV3.WheelType.AWD;
                            break;
                    }
                }
                else
                {
                    InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>()._wheelTypeChoise = RCC_CarControllerV3.WheelType.RWD;
                }
                #endregion

            }
        }

    void Latency()
    {
        InstantiatedCar.AddComponent<RCC_EnterExitCar>();
    }
    void Update()
        {

        cnt += 1;
        if (cnt == 1)
        {
            carMirror = GameObject.FindGameObjectWithTag("CarMirror");
            carMirror.SetActive(false);
        }
            
            maxspd.text = "MAX:" + InstantiatedCar.GetComponentInChildren<RCC_CarControllerV3>().maxspeed.ToString() + "KM/H";
            if (InstantiatedCar.GetComponent<RCC_CarControllerV3>().speed > 30f)
            {
                count += 1;
                if (count == 1)
                {
                    Amplitude.Instance.logEvent("StartRide > 30 kmh");

                }
            }
        }
    }
