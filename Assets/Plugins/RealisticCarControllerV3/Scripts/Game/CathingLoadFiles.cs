using System;
using UnityEngine;
using System.Collections;
using System.IO;

public class CathingLoadFiles : MonoBehaviour
{
    public static CathingLoadFiles manage;
    
    public string SceneBundleURL;
    public string SceneNameToLoadAB;
    public int version;
    public int pesen;
    [Header("URL")]
    public string car11;
    public string karus_bus;
    public string modelT;
    [Header("Loaded Object")]
    [SerializeField] Transform parentForCar11;
    [SerializeField] Transform parentForBus;
    [SerializeField] Transform _modelT;
    public GameObject car_buick;
    public GameObject ikarus;
    public GameObject modelT_ab;
    [Header("Assets Name Object")]
    public string AssetNameCar11;
    public string AssetNameBus;
    public string assetModelT;

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
    }

    public void loadCar()
    {
        StartCoroutine(DownloadAndCache());
        StartCoroutine(DownloadAndCacheBus());
        StartCoroutine(ModelT());
    }

    public void loadCityOnline()
    {
        StartCoroutine(CityOnlineMap());
    }
    public string saveTo = ".";

    #region LoadOrCashedObjects
    IEnumerator DownloadAndCache()
    {
        // Wait for the Caching system to be ready
        while (!Caching.ready)
            yield return null;


        // Load the AssetBundle file from Cache if it exists with the same version or download and store it in the cache
        using (WWW www = WWW.LoadFromCacheOrDownload(car11 + "", 1))
        {


            //    WWW www = WWW.LoadFromCacheOrDownload(BundleURL + "", version);


            yield return www;


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

                if (AssetNameCar11 == "")
                {
                    car_buick = (GameObject)(bundle.mainAsset);
                    car_buick.GetComponentInChildren<Rigidbody>().isKinematic = true;
                    car_buick.transform.position = new Vector3(0, 1.3f, 0);
                    car_buick.transform.eulerAngles = new Vector2(0, 0);
                    Instantiate(car_buick).transform.SetParent(parentForCar11);
                }
                else {
                    car_buick = (GameObject)(bundle.LoadAsset(AssetNameCar11));
                    car_buick.GetComponentInChildren<Rigidbody>().isKinematic = true;
                    car_buick.transform.position = new Vector3(0, 1.3f, 0);
                    car_buick.transform.eulerAngles = new Vector2(0, 0);
                    Instantiate(car_buick).transform.SetParent(parentForCar11);
                    // Unload the AssetBundles compressed contents to conserve memory
                    Debug.Log(bundle.name);
                    bundle.Unload(false);
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


            yield return www;


            AssetBundle bundle_bus;
            if (!string.IsNullOrEmpty(www.error))
            {

                throw new Exception("WWW download had an error:" + www.error);

            }

            else
            {
                bundle_bus = www.assetBundle;
                Debug.Log(bundle_bus.name);


                for (int i = 0; i < bundle_bus.GetAllAssetNames().Length; i++)
                {
                    Debug.Log(bundle_bus.GetAllAssetNames()[i]);
                }

                if (AssetNameBus == "")
                {
                    ikarus = (GameObject)(bundle_bus.mainAsset);
                    ikarus.GetComponentInChildren<Rigidbody>().isKinematic = true;
                    ikarus.transform.position = new Vector3(0, 1.3f, 0);
                    ikarus.transform.eulerAngles = new Vector2(0, 0);
                    Instantiate(ikarus).transform.SetParent(parentForBus);
                }
                else
                {
                    ikarus = (GameObject)(bundle_bus.LoadAsset(AssetNameBus));
                    Instantiate(ikarus).transform.SetParent(parentForBus);
                    // Unload the AssetBundles compressed contents to conserve memory
                    Debug.Log(bundle_bus.name);
                    bundle_bus.Unload(false);
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


            yield return www;


            AssetBundle bundle_modelT;
            if (!string.IsNullOrEmpty(www.error))
            {

                throw new Exception("WWW download had an error:" + www.error);

            }

            else
            {
                bundle_modelT = www.assetBundle;
                Debug.Log(bundle_modelT.name);


                for (int i = 0; i < bundle_modelT.GetAllAssetNames().Length; i++)
                {
                    Debug.Log(bundle_modelT.GetAllAssetNames()[i]);
                }

                if (AssetNameBus == "")
                {
                    modelT_ab = (GameObject)(bundle_modelT.mainAsset);
                    modelT_ab.GetComponentInChildren<Rigidbody>().isKinematic = true;
                    modelT_ab.transform.position = new Vector3(0, 1.3f, 0);
                    modelT_ab.transform.eulerAngles = new Vector2(0, 0);
                    Instantiate(modelT_ab).transform.SetParent(_modelT);
                }
                else
                {
                    modelT_ab = (GameObject)(bundle_modelT.LoadAsset(assetModelT));
                    Instantiate(modelT_ab).transform.SetParent(_modelT);
                    // Unload the AssetBundles compressed contents to conserve memory
                    Debug.Log(bundle_modelT.name);
                    bundle_modelT.Unload(false);
                }
            }

        } // memory is freed from the web stream (www.Dispose() gets called implicitly)
    }


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

    #endregion
}
