using System;
using UnityEngine;
using UnityEngine.UI;
using static Constants;
[System.Serializable]

public class NpcBase : MonoBehaviour
{

    enum EState
    {
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
    public int cost;
    private float maxHealthPoints;
    private float currentHealthPoints;
    //Team definition
    private bool bPlayerTeam;
    //Job variable
    private JobClass jobClass;
    //Combat variables
    private Vector3 startPosition;
    private Vector3 slideTargetPosition;
    private EState state;
    private Action onSlideCompleted;
    private Action onAttackCompleted;
    //Health Slider
    public Slider healthBarSlider;
    //Sickness variable
    private int sickLevel;
    private bool bSick;
    //Loyalty variable
    private int loyalty;

    private void Start()
    {
        this.jobClass = new JobClass(npcJob, jobLevel);
        SetMaxHP();
        state = EState.Idle;
        this.healthBarSlider.value = CalculateHealthBarValue();
    }

    private void Update()
    {
        switch (state)
        {
            case EState.Idle:
                break;
            case EState.Busy:
                break;
            case EState.Sliding:
                transform.position = Vector3.MoveTowards(transform.position, slideTargetPosition, 5 * Time.deltaTime);
                if (Vector3.Distance(transform.position, slideTargetPosition) < 1f)
                {
                    transform.position = slideTargetPosition;
                    onSlideCompleted();
                }
                break;
        }
        this.healthBarSlider.value = CalculateHealthBarValue();
    }

    //Constructor for player npcs
    public NpcBase(string npcName, int npclevel, EJobType npcJob, int npcJobLevel, int npcStrength, int npcDexterity, int npcIntelligence, int npcSight)
    {
        this.npcName = npcName;
        this.level = npclevel;
        this.jobClass = new JobClass(npcJob, npcJobLevel);
        this.strength = npcStrength;
        this.dexterity = npcDexterity;
        this.intelligence = npcIntelligence;
        this.sight = npcSight;
        SetMaxHP();
        //this.healthBarSlider.value = CalculateHealthBarValue();
        loyalty = UnityEngine.Random.Range(40, 60);
    }

    //Constructor for random enemies
    public NpcBase(int npclevel, EJobType npcJob, int npcJobLevel, int npcStrength, int npcDexterity)
    {
        level = npclevel;
        jobClass = new JobClass(npcJob, npcJobLevel);
        strength = npcStrength;
        dexterity = npcDexterity;
        SetMaxHP();
    }

    public void SetNewName(string newName)
    {
        npcName = newName;
    }

    public void SwitchJob(EJobType newJob)
    {
        this.jobClass.SwitchJobClass(newJob);
    }

    private void SetMaxHP()
    {
        //TODO - instead of 10, depends on job type.
        switch (jobClass.jobType)
        {
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

    public void IncreaseSicknessLevel(int v)
    {
        sickLevel += v;
    }

    public void DecreaseSicknessLevel(int v)
    {
        sickLevel -= v;
    }

    public int GetSickLevel()
    {
        return sickLevel;
    }

    public void IncreaseLoyalty(int v)
    {
        loyalty += v;
    }

    public void DecreaseLoyalty(int v)
    {
        loyalty -= v;
    }

    public int GetLoyalty()
    {
        return loyalty;
    }

    public void DecreaseStats()
    {
        this.strength -= 1;
        this.dexterity -= 1;
        this.intelligence -= 1;
        this.sight -= 1;
    }

    public void AttackEnemy(NpcBase defender, Action onAttackCompleted)
    {
        if (this.AttackDamage() > defender.EvadeDamage())
        {
            //transform.LookAt(defender.transform.position);
            Vector3 slideTargetPosition = defender.transform.position;
            SlideToPosition(slideTargetPosition, () =>
            {
                state = EState.Busy;
                defender.OnDamageReceived(this.AttackDamage() - dexterity / 2);
                SlideToPosition(startPosition, () =>
                {
                    state = EState.Idle;
                    onAttackCompleted();
                });
            });
        }
        else
        {
            Vector3 slideTargetPosition = defender.transform.position;
            SlideToPosition(slideTargetPosition, () =>
            {
                state = EState.Busy;
                print(this.npcName + " Missed " + defender.npcName + "!");
                SlideToPosition(startPosition, () =>
                {
                    state = EState.Idle;
                    onAttackCompleted();
                });
            });
        }
    }

    //To use in a battle to hurt enemies
    private int AttackDamage()
    {
        return strength + jobClass.jobLevel + UnityEngine.Random.Range(0, strength);
    }

    //To use in a battle to avoid damage
    private int EvadeDamage()
    {
        return dexterity + jobClass.jobLevel;
    }

    //To use when AttackDamage received > this Evade
    public void OnDamageReceived(int damage)
    {
        this.currentHealthPoints -= damage;
    }

    public void GainExperience(int experience)
    {
        jobClass.IncreaseJobExperience(experience);
    }

    public bool GetPlayerTeam()
    {
        return bPlayerTeam;
    }

    public void SetPlayerTeam(bool isInPlayerTeam)
    {
        bPlayerTeam = isInPlayerTeam;
    }

    public void SetStartPosition(Vector3 mainPosition)
    {
        transform.position = mainPosition;
        startPosition = transform.position;
    }

    public float GetNpcMaximunHealth()
    {
        return this.maxHealthPoints;
    }

    public float GetNpcCurrentHealth()
    {
        return this.currentHealthPoints;
    }

    public void Recover()
    {
        this.currentHealthPoints = maxHealthPoints;
    }

    private float CalculateHealthBarValue()
    {
        return (this.currentHealthPoints / maxHealthPoints);
    }

    private void SlideToPosition(Vector3 position, Action onSlideComplete)
    {
        this.slideTargetPosition = position;
        this.onSlideCompleted = onSlideComplete;
        state = EState.Sliding;
    }

}
