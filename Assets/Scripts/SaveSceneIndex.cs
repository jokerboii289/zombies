using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveSceneIndex : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetInt("Index") == SceneManager.sceneCountInBuildSettings) // if reached last lvl
        {
            SceneManager.LoadScene("level 1");
        }
        else if(PlayerPrefs.GetInt("Index")<=0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + PlayerPrefs.GetInt("Index"));
    }
}