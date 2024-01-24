using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSwitcher : MonoBehaviour {
    public string sceneName;
    public string unloadSceneName;

    // load selected scene
    public void ChangeScene() {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }

    public void UnloadCurrentScene() {
        SceneManager.UnloadSceneAsync(unloadSceneName);
    }

    public void switchScene() {
        UnloadCurrentScene();
        SceneManager.LoadScene(sceneName);
    }
}
