using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Constants;

public class NpcBase : MonoBehaviour {

    enum EState {
        Idle,
        Busy,
        Sliding
    }

    //Attributes definition.
    public string npcName = "";
    public int level = 0;
    public int jobLevel;
    public EJobType npcJob;    
    public int strength = 0;
    public int dexterity = 0;
    public int intelligence = 0;
    public int sight = 0;
    private float maxHealthPoints;
    private float currentHealthPoints;
    private bool bPlayerTeam;
    private JobClass jobClass;
    private Vector3 startPosition;
    private Vector3 slideTargetPosition;
    private EState state;
    public Slider healthBarSlider;
    private Action onSlideCompleted;
    private Action onAttackCompleted;

    private void Start() {
        this.jobClass = new JobClass(npcJob, jobLevel);
        setMaxHP();
        state = EState.Idle;
        startPosition = transform.position;
        this.healthBarSlider.value = CalculateHealthBarValue();
    }

    private void Update() {
        switch (state) {
            case EState.Idle:
                break;
            case EState.Busy:
                break;
            case EState.Sliding:
                transform.position = Vector3.MoveTowards(transform.position, slideTargetPosition, 5 * Time.deltaTime);
                if(Vector3.Distance(transform.position,slideTargetPosition) < 1f) {
                    transform.position = slideTargetPosition;
                    onSlideCompleted();
                }
                break;
        }
        this.healthBarSlider.value = CalculateHealthBarValue();
    }

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
        this.healthBarSlider.value = CalculateHealthBarValue();
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
        npcName = newName;
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

    public void AttackEnemy(NpcBase defender, Action onAttackCompleted) {
        if (this.AttackDamage() > defender.EvadeDamage()) {
            //transform.LookAt(defender.transform.position);
            Vector3 slideTargetPosition = defender.transform.position;
            SlideToPosition(slideTargetPosition, () => {
                state = EState.Busy;                
                defender.OnDamageReceived(this.AttackDamage());
                SlideToPosition(startPosition, () => {
                    state = EState.Idle;
                    onAttackCompleted();
                });
            });
        } else {
            Vector3 slideTargetPosition = defender.transform.position;
            SlideToPosition(slideTargetPosition, () => {
                state = EState.Busy;
                print(this.npcName+" Missed "+defender.npcName+"!");
                SlideToPosition(startPosition, () => {
                    state = EState.Idle;
                    onAttackCompleted();
                });
            });
        }
    }

    //To use in a battle to hurt enemies
    private int AttackDamage() {
        return strength + jobClass.jobLevel + UnityEngine.Random.Range(0,strength);
    }

    //To use in a battle to avoid damage
    private int EvadeDamage() {
        return dexterity + jobClass.jobLevel;
    }

    //To use when AttackDamage received > this Evade
    private void OnDamageReceived(int damage) {
        this.currentHealthPoints -= (damage-dexterity/2);
    }

    public bool GetPlayerTeam() {
        return bPlayerTeam;
    }

    public void SetPlayerTeam(bool isInPlayerTeam) {
        bPlayerTeam = isInPlayerTeam;
    }

    public float getNpcMaximunHealth() {
        return this.maxHealthPoints;
    }

    public float GetNpcCurrentHealth() {
        return this.currentHealthPoints;
    }

    private float CalculateHealthBarValue() {
        return (this.currentHealthPoints/maxHealthPoints);
    }
         
    private void SlideToPosition(Vector3 position, Action onSlideComplete) {
        this.slideTargetPosition = position;
        this.onSlideCompleted = onSlideComplete;
        state = EState.Sliding;
    }

}
