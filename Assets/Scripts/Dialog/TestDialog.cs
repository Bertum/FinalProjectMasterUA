using UnityEngine;

public class TestDialog : MonoBehaviour
{
    void Start()
    {
        DialogController.instance.ShowDialog("quest.test", null);
    }
}
