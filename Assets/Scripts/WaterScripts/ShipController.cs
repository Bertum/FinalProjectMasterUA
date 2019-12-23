using System;
using System.Collections.Generic;
using UnityEngine;
using static Constants;

public class ShipController : MonoBehaviour
{

    public float shipSpeed = 20;
    float offset = 0.5f;
    double domeRenderX = 0.5;
    float verticalInput;
    float movementFactor;
    float horizontalInput;
    float steerFactor;
    Rigidbody rigidBody;
    private Renderer domeRenderer;
    List<NpcStats> playerCrew = new List<NpcStats>();
    PlayerDataController pDController;
    UIController uiController;

    private void Awake()
    {
        GameObject dome = GameObject.FindGameObjectWithTag("Sky");
        domeRenderer = dome.GetComponent<Renderer>();
        domeRenderer.material.mainTextureOffset = new Vector2(offset, 0);
        domeRenderX = domeRenderer.material.mainTextureOffset.x;
        pDController = FindObjectOfType<PlayerDataController>().GetComponent<PlayerDataController>();
        uiController = FindObjectOfType<UIController>().GetComponent<UIController>();
    }


    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    public void SetInitialCrew()
    {
        if (!PlayerPrefs.HasKey(Constants.NEWGAME) || PlayerPrefs.GetInt(Constants.NEWGAME) == 1)
        {
            NpcStats[] crew = {
            new NpcStats("Smith", 2, Constants.EJobType.Official, 0, UnityEngine.Random.Range(4,6), UnityEngine.Random.Range(4, 6), UnityEngine.Random.Range(4, 6), UnityEngine.Random.Range(4, 6)),
            new NpcStats("Juanito", 1, Constants.EJobType.Rookie, 0, UnityEngine.Random.Range(3,5), UnityEngine.Random.Range(3, 5), UnityEngine.Random.Range(3, 5), UnityEngine.Random.Range(3, 5)),
            new NpcStats("Jorgito", 1, Constants.EJobType.Rookie, 0, UnityEngine.Random.Range(3, 5), UnityEngine.Random.Range(3, 5), UnityEngine.Random.Range(3, 5), UnityEngine.Random.Range(3, 5)),
            new NpcStats("Jaimito", 1, Constants.EJobType.Rookie, 0, UnityEngine.Random.Range(3, 5), UnityEngine.Random.Range(3, 5), UnityEngine.Random.Range(3, 5), UnityEngine.Random.Range(3, 5))};
            playerCrew = new List<NpcStats>(crew);
            pDController.PlayerData.CurrentCrew = playerCrew;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        Movement();
        ControlDayTime();
        uiController.ResourcesChanged(pDController.PlayerData);
        switch (domeRenderX)
        {
            case DayTime.Day:
                print("Day");
                CrewControl();
                LoyaltyCheck();
                break;
            case DayTime.MidDay:
                print("midDay");
                CrewControl();
                LoyaltyCheck();
                break;
            case DayTime.Afternoon:
                print("Afternoon");
                ShipRepairment();
                break;
            case DayTime.Night:
                print("Night");
                CrewControl();
                LoyaltyCheck();
                break;
            default:
                break;
        }
    }

    private void Movement()
    {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
        if (verticalInput > 0)
        {
            rigidBody.velocity = transform.forward * verticalInput * shipSpeed;
        }
        if (horizontalInput > 0)
        {
            transform.Rotate(new Vector3(0, 1, 0) * Time.deltaTime * shipSpeed, Space.World);
        }
        else if (horizontalInput < 0)
        {
            transform.Rotate(new Vector3(0, -1, 0) * Time.deltaTime * shipSpeed, Space.World);
        }
    }

    private void ControlDayTime()
    {
        if (offset > 1.5f)
        {
            offset = 0.4999f;
        }
        offset += 0.0001f;
        domeRenderer.material.mainTextureOffset = new Vector2(offset, 0);
        domeRenderX = Math.Round(domeRenderer.material.mainTextureOffset.x, 4);
    }

    private void ShipRepairment()
    {
        //TODO: If ShipHealth < 100% and carpenter in crew and wood, then ship gets fixed.
    }

    //Main function for the actions that happens while navigating in your crew
    private void CrewControl()
    {
        for (int i = pDController.PlayerData.CurrentCrew.Count - 1; i >= 0; i--)
        {
            ConsumeFood(pDController.PlayerData.CurrentCrew[i]);
            ConsumeWater(pDController.PlayerData.CurrentCrew[i]);
            HealSickness(pDController.PlayerData.CurrentCrew[i]);
            HealInjuries(pDController.PlayerData.CurrentCrew[i]);
            Sickness(pDController.PlayerData.CurrentCrew[i]);
        }
    }

    private void ConsumeFood(NpcStats crewMember)
    {
        //TODO: Each npc eats 3 meals per day, if not, sick level increases.
        if (pDController.PlayerData.CurrentFood > 0)
        {
            pDController.PlayerData.CurrentFood -= 1;
        } else {
            crewMember.IncreaseSicknessLevel(1);
        }
    }

    private void ConsumeWater(NpcStats crewMember)
    {
        //TODO: Each npc drinks 3 waters per day, if not, sick level increases.        
        if (pDController.PlayerData.CurrentWater > 0) {
            pDController.PlayerData.CurrentWater -= 1;
        } else {
            crewMember.IncreaseSicknessLevel(2);
        }
    }

    private void HealSickness(NpcStats crewMember)
    {
        //TODO: If medic in crew AND it has medicines, sick level of all decreasesx2. If not decreases.
        //If medic is sick, shit happens
        NpcStats medic = CheckForMedic();
        if (medic != null && medic.GetSickLevel() < 10 && pDController.PlayerData.CurrentMedicine > 0)
        {
            pDController.PlayerData.CurrentMedicine -= 1;
            crewMember.DecreaseSicknessLevel(2);
        }
    }

    private void HealInjuries(NpcStats crewMember)
    {
        if (crewMember.GetSickLevel() < 10 && crewMember.GetNpcCurrentHealth() < crewMember.GetNpcMaximunHealth())
        {
            crewMember.Recover();
        }
    }

    private NpcStats CheckForMedic()
    {
        foreach (NpcStats npc in playerCrew)
        {
            if (npc.npcJob == EJobType.Medic)
            {
                return npc;
            }
        }
        return null;
    }

    private void Sickness(NpcStats crewMember)
    {
        //TODO: If sickLevel of npc > 10, health is lost.
        //TODO: If sickLevel of npc > 20, attributes and health are lost.
        //TODO: If sickLevel of npc > 30 or health = 0, RIP, all npcs loses 10 of loyalty
        int sickLevel = crewMember.GetSickLevel();
        if (sickLevel >= 10)
        {
            crewMember.OnDamageReceived(2);
        }
        if (sickLevel >= 20)
        {
            crewMember.OnDamageReceived(4);
            crewMember.DecreaseStats();
        }
        if (sickLevel >= 30)
        {
            crewMember.OnDamageReceived(100);
        }
        if (crewMember.GetNpcCurrentHealth() <= 0)
        {
            playerCrew.Remove(crewMember);
        }

    }

    private void LoyaltyCheck()
    {
        //TODO: Loyalty of an npc -sickLevel
        //TODO: If overall loyalty < 50 AND , YOU DIED
        int globalLoyalty = 0;
        foreach (NpcStats crewMember in playerCrew)
        {
            crewMember.DecreaseLoyalty(crewMember.GetSickLevel());
            globalLoyalty += crewMember.GetLoyalty();
        }
        if (globalLoyalty < 50)
        {
            //YOU DIED
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "border")
        {
            rigidBody.velocity = Vector3.zero;
        }
    }
}
