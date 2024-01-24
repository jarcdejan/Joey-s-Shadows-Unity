using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
 
public class Music : MonoBehaviour
{
    public static Music instance;
 
    void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void DestroyExistingInstance()
    {
        // Check if the current scene is the scene where you want to destroy the music
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "SampleScene" || currentScene.name == "pauseScreen")
        {
            Destroy(instance.gameObject);
        }
    }
}