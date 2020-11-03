using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Exit : MonoBehaviour
{

    public void MainMenu()
    {
        SceneManager.LoadScene("garage");
    }

    public void quit()
    {
        Application.Quit();
    }
}
