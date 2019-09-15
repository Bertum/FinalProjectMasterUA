using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public Dropdown dropdown;
    public GameObject btnContinue;
    private SaveLoadService saveLoadService;
    private SceneLoaderService sceneLoaderService;

    private void Awake()
    {
        dropdown.value = PlayerPrefs.HasKey(Constants.LANGUAGEINDEXSELECTED) ? PlayerPrefs.GetInt(Constants.LANGUAGEINDEXSELECTED) : 0;
        saveLoadService = new SaveLoadService();
        sceneLoaderService = new SceneLoaderService();
        btnContinue.SetActive(saveLoadService.CheckExistSave());
    }

    public void LanguageChanged(Dropdown dropdown)
    {
        PlayerPrefs.SetString(Constants.LANGUAGESELECTED, dropdown.captionText.text);
        PlayerPrefs.SetInt(Constants.LANGUAGEINDEXSELECTED, dropdown.value);
        LanguageController.instance.UpdateTexts();
    }

    public void LoadSavedGame()
    {
        if (saveLoadService.CheckExistSave())
        {
            PlayerPrefs.SetInt(Constants.NEWGAME, 0);
            sceneLoaderService.LoadScene(SceneNames.GAME);
        }
    }

    public void LoadNewGame()
    {
        PlayerPrefs.SetInt(Constants.NEWGAME, 1);
        sceneLoaderService.LoadScene(SceneNames.GAME);
    }
}
