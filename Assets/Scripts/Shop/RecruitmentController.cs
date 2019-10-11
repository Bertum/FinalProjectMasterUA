using System.Collections.Generic;
using UnityEngine;
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

    public void Buy(EJobType type)
    {
    }

    public void CreateList(List<EJobType> jobs)
    {
        foreach (var job in jobs)
        {
            var shopItem = Instantiate(npcToRecruit);

            npcToRecruit.transform.parent = shopList.transform;
        }
    }
}
