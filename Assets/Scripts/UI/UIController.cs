using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text goldText, foodText, piratesText, waterText, medicineText;

    public void ResourcesChanged(PlayerData playerData)
    {
        goldText.text = string.Format("{0} / {1}", playerData.CurrentGold.ToString(), playerData.TotalGold.ToString());
        foodText.text = string.Format("{0} / {1}", playerData.CurrentFood.ToString(), playerData.TotalFood.ToString());
        piratesText.text = string.Format("{0} / {1}", playerData.CurrentCrew.Count.ToString(), playerData.TotalPirates.ToString());
        waterText.text = string.Format("{0} / {1}", playerData.CurrentWater.ToString(), playerData.TotalWater.ToString());
        medicineText.text = string.Format("{0} / {1}", playerData.CurrentMedicine.ToString(), playerData.TotalMedicine.ToString());
    }
}
