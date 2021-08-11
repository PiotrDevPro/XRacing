using System;
using UnityEngine;
using System.Collections;
using System.IO;

public class CathingLoadFiles : MonoBehaviour
{
    public static CathingLoadFiles manage;

    [SerializeField] string SceneBundleURL;
    [SerializeField] string SceneNameToLoadAB;
    [SerializeField] int version;
    [SerializeField] int pesen;
    [SerializeField] GameObject LoadingPanel;
    [Header("URL")]
    [SerializeField] string karus_bus;
    [SerializeField] string modelT;
    [SerializeField] string buggy_gtr;
    [SerializeField] string gt500_url;
    [SerializeField] string towncar_url;
    [SerializeField] string hotrodd_url;
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
    [SerializeField] Transform parentForBus;
    [SerializeField] Transform _modelT_parent;
    [SerializeField] Transform _buggy_gtr_parent;
    [SerializeField] Transform gt500_parent;
    [SerializeField] Transform towncar_parent;
    [SerializeField] Transform hotrodd_parent;
    //Cars
    public GameObject ikarus;
    public GameObject modelT_ab;
    public GameObject buggy_gtr_ab;
    public GameObject gt500_ab;
    public GameObject townCar_ab;
    public GameObject hot_rodd_ab;
    //Soundtracks
    public GameObject track11_ab;
    public GameObject track12_ab;
    public GameObject track13_ab;
    public GameObject track14_ab;
    public GameObject track15_ab;
    public GameObject track16_ab;
    public GameObject track17_ab;
    public GameObject track18_ab;
    [Header("Music")]
    [SerializeField] Transform parentForTrack11;
    [Header("Assets Name Object")]
    [SerializeField] string AssetNameBus;
    [SerializeField] string assetModelT;
    [SerializeField] string assetbuggyGtr;
    [SerializeField] string assetgt500;
    [SerializeField] string assetsTownCar;
    [SerializeField] string assetsHotRodd;
    [SerializeField] string assets_track11;
    [SerializeField] string assets_track12;
    [SerializeField] string assets_track13;
    [SerializeField] string assets_track14;
    [SerializeField] string assets_track15;
    [SerializeField] string assets_track16;
    [SerializeField] string assets_track17;
    [SerializeField] string assets_track18;
    [Header("Detect Loaded obj")]
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

    int count = 0;

    private void Awake()
    {
        if (manage == null)
        {
            manage = this;

        }
    }

    private void Start()
    {
        loadCar();
        SoundTrackPlay();
    }

    public void loadCar()
    {

        //Cars
        StartCoroutine(townCarLoad());
        StartCoroutine(DownloadAndCacheBus());
        StartCoroutine(ModelT());
        StartCoroutine(BuggyGTR());
        StartCoroutine(GT500());
        StartCoroutine(hotRodd());
    }

    public void SoundTrackPlay()
    {
        StartCoroutine(track11());
        StartCoroutine(track12());
        StartCoroutine(track13());
        StartCoroutine(track14());
        StartCoroutine(track15());
        StartCoroutine(track16());
        StartCoroutine(track17());
        StartCoroutine(track18());
    }

    public void loadCityOnline()
    {
        StartCoroutine(CityOnlineMap());
    }

