using UnityEngine;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{
    public GameObject DialogCanvas;
    public Text DialogText, Title;
    public Image Image;
    private bool isQuest;
    [HideInInspector]
    public delegate void FunctionCallback();
    [HideInInspector]
    public FunctionCallback successFunction, declineFunction;

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


    public void ShowDialog(DialogData data, FunctionCallback success, FunctionCallback decline)
    {
        successFunction = success;
        declineFunction = decline;
        DialogText.text = LanguageController.instance.GetTextById(data.TextId);
        Image = data.Image;
        isQuest = data.IsQuest;
        Title.text = data.CharacterName;
        DialogCanvas.SetActive(true);
    }

    public void Accept()
    {
        //Save the quest if neccesary
        successFunction();
        successFunction?.Invoke();
        successFunction = null;
        DialogCanvas.SetActive(false);
    }

    public void Decline()
    {
        declineFunction?.Invoke();
        declineFunction = null;
        DialogCanvas.SetActive(false);
    }
}
