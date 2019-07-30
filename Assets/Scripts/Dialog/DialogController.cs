using UnityEngine;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{
    public GameObject DialogCanvas;
    public Text DialogText;

    #region Singleton
    public static DialogController instance
    {
        get
        {
            var gc = GameObject.FindObjectOfType<DialogController>();
            if (gc == null)
                throw new System.Exception("DialogController not added to Scene");
            return gc;
        }
    }
    #endregion

    private void Awake()
    {
        DialogCanvas.SetActive(false);
        ShowDialog("menu.start");
    }


    public void ShowDialog(string idText)
    {
        DialogText.text = LanguageController.instance.GetTextById(idText);
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