    #region LoadOrCashedObjects
    IEnumerator townCarLoad()
    {
        // Wait for the Caching system to be ready
        while (!Caching.ready)
            yield return null;


        // Load the AssetBundle file from Cache if it exists with the same version or download and store it in the cache
        using (WWW www = WWW.LoadFromCacheOrDownload(towncar_url + "", 18))
        {


            //    WWW www = WWW.LoadFromCacheOrDownload(BundleURL + "", version);
            LoadingPanel.SetActive(true);

            yield return www;


            AssetBundle bundle_tc;
            if (!string.IsNullOrEmpty(www.error))
            {

                throw new Exception("WWW download had an error:" + www.error);


            }

            else
            {
                bundle_tc = www.assetBundle;
                //Debug.Log(bundle_tc.name);


                for (int i = 0; i < bundle_tc.GetAllAssetNames().Length; i++)
                {
                    //Debug.Log(bundle_tc.GetAllAssetNames()[i]);
                }

                if (assetsTownCar == "")
                {
                    townCar_ab = (GameObject)(bundle_tc.mainAsset);
                    print(townCar_ab);
                    // townCar_ab.GetComponentInChildren<Rigidbody>().isKinematic = true;
                    townCar_ab.transform.position = new Vector3(0, 1.3f, 0);
                    // townCar_ab.transform.eulerAngles = new Vector2(0, 0);
                    Instantiate(townCar_ab).transform.SetParent(towncar_parent);
                    Amplitude.Instance.logEvent("TownCarLoadedFirstTime");
                }
                else
                {
                    townCar_ab = (GameObject)(bundle_tc.LoadAsset(assetsTownCar));
                    //townCar_ab.GetComponentInChildren<Rigidbody>().isKinematic = true;
                    townCar_ab.transform.position = new Vector3(0, 1.3f, 0);
                    //townCar_ab.transform.eulerAngles = new Vector2(0, 0);
                    Instantiate(townCar_ab).transform.SetParent(towncar_parent);
                    // Unload the AssetBundles compressed contents to conserve memory
                    //Debug.Log(bundle_tc.name);
                    bundle_tc.Unload(false);
                    Amplitude.Instance.logEvent("TownCarLoadedCashed");
                    LoadingPanel.SetActive(false);
                }
            }

        } // memory is freed from the web stream (www.Dispose() gets called implicitly)
    }
    IEnumerator DownloadAndCacheBus()
    {
        // Wait for the Caching system to be ready
        while (!Caching.ready)
            yield return null;


        // Load the AssetBundle file from Cache if it exists with the same version or download and store it in the cache
        using (WWW www = WWW.LoadFromCacheOrDownload(karus_bus + "", 2))
        {


            //    WWW www = WWW.LoadFromCacheOrDownload(BundleURL + "", version);

            LoadingPanel.SetActive(true);
            yield return www;


            AssetBundle bundle_bus;
            if (!string.IsNullOrEmpty(www.error))
            {

                throw new Exception("WWW download had an error:" + www.error);

            }

            else
            {
                bundle_bus = www.assetBundle;


                for (int i = 0; i < bundle_bus.GetAllAssetNames().Length; i++)
                {

                }

                if (AssetNameBus == "")
                {
                    ikarus = (GameObject)(bundle_bus.mainAsset);
                    ikarus.GetComponentInChildren<Rigidbody>().isKinematic = true;
                    ikarus.transform.position = new Vector3(0, 1.3f, 0);
                    ikarus.transform.eulerAngles = new Vector2(0, 0);
                    Instantiate(ikarus).transform.SetParent(parentForBus);
                    Amplitude.Instance.logEvent("IkarusLoadedFirstTime");
                }
                else
                {
                    ikarus = (GameObject)(bundle_bus.LoadAsset(AssetNameBus));
                    Instantiate(ikarus).transform.SetParent(parentForBus);
                    // Unload the AssetBundles compressed contents to conserve memory
                    bundle_bus.Unload(false);
                    Amplitude.Instance.logEvent("IkarusLoadedCashed");
                    LoadingPanel.SetActive(false);
                }
            }

        } // memory is freed from the web stream (www.Dispose() gets called implicitly)
    }
    IEnumerator ModelT()
    {
        // Wait for the Caching system to be ready
        while (!Caching.ready)
            yield return null;

        
        // Load the AssetBundle file from Cache if it exists with the same version or download and store it in the cache
        using (WWW www = WWW.LoadFromCacheOrDownload(modelT + "", 3))
        {


            //    WWW www = WWW.LoadFromCacheOrDownload(BundleURL + "", version);

            LoadingPanel.SetActive(true);
            yield return www;


            AssetBundle bundle_modelT;
            if (!string.IsNullOrEmpty(www.error))
            {

                throw new Exception("WWW download had an error:" + www.error);

            }

            else
            {
                bundle_modelT = www.assetBundle;


                for (int i = 0; i < bundle_modelT.GetAllAssetNames().Length; i++)
                {

                }

                if (assetModelT == "")
                {
                    modelT_ab = (GameObject)(bundle_modelT.mainAsset);
                    modelT_ab.GetComponentInChildren<Rigidbody>().isKinematic = true;
                    modelT_ab.transform.position = new Vector3(0, 1.3f, 0);
                    modelT_ab.transform.eulerAngles = new Vector2(0, 0);
                    Instantiate(modelT_ab).transform.SetParent(_modelT_parent);
                    Amplitude.Instance.logEvent("TeslaTLoadedFirstTime");
                }
                else
                {
                    modelT_ab = (GameObject)(bundle_modelT.LoadAsset(assetModelT));
                    Instantiate(modelT_ab).transform.SetParent(_modelT_parent);
                    // Unload the AssetBundles compressed contents to conserve memory

                    bundle_modelT.Unload(false);
                    Amplitude.Instance.logEvent("TeslaTLoadedCashed");
                    LoadingPanel.SetActive(false);
                }
            }

        } // memory is freed from the web stream (www.Dispose() gets called implicitly)
    }
    IEnumerator BuggyGTR()
    {
        // Wait for the Caching system to be ready
        while (!Caching.ready)
            yield return null;
        // Load the AssetBundle file from Cache if it exists with the same version or download and store it in the cache
        using (WWW www = WWW.LoadFromCacheOrDownload(buggy_gtr + "", 4))
        {


            //    WWW www = WWW.LoadFromCacheOrDownload(BundleURL + "", version);

            LoadingPanel.SetActive(true);
            yield return www;


            AssetBundle bundle_gtr;
            if (!string.IsNullOrEmpty(www.error))
            {

                throw new Exception("WWW download had an error:" + www.error);

            }

            else
            {
                bundle_gtr = www.assetBundle;


                for (int i = 0; i < bundle_gtr.GetAllAssetNames().Length; i++)
                {

                }

                if (assetbuggyGtr == "")
                {
                    buggy_gtr_ab = (GameObject)(bundle_gtr.mainAsset);
                    buggy_gtr_ab.GetComponentInChildren<Rigidbody>().isKinematic = true;
                    buggy_gtr_ab.transform.position = new Vector3(0, 1.3f, 0);
                    buggy_gtr_ab.transform.eulerAngles = new Vector2(0, 0);
                    Instantiate(buggy_gtr_ab).transform.SetParent(_buggy_gtr_parent);
                    Amplitude.Instance.logEvent("BuggyLoadedFirstTime");
                }
                else
                {
                    buggy_gtr_ab = (GameObject)(bundle_gtr.LoadAsset(assetbuggyGtr));
                    buggy_gtr_ab.GetComponentInChildren<Rigidbody>().isKinematic = true;
                    buggy_gtr_ab.transform.position = new Vector3(0, 1.3f, 0);
                    buggy_gtr_ab.transform.eulerAngles = new Vector2(0, 0);
                    Instantiate(buggy_gtr_ab).transform.SetParent(_buggy_gtr_parent);
                    // Unload the AssetBundles compressed contents to conserve memory
                    bundle_gtr.Unload(false);
                    Amplitude.Instance.logEvent("BuggyLoadedCashed");
                    LoadingPanel.SetActive(false);
                }
            }

        } // memory is freed from the web stream (www.Dispose() gets called implicitly)
    }
    IEnumerator GT500()
    {
        // Wait for the Caching system to be ready
        while (!Caching.ready)
            yield return null;


        // Load the AssetBundle file from Cache if it exists with the same version or download and store it in the cache
        using (WWW www = WWW.LoadFromCacheOrDownload(gt500_url + "", 5))
        {


            //    WWW www = WWW.LoadFromCacheOrDownload(BundleURL + "", version);

            LoadingPanel.SetActive(true);
            yield return www;


            AssetBundle bundle_gt500;
            if (!string.IsNullOrEmpty(www.error))
            {

                throw new Exception("WWW download had an error:" + www.error);

            }

            else
            {
                bundle_gt500 = www.assetBundle;


                for (int i = 0; i < bundle_gt500.GetAllAssetNames().Length; i++)
                {
                    Debug.Log(bundle_gt500.GetAllAssetNames()[i]);
                }

                if (assetgt500 == "")
                {
                    gt500_ab = (GameObject)(bundle_gt500.mainAsset);
                    gt500_ab.GetComponentInChildren<Rigidbody>().isKinematic = true;
                    gt500_ab.transform.position = new Vector3(0, 1.3f, 0);
                    gt500_ab.transform.eulerAngles = new Vector2(0, 0);
                    Instantiate(gt500_ab).transform.SetParent(gt500_parent);
                    Amplitude.Instance.logEvent("GT500LoadedFirstTime");
                }
                else
                {
                    gt500_ab = (GameObject)(bundle_gt500.LoadAsset(assetgt500));
                    Instantiate(gt500_ab).transform.SetParent(gt500_parent);
                    // Unload the AssetBundles compressed contents to conserve memory
                    bundle_gt500.Unload(false);
                    Amplitude.Instance.logEvent("GT500Cashed");
                    LoadingPanel.SetActive(false);
                }
            }

        } // memory is freed from the web stream (www.Dispose() gets called implicitly)
    }
    IEnumerator hotRodd()
    {
        // Wait for the Caching system to be ready
        while (!Caching.ready)
            yield return null;


        // Load the AssetBundle file from Cache if it exists with the same version or download and store it in the cache
        using (WWW www = WWW.LoadFromCacheOrDownload(hotrodd_url + "", 13))
        {


            //    WWW www = WWW.LoadFromCacheOrDownload(BundleURL + "", version);
            LoadingPanel.SetActive(true);

            yield return www;


            AssetBundle bundle_hot_rodd;
            if (!string.IsNullOrEmpty(www.error))
            {

                throw new Exception("WWW download had an error:" + www.error);

            }

            else
            {
                bundle_hot_rodd = www.assetBundle;


                for (int i = 0; i < bundle_hot_rodd.GetAllAssetNames().Length; i++)
                {
                }

                if (assetsHotRodd == "")
                {
                    hot_rodd_ab = (GameObject)(bundle_hot_rodd.mainAsset);
                    hot_rodd_ab.GetComponentInChildren<Rigidbody>().isKinematic = true;
                    hot_rodd_ab.transform.position = new Vector3(0, 1.3f, 0);
                    hot_rodd_ab.transform.eulerAngles = new Vector2(0, 0);
                    Instantiate(hot_rodd_ab).transform.SetParent(hotrodd_parent);
                    Amplitude.Instance.logEvent("HotRoddLoaded");
                }
                else
                {
                    hot_rodd_ab = (GameObject)(bundle_hot_rodd.LoadAsset(assetsHotRodd));
                    hot_rodd_ab.GetComponentInChildren<Rigidbody>().isKinematic = true;
                    hot_rodd_ab.transform.position = new Vector3(0, 1.3f, 0);
                    hot_rodd_ab.transform.eulerAngles = new Vector2(0, 0);
                    Instantiate(hot_rodd_ab).transform.SetParent(hotrodd_parent);
                    // Unload the AssetBundles compressed contents to conserve memory
                    bundle_hot_rodd.Unload(false);
                    Amplitude.Instance.logEvent("HotRoddCashed");
                    LoadingPanel.SetActive(false);
                }
            }

        } // memory is freed from the web stream (www.Dispose() gets called implicitly)
    }

