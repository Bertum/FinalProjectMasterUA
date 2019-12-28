using UnityEngine;
using UnityEngine.UI;

public class ConfirmationScreen : MonoBehaviour
{
    public GameObject Canvas;
    public Text TitleText;
    private string sceneName;
    private LanguageController languageController;
    private SceneLoaderService sceneLoaderService;

    private void Awake()
    {
        languageController = FindObjectOfType<LanguageController>();
        sceneLoaderService = new SceneLoaderService();
    }

    private void Start()
    {
        Canvas.SetActive(false);
    }

    public void ActivateCanvas(string scene, string place)
    {
        sceneName = scene;
        TitleText.text = string.Format(languageController.GetTextById("confirmation.body"), place);
        Canvas.SetActive(true);
    }

    public void LoadScene()
    {
        Canvas.SetActive(false);
        sceneLoaderService.LoadScene(sceneName);
    }

    public void ClosePanel()
    {
        Canvas.SetActive(false);
    }
}
