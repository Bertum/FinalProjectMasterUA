using UnityEngine;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{
    public GameObject DialogCanvas;
    public Text DialogText;
    public Text CharacterName;
    public Image Image;
    private bool isQuest;

    #region Singleton
    public static DialogController instance
    {
        get
        {
            var gc = FindObjectOfType<DialogController>();
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


    public void ShowDialog(DialogData data)
    {
        DialogText.text = LanguageController.instance.GetTextById(data.TextId);
        Image = data.Image;
        isQuest = data.IsQuest;
        CharacterName.text = data.CharacterName;
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
