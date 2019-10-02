using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreenController : MonoBehaviour
{
    public Text loadingText;
    public Text tipText;
    public Slider slider;
    private string sceneName;
    private List<string> tips;
    private float changeTipTimer;
    private readonly float timeToChangeTip = 3;
    private AsyncOperation asyncOperation;

    private void Awake()
    {
        changeTipTimer = 0;
        tips = LanguageController.instance.GetTips();
        sceneName = PlayerPrefs.GetString(Constants.SCENETOLOAD);
        GetNewTip();
    }

    private void Start()
    {
        StartCoroutine(LoadNewScene());
    }

    private void Update()
    {
        changeTipTimer += Time.deltaTime;
        loadingText.color = new Color(loadingText.color.r, loadingText.color.g, loadingText.color.b, Mathf.PingPong(Time.time, 1));
        if (changeTipTimer >= timeToChangeTip)
        {
            changeTipTimer = 0;
            GetNewTip();
            asyncOperation.allowSceneActivation = true;
        }
    }

    private void GetNewTip()
    {
        if (tips.Count > 0)
        {
            tipText.text = tips[Random.Range(0, tips.Count)];
        }
    }

    IEnumerator LoadNewScene()
    {
        yield return new WaitForSeconds(1);
        asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        asyncOperation.allowSceneActivation = false;

        while (!asyncOperation.isDone)
        {
            float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);
            slider.value = progress;
            yield return null;
        }
    }
}
