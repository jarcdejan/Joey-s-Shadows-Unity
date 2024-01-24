using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{
    public bool paused;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !paused)
        {  
            Pause();
        }
    }

    public void Pause() {
        if (!paused) 
        {
            GameObject.Find("EventSystemSample").GetComponent<PauseGame>().paused = true;
            Time.timeScale = 0;
            Cursor.visible = true; // Set cursor to visible
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene("pauseScreen", LoadSceneMode.Additive);
        }
    }

    public void UnPause()
    {
        GameObject.Find("EventSystemSample").GetComponent<PauseGame>().paused = false;
        Time.timeScale = 1;
        Cursor.visible = false; // Set cursor to invisible when resuming the game
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.UnloadSceneAsync("pauseScreen");
    }
}