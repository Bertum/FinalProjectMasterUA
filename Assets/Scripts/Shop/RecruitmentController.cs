using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Constants;

public class RecruitmentController : MonoBehaviour
{
    public GameObject npcToRecruit;
    public GameObject shopList;
    private PlayerDataController playerDataController;

    void Awake()
    {
        playerDataController = GameObject.FindObjectOfType<PlayerDataController>();
    }

    private void Start()
    {
        var list = new List<EJobType>() { EJobType.Carpenter, EJobType.Cook };
        CreateList(list);
    }



    public void Buy(EJobType type)
    {
    }

    public void CreateList(List<EJobType> jobs)
    {
        foreach (var job in jobs)
        {
            var shopItem = Instantiate(npcToRecruit, shopList.transform);
            var jobText = LanguageController.instance.GetTextById(string.Format("job.{0}", job.ToString().ToLower()));
            shopItem.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = jobText;
            //Stregnth
            shopItem.transform.GetChild(0).GetChild(2).GetChild(1).GetComponent<Text>().text = "1";
            //Dexterity
            shopItem.transform.GetChild(0).GetChild(2).GetChild(3).GetComponent<Text>().text = "2";
            //Intelligence
            shopItem.transform.GetChild(0).GetChild(2).GetChild(5).GetComponent<Text>().text = "3";
        }
    }
}
