using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreenController : MonoBehaviour
{
    public Text loadingText;
    private string sceneName;

    private void Awake()
    {
        sceneName = PlayerPrefs.GetString(Constants.SCENETOLOAD);
    }

    void Update()
    {
        StartCoroutine(LoadNewScene());
        loadingText.color = new Color(loadingText.color.r, loadingText.color.g, loadingText.color.b, Mathf.PingPong(Time.time, 1));
    }

    IEnumerator LoadNewScene()
    {
        yield return new WaitForSeconds(3);

        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);

        while (!async.isDone)
        {
            yield return null;
        }
    }
}
