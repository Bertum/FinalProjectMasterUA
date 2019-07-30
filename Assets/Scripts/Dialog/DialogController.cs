using UnityEngine;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{
    public GameObject DialogCanvas;
    public Text DialogText;
    public Image Image;
    private bool isQuest;

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
    }


    public void ShowDialog(string idText, Image image, bool isQuest = false)
    {
        DialogText.text = LanguageController.instance.GetTextById(idText);
        Image = image;
        this.isQuest = isQuest;
        DialogCanvas.SetActive(true);
    }

    public void Accept()
    {
        //Save the quest if neccesary
        DialogCanvas.SetActive(false);
    }

    public void Decline()
    {
        DialogCanvas.SetActive(false);
    }
}
