using UnityEngine;

public class TestDialog : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DialogData data = new DialogBuilder().WithCharacterName("Paco").WithTextId("menu.start").Build();
        DialogController.instance.ShowDialog(data, Test, Test);
    }

    public void Test()
    {
        Debug.Log("Hello");
    }
}
