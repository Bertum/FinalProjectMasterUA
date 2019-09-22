using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LanguageController : MonoBehaviour
{
    private Dictionary<string, string> texts;
    private string languageSelected;

    #region Singleton
    public static LanguageController instance
    {
        get
        {
            var gc = GameObject.FindObjectOfType<LanguageController>();
            if (gc == null)
                throw new System.Exception("LanguageController not added to Scene");
            return gc;
        }
    }
    #endregion


    private void Awake()
    {
        GetCurrentLanguage();
    }

    private void GetCurrentLanguage()
    {
        languageSelected = PlayerPrefs.HasKey(Constants.LANGUAGESELECTED) ? PlayerPrefs.GetString(Constants.LANGUAGESELECTED) : Application.systemLanguage.ToString();
        var files = Resources.LoadAll<TextAsset>("Languages");
        //If the selected or default language doesn't exists takes english as a default language
        TextAsset selectedFile = files.Any(a => a.name == languageSelected) ? files.FirstOrDefault(f => f.name == languageSelected) : files.FirstOrDefault(f => f.name == SystemLanguage.English.ToString());
        texts = JsonConvert.DeserializeObject<Dictionary<string, string>>(selectedFile.ToString());
    }

    /// <summary>
    /// Get the text by id, if is not found return a default string
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public string GetTextById(string id)
    {
        string value = null;
        if (!texts.TryGetValue(id, out value))
        {
            value = string.Format("Text with id {0} not found", id);
        }
        return value;
    }

    public void UpdateTexts()
    {
        GetCurrentLanguage();
        var allActiveTexts = Resources.FindObjectsOfTypeAll<Text>();
        foreach (var text in allActiveTexts)
        {
            var translatableComponente = text.gameObject.GetComponent<TranslatableComponent>();
            if (translatableComponente != null)
            {
                text.text = GetTextById(translatableComponente.TextId);
            }
        }
    }
}
