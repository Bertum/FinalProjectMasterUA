using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderService
{
    public void LoadScene(string sceneName)
    {

        PlayerPrefs.SetString(Constants.SCENETOLOAD, sceneName);
        SceneManager.LoadScene(SceneNames.LOADING_SCENE);
    }
}
