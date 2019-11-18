using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RecruitmentController : MonoBehaviour
{
    public GameObject npcToRecruit;
    public GameObject shopList;
    public GameObject shopCanvas;
    private PlayerDataController playerDataController;
    private UIController uiController;
    private List<Sprite> portraits;

    void Awake()
    {
        portraits = new List<Sprite>();
        playerDataController = FindObjectOfType<PlayerDataController>();
        uiController = FindObjectOfType<UIController>();
        portraits = Resources.LoadAll<Sprite>("Sprites").ToList();
        shopCanvas.SetActive(false);
    }

    public void Buy(NpcStats npc, GameObject shopItem)
    {
        if (npc.cost <= playerDataController.PlayerData.CurrentGold)
        {
            playerDataController.PlayerData.CurrentGold -= npc.cost;
            playerDataController.PlayerData.CurrentCrew.Add(npc);
            uiController.ResourcesChanged(playerDataController.PlayerData);
            shopItem.SetActive(false);
        }
    }

    public void CreateList(List<NpcStats> npcs)
    {
        foreach (var npc in npcs)
        {
            var shopItem = Instantiate(npcToRecruit, shopList.transform);
            var jobText = LanguageController.instance.GetTextById(string.Format("job.{0}", npc.npcJob.ToString().ToLower()));
            shopItem.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = jobText;
            var rnd = Random.Range(0, portraits.Count);
            //Image
            shopItem.transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite = portraits[rnd];
            //Stregnth
            shopItem.transform.GetChild(0).GetChild(2).GetChild(1).GetComponent<Text>().text = npc.strength.ToString();
            //Dexterity
            shopItem.transform.GetChild(0).GetChild(2).GetChild(3).GetComponent<Text>().text = npc.dexterity.ToString();
            //Intelligence
            shopItem.transform.GetChild(0).GetChild(2).GetChild(5).GetComponent<Text>().text = npc.intelligence.ToString();
            //Button
            shopItem.transform.GetChild(0).GetChild(3).GetComponent<Button>().onClick.AddListener(delegate { Buy(npc, shopItem); });
            //Cost Amount
            shopItem.transform.GetChild(0).GetChild(5).GetComponent<Text>().text = string.Format("{0}$", npc.cost.ToString());
        }
    }

    public void OpenShop(int islandLevel)
    {
        if (!shopCanvas.activeSelf)
        {
            foreach (Transform child in shopList.transform)
            {
                Destroy(child.gameObject);
            }
            List<NpcStats> npcs = new List<NpcStats>();
            for (int i = 0; i < 6; i++)
            {
                npcs.Add(new NpcStats(1, islandLevel));
            }
            CreateList(npcs);
            shopCanvas.SetActive(true);
        }
    }

    public void CloseShop()
    {
        shopCanvas.SetActive(false);
    }
}
