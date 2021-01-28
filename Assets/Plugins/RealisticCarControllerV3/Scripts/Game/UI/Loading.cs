using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Loading : MonoBehaviour
{
    public Button load;

    private void Start()
    {
        
    }

    public void loadLevel()
    {
        SceneManager.LoadScene("garage");
    }


}
