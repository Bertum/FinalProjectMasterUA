using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text goldText, foodText, piratesText, waterText, medicineText;

    public void ResourcesChanged(PlayerData playerData)
    {
        if (goldText != null)
            goldText.text = string.Format("{0} / {1}", playerData.CurrentGold.ToString(), playerData.TotalGold.ToString());
        if (foodText != null)
            foodText.text = string.Format("{0} / {1}", playerData.CurrentFood.ToString(), playerData.TotalFood.ToString());
        if (piratesText != null)
            piratesText.text = string.Format("{0} / {1}", playerData.CurrentCrew.Count.ToString(), playerData.TotalPirates.ToString());
        if (waterText != null)
            waterText.text = string.Format("{0} / {1}", playerData.CurrentWater.ToString(), playerData.TotalWater.ToString());
        if (medicineText != null)
            medicineText.text = string.Format("{0} / {1}", playerData.CurrentMedicine.ToString(), playerData.TotalMedicine.ToString());
    }
}
