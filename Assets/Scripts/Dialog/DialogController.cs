using UnityEngine;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{
    public GameObject DialogCanvas;
    public Text DialogText;
    private LanguageController languageController;

    #region Singleton
    public static DialogController instance
    {
        get
        {
            var gc = GameObject.FindObjectOfType<DialogController>();
            if (gc == null)
                throw new System.Exception("GameController not added to Scene");
            return gc;
        }
    }
    #endregion

    private void Awake()
    {
        languageController = GameObject.FindObjectOfType<LanguageController>();
        DialogCanvas.SetActive(false);
        ShowDialog("menu.start");
    }


    public void ShowDialog(string idText)
    {
        DialogText.text = languageController.GetTextById(idText);
        DialogCanvas.SetActive(true);
    }

    public void Accept()
    {
        DialogCanvas.SetActive(false);
    }

    public void Decline()
    {
        DialogCanvas.SetActive(false);
    }
}
