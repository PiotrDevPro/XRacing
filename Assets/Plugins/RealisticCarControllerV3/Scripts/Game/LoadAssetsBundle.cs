using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.IO;
using Photon.Pun;
using Photon.Realtime;

public class LoadAssetsBundle : MonoBehaviour
{
    public static LoadAssetsBundle manage;
    
    AssetBundle assetBundle;
    WWW www;
    public string SceneNameToLoadAB;
    bool loadingStart = false;
    [SerializeField] Transform parentForAB;
    [SerializeField] string url = "https://drive.google.com/uc?export=download&id=1xVWP64yAxmdg_SDxTDn_Qa9IV9A5Koxr";
    [SerializeField] string url_city_online = "https://drive.google.com/uc?export=download&id=1JPvF5A9Ipel98rKU0_ZOfeKAzpL6bCNh";
    [Header("Other")]
    public GameObject loadingPanel;
    public Image bg_fill;
    public Text percent;
    [Header("Loaded Object")]
    [SerializeField] GameObject obj;

    private void Awake()
    {
        if (manage == null)
        {
            manage = this;
        }
    }

    // Update is called once per frame

    private void Update()
    {

        if (loadingStart)
        {
            double v = www.progress;
            bg_fill.fillAmount = (float)v;
            v = System.Math.Round(v, 2);
            v *= 100;
            percent.text = "" + v + "%";
            print("loadingStart");
        }
    }

    IEnumerator DownloadFiles()
    {
        if (!assetBundle)
        {
           // using (www =  WWW.LoadFromCacheOrDownload(url_city_online,0))
           using (www = new WWW(url_city_online))
            {
                print("Using Now");
                loadingPanel.SetActive(true);
                loadingStart = true;
                yield return www;
                if (!string.IsNullOrEmpty(www.error))
                {
                    Debug.LogError(www.error);
                    yield break;
                }
                assetBundle = www.assetBundle;
            }
        }
        loadingStart = false;
        
        string[] scenes = assetBundle.GetAllScenePaths();
        print("scenes.Lengths " + scenes.Length);
        foreach (string scenename in scenes)
        {
            SceneNameToLoadAB = Path.GetFileNameWithoutExtension(scenename).ToString();
            print("Scene: " + Path.GetFileNameWithoutExtension(scenename));
            LobbyManager.manage.CreateRoomCity();
            PlayerPrefs.SetInt("isLoaded",1);
        }
    }

    public void LoadAssetBundleSceneCity()
    {
            StartCoroutine(DownloadFiles());
        //SceneManager.LoadScene(SceneNameToLoadAB);
    }

    
}
