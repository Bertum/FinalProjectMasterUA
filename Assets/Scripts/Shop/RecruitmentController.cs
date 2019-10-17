﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecruitmentController : MonoBehaviour
{
    public GameObject npcToRecruit;
    public GameObject shopList;
    private PlayerDataController playerDataController;

    void Awake()
    {
        playerDataController = FindObjectOfType<PlayerDataController>();
    }

    private void Start()
    {
        List<NpcStats> npcs = new List<NpcStats>();
        for (int i = 0; i < 6; i++)
        {
            npcs.Add(new NpcStats(1, 2));
        }
        CreateList(npcs);
    }

    public void Buy(NpcStats npc, GameObject shopItem)
    {
        if (npc.cost <= playerDataController.PlayerData.CurrentGold)
        {
            playerDataController.PlayerData.CurrentGold -= npc.cost;
            playerDataController.PlayerData.CurrentCrew.Add(npc);
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
}
