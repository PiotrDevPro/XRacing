using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AndroidSplitLoadFirstScene : MonoBehaviour
{
    private string nextScene = "level_lap6";
    private bool obbisok = false;
    private bool loading = false;
    private bool replacefiles = false;

	private string[] paths =
    {
		"Scene/garage.scene",
		"Scene/level_lap6.scene"
	};

    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
			
            if (Application.dataPath.Contains(".obb") && !obbisok)
            {
				//StartCoroutine(CheckSetUp());
				//obbisok = true;
			}
		}
		else
		{
			if (!loading)
			{
				StartApp();
			}
		}
	}

	public void StartApp()
	{
		loading = true;
		//SceneManager.LoadSceneAsync(nextScene);
	}

	public void LevelStart()
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			print("Application.platform == RuntimePlatform.Android");
			if (Application.dataPath.Contains(".obb") && !obbisok)
			{
				print("Application.dataPath.Contains(.obb) && !obbisok");
				StartCoroutine(CheckSetUp());
			}
		}
	}

	public IEnumerator CheckSetUp()
	{
		//Check and install!
		for (int i = 0; i < paths.Length; ++i)
		{
			yield return StartCoroutine(PullStreamingAssetFromObb(paths[i]));
		}
		yield return new WaitForSeconds(1f);
		StartApp();
	}


	public IEnumerator PullStreamingAssetFromObb(string sapath)
	{
		if (!File.Exists(Application.persistentDataPath + sapath) || replacefiles)
		{
			WWW unpackerWWW = new WWW(Application.streamingAssetsPath + "/" + sapath);
			while (!unpackerWWW.isDone)
			{
				yield return null;
			}
			if (!string.IsNullOrEmpty(unpackerWWW.error))
			{
				Debug.Log("Error unpacking:" + unpackerWWW.error + " path: " + unpackerWWW.url);

				yield break; //skip it
			}
			else
			{
				Debug.Log("Extracting " + sapath + " to Persistant Data");

				if (!Directory.Exists(Path.GetDirectoryName(Application.persistentDataPath + "/" + sapath)))
				{
					Directory.CreateDirectory(Path.GetDirectoryName(Application.persistentDataPath + "/" + sapath));
				}
				File.WriteAllBytes(Application.persistentDataPath + "/" + sapath, unpackerWWW.bytes);
			}
		}
		yield return 0;
	}
}
