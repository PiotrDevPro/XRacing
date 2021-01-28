using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class playGameScript : MonoBehaviour
{
    public static playGameScript pgs;
    public string[] url, objName;

    static AssetBundle assetBundle;
    WWW www;
    public Text percent;
    public Image fg;
    // Start is called before the first frame update https://drive.google.com/uc?export=vi...

    bool loadingStart = false;
    public int game = 1;
    private void Awake()
    {
        if (pgs == null)
        {
            pgs = this;
        }

    }
    private void Start()
    {
        // StartCoroutine(s(3));
        //  SceneManager.LoadScene("turboStarScene");
        //  playGameScript.pgs.playGamePressed(3);
        playGamePressed(game);
    }


    private void Update()
    {
        if (loadingStart)
        {
            double v = www.progress;

            //fg.fillAmount = (float)v;//portrait
            fg.fillAmount = (float)v;//landscape

            v = System.Math.Round(v, 2);

            v *= 100;
            //percent.text = "" + v + "%";//portrait
            percent.text = "" + v + "%";//landscape
        }

    }

    public void playGamePressed(int i)
    {

        StartCoroutine(s(i));
    }
    IEnumerator s(int i)
    {
        if (!assetBundle)
        {
            using (www = new WWW(url[i]))
            {
                loadingStart = true;
                yield return www;
                if (!string.IsNullOrEmpty(www.error))
                {
                    print(www.error);
                    yield break;

                }
                assetBundle = www.assetBundle;

            }
        }
        loadingStart = false;
        string[] scenes = assetBundle.GetAllScenePaths();

        foreach (string s in scenes)
        {
            print(Path.GetFileNameWithoutExtension(s));
            //print(Path.GetFileNameWithoutExtension(s));
            //loadScene(Path.GetFileNameWithoutExtension(s));
            if (Path.GetFileNameWithoutExtension(s) == objName[i])
            {
                loadScene(Path.GetFileNameWithoutExtension(s));
            }
        }
    }


    public void loadScene(string name)
    {

        SceneManager.LoadScene(name);
    }




}
