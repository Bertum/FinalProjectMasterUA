using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public Dropdown dropdown;


    private void Awake()
    {
        dropdown.value = PlayerPrefs.HasKey(Constants.LANGUAGEINDEXSELECTED) ? PlayerPrefs.GetInt(Constants.LANGUAGEINDEXSELECTED) : 0;
    }

    public void LanguageChanged(Dropdown dropdown)
    {
        PlayerPrefs.SetString(Constants.LANGUAGESELECTED, dropdown.captionText.text);
        PlayerPrefs.SetInt(Constants.LANGUAGEINDEXSELECTED, dropdown.value);
        LanguageController.instance.UpdateTexts();
    }
}
