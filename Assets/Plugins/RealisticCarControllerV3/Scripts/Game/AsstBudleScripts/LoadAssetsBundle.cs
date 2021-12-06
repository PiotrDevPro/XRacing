using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.IO;

public class LoadAssetsBundle : MonoBehaviour
{
    public static LoadAssetsBundle manage;
    
    AssetBundle assetBundle;
    WWW www;
    [SerializeField] string SceneNameToLoadAB;
    bool loadingStart = false;
    [SerializeField] Transform parentForAB;
    [SerializeField] string url = "https://drive.google.com/uc?export=download&id=1xVWP64yAxmdg_SDxTDn_Qa9IV9A5Koxr";
    [SerializeField] string url_city_online = "";
    [Header("Loaded Obj")]
    [SerializeField] string url_city_single = "";
    [SerializeField] string sceneNameCitySingle;
    [Header("Other")]
    public GameObject loadingPanel;
    public Image bg_fill;
    public Text percent;


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
        //print("AppActivate: " + PlayerPrefs.GetInt("AppActivate"));
        //print("citysingle: " + PlayerPrefs.GetInt("citysingle"));
        if (loadingStart)
        {
            double v = www.progress;
            bg_fill.fillAmount = (float)v;
            v = System.Math.Round(v, 2);
            v *= 100;
            percent.text = "" + v + "%";
           // print("loadingStart");
        }
    }

    IEnumerator DownloadFiles()
    {
        while (!Caching.ready)
            yield return null;

        using (www = WWW.LoadFromCacheOrDownload(url_city_online + "", 81))
        {
            print("Using Now");
            loadingPanel.SetActive(true);
            loadingStart = true;
            yield return www;
            AssetBundle city;
            if (!string.IsNullOrEmpty(www.error))
            {
                Debug.LogError(www.error);
                yield break;
            }
            else
            {
                city = www.assetBundle;
                print(city.name);

                for (int i = 0; i < city.GetAllAssetNames().Length; i++)
                {
                    Debug.Log(city.GetAllAssetNames()[i]);
                }
                loadingStart = false;
                string[] scenes = city.GetAllScenePaths();
                print("scenes.Lengths " + scenes.Length);
                foreach (string scenename in scenes)
                {
                    SceneNameToLoadAB = Path.GetFileNameWithoutExtension(scenename).ToString();
                    print("Scene: " + Path.GetFileNameWithoutExtension(scenename));
                    LobbyManager.manage.CreateRoomCity();
                    PlayerPrefs.SetInt("CityisLoaded", 1);
                }
            }
        }
    }

    IEnumerator City_single()
    {
        while (!Caching.ready)
            yield return null;

        using (www = WWW.LoadFromCacheOrDownload(url_city_single + "", 82))
        {
            if (PlayerPrefs.GetInt("citysingle") == 1)
            {
                loadingPanel.SetActive(true);
            } else
            {
                MainMenuManager.manage.loadingPanel.SetActive(true);
            }

            loadingStart = true;
            yield return www;
            AssetBundle city_single;
            if (!string.IsNullOrEmpty(www.error))
            {
                Debug.LogError(www.error);
                yield break;
            }
            else
            {
                city_single = www.assetBundle;
                print(city_single.name);

                for (int i = 0; i < city_single.GetAllAssetNames().Length; i++)
                {
                    Debug.Log(city_single.GetAllAssetNames()[i]);
                }
                loadingStart = false;
                string[] scenes = city_single.GetAllScenePaths();
                print("scenes.Lengths " + scenes.Length);
                foreach (string scenename in scenes)
                {
                    sceneNameCitySingle = Path.GetFileNameWithoutExtension(scenename).ToString();
                    print("Scene: " + Path.GetFileNameWithoutExtension(scenename));
                    PlayerPrefs.SetInt("citysingle", PlayerPrefs.GetInt("citysingle")+1);
                    MainMenuManager.manage.level_city();
                }
            }
        }
    }
    public void LoadAssetBundleSceneCity()
            {
                StartCoroutine(DownloadFiles());
                //SceneManager.LoadScene(SceneNameToLoadAB);
      }

    public void LoadCitySingle()
    {
        StartCoroutine(City_single());
    }

    }