    //Scenes
    IEnumerator CityOnlineMap()
    {
        // Wait for the Caching system to be ready
        while (!Caching.ready)
            yield return null;
        using (WWW www = WWW.LoadFromCacheOrDownload(SceneBundleURL + "", version))
        {

            AssetBundle bundle;
            if (!string.IsNullOrEmpty(www.error))
            {

                throw new Exception("WWW download had an error:" + www.error);

            }
            else
            {
                bundle = www.assetBundle;
                Debug.Log(bundle.name);
                for (int i = 0; i < bundle.GetAllAssetNames().Length; i++)
                {
                    Debug.Log(bundle.GetAllAssetNames()[i]);
                }
                string[] scenes = bundle.GetAllScenePaths();
                print("scenes.Lengths " + scenes.Length);
                foreach (string scenename in scenes)
                {
                    SceneNameToLoadAB = Path.GetFileNameWithoutExtension(scenename).ToString();
                    print("Scene: " + Path.GetFileNameWithoutExtension(scenename));
                    LobbyManager.manage.CreateRoomCity();
                }
            }
        }
    }

    //Soundtrack
    IEnumerator track11()
    {
        // Wait for the Caching system to be ready
        while (!Caching.ready)
            yield return null;


        // Load the AssetBundle file from Cache if it exists with the same version or download and store it in the cache
        using (WWW www = WWW.LoadFromCacheOrDownload(track11_url + "", 21))
        {

            LoadingPanel.SetActive(true);
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
                    track11_ab = (GameObject)(bundle_track11.mainAsset);
                    Instantiate(track11_ab).transform.SetParent(parentForTrack11);
                    Amplitude.Instance.logEvent("track11Loaded");

                }
                else
                {
                    track11_ab = (GameObject)(bundle_track11.LoadAsset(assets_track11));
                    Instantiate(track11_ab).transform.SetParent(parentForTrack11);
                    // Unload the AssetBundles compressed contents to conserve memory
                    Debug.Log(bundle_track11.name);
                    bundle_track11.Unload(false);
                    Amplitude.Instance.logEvent("track11Cashed");
                    track11_isloaded = true;
                    LoadingPanel.SetActive(false);
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
                    track12_ab = (GameObject)(bundle_track12.mainAsset);
                    Instantiate(track12_ab).transform.SetParent(parentForTrack11);
                    Amplitude.Instance.logEvent("track12Loaded");

                }
                else
                {
                    track12_ab = (GameObject)(bundle_track12.LoadAsset(assets_track12));
                    Instantiate(track12_ab).transform.SetParent(parentForTrack11);
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
                    track13_ab = (GameObject)(bundle_track13.mainAsset);
                    Instantiate(track13_ab).transform.SetParent(parentForTrack11);
                    Amplitude.Instance.logEvent("track13Loaded");

                }
                else
                {
                    track13_ab = (GameObject)(bundle_track13.LoadAsset(assets_track13));
                    Instantiate(track13_ab).transform.SetParent(parentForTrack11);
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
                    track14_ab = (GameObject)(bundle_track14.mainAsset);
                    Instantiate(track14_ab).transform.SetParent(parentForTrack11);
                    Amplitude.Instance.logEvent("track14Loaded");

                }
                else
                {
                    track14_ab = (GameObject)(bundle_track14.LoadAsset(assets_track14));
                    Instantiate(track14_ab).transform.SetParent(parentForTrack11);
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
                    track15_ab = (GameObject)(bundle_track15.mainAsset);
                    Instantiate(track15_ab).transform.SetParent(parentForTrack11);
                    Amplitude.Instance.logEvent("track15Loaded");

                }
                else
                {
                    track15_ab = (GameObject)(bundle_track15.LoadAsset(assets_track15));
                    Instantiate(track15_ab).transform.SetParent(parentForTrack11);
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
                    track16_ab = (GameObject)(bundle_track16.mainAsset);
                    Instantiate(track16_ab).transform.SetParent(parentForTrack11);
                    Amplitude.Instance.logEvent("track16Loaded");

                }
                else
                {
                    track16_ab = (GameObject)(bundle_track16.LoadAsset(assets_track16));
                    Instantiate(track16_ab).transform.SetParent(parentForTrack11);
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
                    track17_ab = (GameObject)(bundle_track17.mainAsset);
                    Instantiate(track17_ab).transform.SetParent(parentForTrack11);
                    Amplitude.Instance.logEvent("track17Loaded");

                }
                else
                {
                    track17_ab = (GameObject)(bundle_track17.LoadAsset(assets_track17));
                    Instantiate(track17_ab).transform.SetParent(parentForTrack11);
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
                    track18_ab = (GameObject)(bundle_track18.mainAsset);
                    Instantiate(track17_ab).transform.SetParent(parentForTrack11);
                    Amplitude.Instance.logEvent("track18Loaded");

                }
                else
                {
                    track18_ab = (GameObject)(bundle_track18.LoadAsset(assets_track18));
                    Instantiate(track18_ab).transform.SetParent(parentForTrack11);
                    // Unload the AssetBundles compressed contents to conserve memory
                    Debug.Log(bundle_track18.name);
                    bundle_track18.Unload(false);
                    Amplitude.Instance.logEvent("track18Cashed");
                    track18_isloaded = true;
                }
            }

        } // memory is freed from the web stream (www.Dispose() gets called implicitly)
    }



    private void Update()
    {

        if (track11_isloaded && track12_isloaded && track13_isloaded
                && track14_isloaded && track15_isloaded && track16_isloaded
                    && track17_isloaded && track18_isloaded)
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


                if (PlayerPrefs.GetInt("MusicActive") != 1)
                {
                    MainMenuManager.manage.tracks[0].Stop();
                    MainMenuManager.manage.tracks[1].Stop();
                    MainMenuManager.manage.tracks[2].Stop();
                    MainMenuManager.manage.tracks[3].Stop();
                    MainMenuManager.manage.tracks[4].Stop();
                    MainMenuManager.manage.tracks[5].Stop();
                    MainMenuManager.manage.StartSoundtrack();
                }
                count = 2;
            }

        }

    }
}
    #endregion

