using System;
using UnityEngine;
using System.Collections;
using System.IO;

public class CathingLoadFiles : MonoBehaviour
{
    public string BundleURL;
    public string AssetName;
    public int version;
    public int pesen;
    void Start()
    {
        StartCoroutine(DownloadAndCache());
    }
    public string saveTo = ".";
    IEnumerator DownloadAndCache()
    {
        // Wait for the Caching system to be ready
        while (!Caching.ready)
            yield return null;


        // Load the AssetBundle file from Cache if it exists with the same version or download and store it in the cache
        using (WWW www = WWW.LoadFromCacheOrDownload(BundleURL + "", version))
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

                if (AssetName == "")
                    Instantiate(bundle.mainAsset);
                else
                    Instantiate(bundle.LoadAsset(AssetName));
                // Unload the AssetBundles compressed contents to conserve memory
                Debug.Log(bundle.name);
                bundle.Unload(false);
            }


        } // memory is freed from the web stream (www.Dispose() gets called implicitly)
    }
}
