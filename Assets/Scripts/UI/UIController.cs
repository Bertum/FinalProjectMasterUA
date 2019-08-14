using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text goldText, foodText, piratesText;

    public void ResourcesChanged(int currentGold, int totalGold, int currentFood, int totalFood, int currentPirates, int totalPirates)
    {
        goldText.text = string.Format("{0} / {1}", currentGold.ToString(), totalGold.ToString());
        foodText.text = string.Format("{0} / {1}", currentFood.ToString(), totalFood.ToString());
        piratesText.text = string.Format("{0} / {1}", currentPirates.ToString(), totalPirates.ToString());
    }
}
