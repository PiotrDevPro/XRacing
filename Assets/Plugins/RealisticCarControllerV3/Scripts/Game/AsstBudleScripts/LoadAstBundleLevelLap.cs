using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadAstBundleLevelLap : MonoBehaviour
{
    public static LoadAstBundleLevelLap manage;
    int count = 0;
    [Header("URL")]
    [SerializeField] string objects_1_url;
    [SerializeField] string city_url;
    //Soundtrack
    [SerializeField] string track11_url;
    [SerializeField] string track12_url;
    [SerializeField] string track13_url;
    [SerializeField] string track14_url;
    [SerializeField] string track15_url;
    [SerializeField] string track16_url;
    [SerializeField] string track17_url;
    [SerializeField] string track18_url;
    [Header("Loaded Object")]
    [SerializeField] Transform objects_1_parent;
    [SerializeField] Transform city_parent;
    [SerializeField] GameObject objects_1_prefab;
    [SerializeField] GameObject city_prefab;
    //Soundtrack
    [SerializeField] GameObject track11_prefab;
    [SerializeField] GameObject track12_prefab;
    [SerializeField] GameObject track13_prefab;
    [SerializeField] GameObject track14_prefab;
    [SerializeField] GameObject track15_prefab;
    [SerializeField] GameObject track16_prefab;
    [SerializeField] GameObject track17_prefab;
    [SerializeField] GameObject track18_prefab;
    [Header("Music")]
    [SerializeField] Transform parentForTrack11;
    [Header("Assets Name Object")]
    [SerializeField] string AssetObjects1;
    [SerializeField] string AssetCityObj;
    [SerializeField] string assets_track11;
    [SerializeField] string assets_track12;
    [SerializeField] string assets_track13;
    [SerializeField] string assets_track14;
    [SerializeField] string assets_track15;
    [SerializeField] string assets_track16;
    [SerializeField] string assets_track17;
    [SerializeField] string assets_track18;
    [Header("Detect Loaded/Cashed obj")]
    public GameObject track11_isLoaded;
    public GameObject track12_isLoaded;
    public GameObject track13_isLoaded;
    public GameObject track14_isLoaded;
    public GameObject track15_isLoaded;
    public GameObject track16_isLoaded;
    public GameObject track17_isLoaded;
    public GameObject track18_isLoaded;
    public bool track11_isloaded = false;
    public bool track12_isloaded = false;
    public bool track13_isloaded = false;
    public bool track14_isloaded = false;
    public bool track15_isloaded = false;
    public bool track16_isloaded = false;
    public bool track17_isloaded = false;
    public bool track18_isloaded = false;

    private void Awake()
    {
        if (manage == null)
        {
            manage = this;
        }
    }

    private void Update()
    {
        /*
        if (track11_isloaded || track12_isloaded ||
            track13_isloaded || track14_isloaded ||
            track15_isloaded || track16_isloaded ||
            track17_isloaded || track18_isloaded)

        {
            count += 1;
            if (count == 1)
            {

                track11_isLoaded = GameObject.Find("track11(Clone)");
                track12_isLoaded = GameObject.Find("track12(Clone)");
                track13_isLoaded = GameObject.Find("track13(Clone)");
                track14_isLoaded = GameObject.Find("track14(Clone)");
                track15_isLoaded = GameObject.Find("track15(Clone)");
                track16_isLoaded = GameObject.Find("track16(Clone)");
                track17_isLoaded = GameObject.Find("track17(Clone)");
                track18_isLoaded = GameObject.Find("track18(Clone)");
                if (PlayerPrefs.GetInt("Soundtrack") != 1)
                {
                    Pause.manage.tracks[0].Stop();
                    Pause.manage.tracks[1].Stop();
                    Pause.manage.tracks[2].Stop();
                    Pause.manage.tracks[3].Stop();
                    Pause.manage.tracks[4].Stop();
                    Pause.manage.tracks[5].Stop();
                    Pause.manage.tracks[6].Stop();
                    Pause.manage.StartSoundtrack();

                }
                count = 2;
            }
        }
        */
    }

    private void Start()
    {
        loadFilesFromAB();
    }

    public void loadFilesFromAB()
    {
        //Soundtrack
       // StartCoroutine(track11());
       // StartCoroutine(track12());
       // StartCoroutine(track13());
       // StartCoroutine(track14());
       // StartCoroutine(track15());
       // StartCoroutine(track16());
       // StartCoroutine(track17());
       // StartCoroutine(track18());

        if (MainMenuManager.manage.isAllvsYou)
        {
            StartCoroutine(LoadCity());
            StartCoroutine(LoadObj1());
        }

        if (MainMenuManager.manage.isFreerideActive)
        {
            StartCoroutine(LoadCity());
        }
    }

    #region CashedObjects
    IEnumerator LoadObj1()
    {
        // Wait for the Caching system to be ready
        while (!Caching.ready)
            yield return null;


        // Load the AssetBundle file from Cache if it exists with the same version or download and store it in the cache
        using (WWW www = WWW.LoadFromCacheOrDownload(objects_1_url + "", 15))
        {


            //    WWW www = WWW.LoadFromCacheOrDownload(BundleURL + "", version);


            yield return www;


            AssetBundle bundle_object;
            if (!string.IsNullOrEmpty(www.error))
            {

                throw new Exception("WWW download had an error:" + www.error);


            }

            else
            {
                bundle_object = www.assetBundle;
                Debug.Log(bundle_object.name);


                for (int i = 0; i < bundle_object.GetAllAssetNames().Length; i++)
                {
                    Debug.Log(bundle_object.GetAllAssetNames()[i]);
                }

                if (AssetObjects1 == "")
                {
                    objects_1_prefab = (GameObject)(bundle_object.mainAsset);
                    Instantiate(objects_1_prefab).transform.SetParent(objects_1_parent);
                    Amplitude.Instance.logEvent("LevelLap6_obj1_Loaded");
                }
                else
                {
                        objects_1_prefab = (GameObject)(bundle_object.LoadAsset(AssetObjects1));
                        Instantiate(objects_1_prefab).transform.SetParent(objects_1_parent);
                        // Unload the AssetBundles compressed contents to conserve memory
                        Debug.Log(bundle_object.name);
                        bundle_object.Unload(false);
                        Amplitude.Instance.logEvent("LevelLap6_obj1_Cashed");
                }
            }

        } 
    }

    IEnumerator LoadCity()
    {
        // Wait for the Caching system to be ready
        while (!Caching.ready)
            yield return null;


        // Load the AssetBundle file from Cache if it exists with the same version or download and store it in the cache
        using (WWW www = WWW.LoadFromCacheOrDownload(city_url + "", 17))
        {


            //    WWW www = WWW.LoadFromCacheOrDownload(BundleURL + "", version);


            yield return www;


            AssetBundle bundle_city;
            if (!string.IsNullOrEmpty(www.error))
            {

                throw new Exception("WWW download had an error:" + www.error);


            }

            else
            {
                bundle_city = www.assetBundle;
                Debug.Log(bundle_city.name);


                for (int i = 0; i < bundle_city.GetAllAssetNames().Length; i++)
                {
                    Debug.Log(bundle_city.GetAllAssetNames()[i]);
                }

                if (AssetCityObj == "")
                {
                    city_prefab = (GameObject)(bundle_city.mainAsset);
                    Instantiate(city_prefab).transform.SetParent(city_parent);
                    Amplitude.Instance.logEvent("LevelLap6_city_Loaded");
                }
                else
                {
                   // if (MainMenuManager.manage.isAllvsYou || MainMenuManager.manage.isFreerideActive)
                   // {
                        city_prefab = (GameObject)(bundle_city.LoadAsset(AssetCityObj));
                        Instantiate(city_prefab).transform.SetParent(city_parent);
                        // Unload the AssetBundles compressed contents to conserve memory
                        Debug.Log(bundle_city.name);
                        bundle_city.Unload(false);
                        Amplitude.Instance.logEvent("LevelLap6_city_Cashed");
                   // }
                }
            }

        }
    }
    #endregion

    #region CashedMusic
    IEnumerator track11()
    {
        // Wait for the Caching system to be ready
        while (!Caching.ready)
            yield return null;


        // Load the AssetBundle file from Cache if it exists with the same version or download and store it in the cache
        using (WWW www = WWW.LoadFromCacheOrDownload(track11_url + "", 21))
        {


            //    WWW www = WWW.LoadFromCacheOrDownload(BundleURL + "", version);


            yield return www;


            AssetBundle bundle_track11;
            if (!string.IsNullOrEmpty(www.error))
            {

                throw new Exception("WWW download had an error:" + www.error);

            }

            else
            {
                bundle_track11 = www.assetBundle;
                Debug.Log(bundle_track11.name);


                for (int i = 0; i < bundle_track11.GetAllAssetNames().Length; i++)
                {
                    Debug.Log(bundle_track11.GetAllAssetNames()[i]);
                }

                if (assets_track11 == "")
                {
                    track11_prefab = (GameObject)(bundle_track11.mainAsset);
                    Instantiate(track11_prefab).transform.SetParent(parentForTrack11);
                    Amplitude.Instance.logEvent("track11LevelLap6Loaded");

                }
                else
                {
                    track11_prefab = (GameObject)(bundle_track11.LoadAsset(assets_track11));
                    Instantiate(track11_prefab).transform.SetParent(parentForTrack11);
                    // Unload the AssetBundles compressed contents to conserve memory
                    Debug.Log(bundle_track11.name);
                    bundle_track11.Unload(false);
                    Amplitude.Instance.logEvent("track11LevelLap6Cashed");
                    track11_isloaded = true;
                }
            }

        } // memory is freed from the web stream (www.Dispose() gets called implicitly)
    }

    IEnumerator track12()
    {
        // Wait for the Caching system to be ready
        while (!Caching.ready)
            yield return null;


        // Load the AssetBundle file from Cache if it exists with the same version or download and store it in the cache
        using (WWW www = WWW.LoadFromCacheOrDownload(track12_url + "", 22))
        {


            //    WWW www = WWW.LoadFromCacheOrDownload(BundleURL + "", version);


            yield return www;


            AssetBundle bundle_track12;
            if (!string.IsNullOrEmpty(www.error))
            {

                throw new Exception("WWW download had an error:" + www.error);

            }

            else
            {
                bundle_track12 = www.assetBundle;
                Debug.Log(bundle_track12.name);


                for (int i = 0; i < bundle_track12.GetAllAssetNames().Length; i++)
                {
                    Debug.Log(bundle_track12.GetAllAssetNames()[i]);
                }

                if (assets_track12 == "")
                {
                    track12_prefab = (GameObject)(bundle_track12.mainAsset);
                    Instantiate(track12_prefab).transform.SetParent(parentForTrack11);
                    Amplitude.Instance.logEvent("track12Loaded");

                }
                else
                {
                    track12_prefab = (GameObject)(bundle_track12.LoadAsset(assets_track12));
                    Instantiate(track12_prefab).transform.SetParent(parentForTrack11);
                    // Unload the AssetBundles compressed contents to conserve memory
                    Debug.Log(bundle_track12.name);
                    bundle_track12.Unload(false);
                    Amplitude.Instance.logEvent("track12Cashed");
                    track12_isloaded = true;
                }
            }

        } // memory is freed from the web stream (www.Dispose() gets called implicitly)
    }

    IEnumerator track13()
    {
        // Wait for the Caching system to be ready
        while (!Caching.ready)
            yield return null;


        // Load the AssetBundle file from Cache if it exists with the same version or download and store it in the cache
        using (WWW www = WWW.LoadFromCacheOrDownload(track13_url + "", 25))
        {


            //    WWW www = WWW.LoadFromCacheOrDownload(BundleURL + "", version);


            yield return www;


            AssetBundle bundle_track13;
            if (!string.IsNullOrEmpty(www.error))
            {

                throw new Exception("WWW download had an error:" + www.error);

            }

            else
            {
                bundle_track13 = www.assetBundle;
                Debug.Log(bundle_track13.name);


                for (int i = 0; i < bundle_track13.GetAllAssetNames().Length; i++)
                {
                    Debug.Log(bundle_track13.GetAllAssetNames()[i]);
                }

                if (assets_track13 == "")
                {
                    track13_prefab = (GameObject)(bundle_track13.mainAsset);
                    Instantiate(track13_prefab).transform.SetParent(parentForTrack11);
                    Amplitude.Instance.logEvent("track13Loaded");

                }
                else
                {
                    track13_prefab = (GameObject)(bundle_track13.LoadAsset(assets_track13));
                    Instantiate(track13_prefab).transform.SetParent(parentForTrack11);
                    // Unload the AssetBundles compressed contents to conserve memory
                    Debug.Log(bundle_track13.name);
                    bundle_track13.Unload(false);
                    Amplitude.Instance.logEvent("track13Cashed");
                    track13_isloaded = true;
                }
            }

        } // memory is freed from the web stream (www.Dispose() gets called implicitly)
    }

    IEnumerator track14()
    {
        // Wait for the Caching system to be ready
        while (!Caching.ready)
            yield return null;


        // Load the AssetBundle file from Cache if it exists with the same version or download and store it in the cache
        using (WWW www = WWW.LoadFromCacheOrDownload(track14_url + "", 24))
        {


            //    WWW www = WWW.LoadFromCacheOrDownload(BundleURL + "", version);


            yield return www;


            AssetBundle bundle_track14;
            if (!string.IsNullOrEmpty(www.error))
            {

                throw new Exception("WWW download had an error:" + www.error);

            }

            else
            {
                bundle_track14 = www.assetBundle;
                Debug.Log(bundle_track14.name);


                for (int i = 0; i < bundle_track14.GetAllAssetNames().Length; i++)
                {
                    Debug.Log(bundle_track14.GetAllAssetNames()[i]);
                }

                if (assets_track14 == "")
                {
                    track14_prefab = (GameObject)(bundle_track14.mainAsset);
                    Instantiate(track14_prefab).transform.SetParent(parentForTrack11);
                    Amplitude.Instance.logEvent("track14Loaded");

                }
                else
                {
                    track14_prefab = (GameObject)(bundle_track14.LoadAsset(assets_track14));
                    Instantiate(track14_prefab).transform.SetParent(parentForTrack11);
                    // Unload the AssetBundles compressed contents to conserve memory
                    Debug.Log(bundle_track14.name);
                    bundle_track14.Unload(false);
                    Amplitude.Instance.logEvent("track14Cashed");
                    track14_isloaded = true;
                }
            }

        } // memory is freed from the web stream (www.Dispose() gets called implicitly)
    }

    IEnumerator track15()
    {
        // Wait for the Caching system to be ready
        while (!Caching.ready)
            yield return null;


        // Load the AssetBundle file from Cache if it exists with the same version or download and store it in the cache
        using (WWW www = WWW.LoadFromCacheOrDownload(track15_url + "", 27))
        {


            //    WWW www = WWW.LoadFromCacheOrDownload(BundleURL + "", version);


            yield return www;


            AssetBundle bundle_track15;
            if (!string.IsNullOrEmpty(www.error))
            {

                throw new Exception("WWW download had an error:" + www.error);

            }

            else
            {
                bundle_track15 = www.assetBundle;
                Debug.Log(bundle_track15.name);


                for (int i = 0; i < bundle_track15.GetAllAssetNames().Length; i++)
                {
                    Debug.Log(bundle_track15.GetAllAssetNames()[i]);
                }

                if (assets_track15 == "")
                {
                    track15_prefab = (GameObject)(bundle_track15.mainAsset);
                    Instantiate(track15_prefab).transform.SetParent(parentForTrack11);
                    Amplitude.Instance.logEvent("track15Loaded");

                }
                else
                {
                    track15_prefab = (GameObject)(bundle_track15.LoadAsset(assets_track15));
                    Instantiate(track15_prefab).transform.SetParent(parentForTrack11);
                    // Unload the AssetBundles compressed contents to conserve memory
                    Debug.Log(bundle_track15.name);
                    bundle_track15.Unload(false);
                    Amplitude.Instance.logEvent("track15Cashed");
                    track15_isloaded = true;
                }
            }

        } // memory is freed from the web stream (www.Dispose() gets called implicitly)
    }

    IEnumerator track16()
    {
        // Wait for the Caching system to be ready
        while (!Caching.ready)
            yield return null;


        // Load the AssetBundle file from Cache if it exists with the same version or download and store it in the cache
        using (WWW www = WWW.LoadFromCacheOrDownload(track16_url + "", 26))
        {


            //    WWW www = WWW.LoadFromCacheOrDownload(BundleURL + "", version);


            yield return www;


            AssetBundle bundle_track16;
            if (!string.IsNullOrEmpty(www.error))
            {

                throw new Exception("WWW download had an error:" + www.error);

            }

            else
            {
                bundle_track16 = www.assetBundle;
                Debug.Log(bundle_track16.name);


                for (int i = 0; i < bundle_track16.GetAllAssetNames().Length; i++)
                {
                    Debug.Log(bundle_track16.GetAllAssetNames()[i]);
                }

                if (assets_track16 == "")
                {
                    track16_prefab = (GameObject)(bundle_track16.mainAsset);
                    Instantiate(track16_prefab).transform.SetParent(parentForTrack11);
                    Amplitude.Instance.logEvent("track16Loaded");

                }
                else
                {
                    track16_prefab = (GameObject)(bundle_track16.LoadAsset(assets_track16));
                    Instantiate(track16_prefab).transform.SetParent(parentForTrack11);
                    // Unload the AssetBundles compressed contents to conserve memory
                    Debug.Log(bundle_track16.name);
                    bundle_track16.Unload(false);
                    Amplitude.Instance.logEvent("track16Cashed");
                    track16_isloaded = true;
                }
            }

        } // memory is freed from the web stream (www.Dispose() gets called implicitly)
    }

    IEnumerator track17()
    {
        // Wait for the Caching system to be ready
        while (!Caching.ready)
            yield return null;


        // Load the AssetBundle file from Cache if it exists with the same version or download and store it in the cache
        using (WWW www = WWW.LoadFromCacheOrDownload(track17_url + "", 28))
        {


            //    WWW www = WWW.LoadFromCacheOrDownload(BundleURL + "", version);


            yield return www;


            AssetBundle bundle_track17;
            if (!string.IsNullOrEmpty(www.error))
            {

                throw new Exception("WWW download had an error:" + www.error);

            }

            else
            {
                bundle_track17 = www.assetBundle;
                Debug.Log(bundle_track17.name);


                for (int i = 0; i < bundle_track17.GetAllAssetNames().Length; i++)
                {
                    Debug.Log(bundle_track17.GetAllAssetNames()[i]);
                }

                if (assets_track17 == "")
                {
                    track17_prefab = (GameObject)(bundle_track17.mainAsset);
                    Instantiate(track17_prefab).transform.SetParent(parentForTrack11);
                    Amplitude.Instance.logEvent("track17Loaded");

                }
                else
                {
                    track17_prefab = (GameObject)(bundle_track17.LoadAsset(assets_track17));
                    Instantiate(track17_prefab).transform.SetParent(parentForTrack11);
                    // Unload the AssetBundles compressed contents to conserve memory
                    Debug.Log(bundle_track17.name);
                    bundle_track17.Unload(false);
                    Amplitude.Instance.logEvent("track17Cashed");
                    track17_isloaded = true;
                }
            }

        } // memory is freed from the web stream (www.Dispose() gets called implicitly)
    }

    IEnumerator track18()
    {
        // Wait for the Caching system to be ready
        while (!Caching.ready)
            yield return null;


        // Load the AssetBundle file from Cache if it exists with the same version or download and store it in the cache
        using (WWW www = WWW.LoadFromCacheOrDownload(track18_url + "", 29))
        {


            //    WWW www = WWW.LoadFromCacheOrDownload(BundleURL + "", version);


            yield return www;


            AssetBundle bundle_track18;
            if (!string.IsNullOrEmpty(www.error))
            {

                throw new Exception("WWW download had an error:" + www.error);

            }

            else
            {
                bundle_track18 = www.assetBundle;
                Debug.Log(bundle_track18.name);


                for (int i = 0; i < bundle_track18.GetAllAssetNames().Length; i++)
                {
                    Debug.Log(bundle_track18.GetAllAssetNames()[i]);
                }

                if (assets_track18 == "")
                {
                    track18_prefab = (GameObject)(bundle_track18.mainAsset);
                    Instantiate(track18_prefab).transform.SetParent(parentForTrack11);
                    Amplitude.Instance.logEvent("track18Loaded");

                }
                else
                {
                    track18_prefab = (GameObject)(bundle_track18.LoadAsset(assets_track18));
                    Instantiate(track18_prefab).transform.SetParent(parentForTrack11);
                    // Unload the AssetBundles compressed contents to conserve memory
                    Debug.Log(bundle_track18.name);
                    bundle_track18.Unload(false);
                    Amplitude.Instance.logEvent("track18Cashed");
                    track18_isloaded = true;
                }
            }

        } // memory is freed from the web stream (www.Dispose() gets called implicitly)
    }

    #endregion

}
