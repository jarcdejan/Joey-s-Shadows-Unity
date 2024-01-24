using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class unPauseGame : MonoBehaviour
{
    public void UnPause()
    {
        Time.timeScale = 1;
        Cursor.visible = false; // Set cursor to invisible when resuming the game
        Cursor.lockState = CursorLockMode.Locked;

        if (SceneManager.GetSceneByName("pauseScreen").isLoaded)
        {
            SceneManager.UnloadSceneAsync("pauseScreen");
        }
    }
}
