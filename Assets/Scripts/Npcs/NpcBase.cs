using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Constants;

public class NpcBase : MonoBehaviour {

    //Attributes definition.
    public string npcName = "";
    public int level = 0;
    public JobClass jobClass;
    public int strength = 0;
    public int dexterity = 0;
    public int intelligence = 0;
    public int sight = 0;
    private int maxHealthPoints;
    private int currentHealthPoints;

    //Constructor for player npcs
    public NpcBase(string npcName, int npclevel, EJobType npcJob, int npcJobLevel, int npcStrength, int npcDexterity, int npcIntelligence, int npcSight) {
        this.npcName = npcName;
        this.level = npclevel;
        this.jobClass = new JobClass(npcJob, npcJobLevel);
        this.strength = npcStrength;
        this.dexterity = npcDexterity;
        this.intelligence = npcIntelligence;
        this.sight = npcSight;
        setMaxHP();
    }

    //Constructor for random enemies
    public NpcBase(int npclevel, EJobType npcJob, int npcJobLevel, int npcStrength, int npcDexterity) {
        level = npclevel;
        jobClass = new JobClass(npcJob, npcJobLevel);
        strength = npcStrength;
        dexterity = npcDexterity;
        setMaxHP();
    }

    public void SetNewName(string newName) {
        name = newName;
    }

    public void SwitchJob(EJobType newJob) {
        this.jobClass.SwitchJobClass(newJob);
    }

    private void setMaxHP() {
        //TODO - instead of 10, depends on job type.
        switch (jobClass.jobType) {
            case EJobType.Rookie:
                maxHealthPoints = 10 + strength + jobClass.jobLevel;
                break;
            case EJobType.Official:
                maxHealthPoints = 15 + strength + jobClass.jobLevel;
                break;
            case EJobType.Cook:
                maxHealthPoints = 5 + jobClass.jobLevel;
                break;
            case EJobType.Pilot:
                maxHealthPoints = 5 + strength + jobClass.jobLevel;
                break;
            case EJobType.Searcher:
                maxHealthPoints = 10 + strength + jobClass.jobLevel;
                break;
            default:
                break;
        }        
        currentHealthPoints = maxHealthPoints;
    }

    //To use in a battle to hurt enemies
    public int AttackDamage() {
        return strength+ jobClass.jobLevel;
    }

    //To use in a battle to avoid damage
    public int EvadeDamage() {
        return dexterity + jobClass.jobLevel;
    }

    //To use when AttackDamage received > this Evade
    public void OnDamageReceived(int damage) {
        currentHealthPoints -= (damage-dexterity/2);
    }
}
