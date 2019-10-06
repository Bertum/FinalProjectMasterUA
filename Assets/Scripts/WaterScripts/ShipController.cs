using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Constants;

public class ShipController : MonoBehaviour {

    public float shipSpeed = 20;
    float offset;

    float verticalInput;
    float movementFactor;
    float horizontalInput;
    float steerFactor;
    Rigidbody rigidBody;

    List<NpcBase> playerCrew = new List<NpcBase>();
    List<ShipResources> resources = new List<ShipResources>();

    private void Start() {
        rigidBody = GetComponent<Rigidbody>();
    }

    public void SetInitialCrew() {
        NpcBase[] crew = {
            new NpcBase("Smith", 2, Constants.EJobType.Official, 0, UnityEngine.Random.Range(4,6), UnityEngine.Random.Range(4, 6), UnityEngine.Random.Range(4, 6), UnityEngine.Random.Range(4, 6)),
            new NpcBase("Juanito", 1, Constants.EJobType.Rookie, 0, UnityEngine.Random.Range(3,5), UnityEngine.Random.Range(3, 5), UnityEngine.Random.Range(3, 5), UnityEngine.Random.Range(3, 5)),
            new NpcBase("Jorgito", 1, Constants.EJobType.Rookie, 0, UnityEngine.Random.Range(3, 5), UnityEngine.Random.Range(3, 5), UnityEngine.Random.Range(3, 5), UnityEngine.Random.Range(3, 5)),
            new NpcBase("Jaimito", 1, Constants.EJobType.Rookie, 0, UnityEngine.Random.Range(3, 5), UnityEngine.Random.Range(3, 5), UnityEngine.Random.Range(3, 5), UnityEngine.Random.Range(3, 5))};
        playerCrew = new List<NpcBase>(crew);
    }

    public void SetInitialResources() {
        resources.Add(new ShipResources(EResourceType.Food, 120));
        resources.Add(new ShipResources(EResourceType.Water, 80));
        resources.Add(new ShipResources(EResourceType.Gold, 1000));
        resources.Add(new ShipResources(EResourceType.Medicine, 0));
    }

    // Update is called once per frame
    void Update() {
        Movement();
        DayTime();
        //CrewControl();
        //LoyaltyCheck();
        //ShipRepairment();
    }

    private void Movement() {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
        if (verticalInput > 0) {
            rigidBody.velocity = transform.forward * verticalInput * shipSpeed;
        }
        if (horizontalInput > 0) {
            transform.Rotate(new Vector3(0, 1, 0) * Time.deltaTime * shipSpeed, Space.World);
        } else if (horizontalInput < 0) {
            transform.Rotate(new Vector3(0, -1, 0) * Time.deltaTime * shipSpeed, Space.World);
        }
    }

    private void DayTime() {
        GameObject dome = GameObject.FindGameObjectWithTag("Sky");
        Renderer domeRenderer = dome.GetComponent<Renderer>();
        offset += Time.deltaTime / (24);
        domeRenderer.material.mainTextureOffset = new Vector2(offset, 0);
    }

    private void ShipRepairment() {
        //TODO: If ShipHealth < 100% and carpenter in crew and wood, then ship gets fixed.
    }

    //Main function for the actions that happens while navigating in your crew
    private void CrewControl() {
        foreach (NpcBase crewMember in playerCrew) {
            ConsumeFood(crewMember);
            ConsumeWater(crewMember);
            HealSickness(crewMember);
            Sickness(crewMember);
            HealInjuries(crewMember);
        }
    }

    private void ConsumeFood(NpcBase crewMember) {
        //TODO: Each npc eats 3 meals per day, if not, sick level increases.
        if (resources[1].Consume(3)) {
            crewMember.IncreaseSicknessLevel(1);
        }
    }

    private void ConsumeWater(NpcBase crewMember) {
        //TODO: Each npc drinks 2 waters per day, if not, sick level increases.        
        if (resources[1].Consume(2)) {
            crewMember.IncreaseSicknessLevel(2);
        }
    }

    private void HealSickness(NpcBase crewMember) {
        //TODO: If medic in crew AND it has medicines, sick level of all decreasesx2. If not decreases.
        //If medic is sick, shit happens
        NpcBase medic = CheckForMedic();
        if (medic.GetSickLevel() < 10 && resources[3].quantity > 0) {
            if (resources[3].Consume(1)) {
                crewMember.DecreaseSicknessLevel(2);
            }
        }
    }

    private void HealInjuries(NpcBase crewMember) {
        if (crewMember.GetSickLevel() < 10 && crewMember.GetNpcCurrentHealth() < crewMember.GetNpcMaximunHealth()) {
            crewMember.Recover();
        }
    }

    private NpcBase CheckForMedic() {
        foreach (NpcBase npc in playerCrew) {
            if (npc.npcJob == EJobType.Medic) {
                return npc;
            }
        }
        return null;
    }

    private void Sickness(NpcBase crewMember) {
        //TODO: If sickLevel of npc > 10, health is lost.
        //TODO: If sickLevel of npc > 20, attributes and health are lost.
        //TODO: If sickLevel of npc > 30 or health = 0, RIP, all npcs loses 10 of loyalty
        int sickLevel = crewMember.GetSickLevel();
        if (sickLevel >= 10) {
            crewMember.OnDamageReceived(2);
        }
        if (sickLevel >= 20) {
            crewMember.OnDamageReceived(4);
            crewMember.DecreaseStats();
        }
        if (sickLevel >= 30) {
            crewMember.OnDamageReceived(100);
        }
        if (crewMember.GetNpcCurrentHealth() <= 0) {
            playerCrew.Remove(crewMember);
        }

    }

    private void LoyaltyCheck() {
        //TODO: Loyalty of an npc -sickLevel
        //TODO: If overall loyalty < 50 AND , YOU DIED
        int globalLoyalty = 0;
        foreach (NpcBase crewMember in playerCrew) {
            crewMember.DecreaseLoyalty(crewMember.GetSickLevel());
            globalLoyalty += crewMember.GetLoyalty();
        }
        if (globalLoyalty < 50) {
            //YOU DIED
        }
    }
}
