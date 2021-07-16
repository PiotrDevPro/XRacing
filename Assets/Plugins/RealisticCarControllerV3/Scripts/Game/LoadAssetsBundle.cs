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
    [SerializeField] string url = "https://firebasestorage.googleapis.com/v0/b/xracing-290708.appspot.com/o/cuvex?alt=media&token=95adc7db-53ce-4632-bcfd-7e3210aadace";
    [SerializeField] string url_city_online = "https://drive.google.com/uc?export=download&id=1pggf9vh2u7-UXf_CBO8N8j6f6g_p-kZN";
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
    void Start()
    {
        
        //WWW www = new WWW(url_lev);
        // StartCoroutine(webReq(www));

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
    IEnumerator webReq(WWW www)
    {
        yield return www;

        while (www.isDone == false)
        {
            yield return null;
        }

        AssetBundle bundle = www.assetBundle;
        if (www.error == null)
        {
            GameObject obj = (GameObject)bundle.LoadAsset("cuvex");
            Instantiate(obj);
            print("Assets loading is completed");
        }
        else
        {
            Debug.Log(www.error);
        }
    }
    IEnumerator DownloadFiles()
    {
        if (!assetBundle)
        {
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
