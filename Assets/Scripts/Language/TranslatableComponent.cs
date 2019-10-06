using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TranslatableComponent : MonoBehaviour
{
    public string TextId;

    void Start()
    {
        ChangeTextLanguage();
    }
    public void ChangeTextLanguage()
    {
        this.GetComponent<Text>().text = LanguageController.instance.GetTextById(TextId);
    }

    private void OnEnable()
    {
        ChangeTextLanguage();
    }
}
