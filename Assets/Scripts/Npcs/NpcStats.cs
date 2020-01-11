using static Constants;
[System.Serializable]
public class NpcStats
{
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
    public float maxHealthPoints;
    public float currentHealthPoints;
    //Sickness variable
    public int sickLevel;
    public bool bSick;
    //Loyalty variable
    public int loyalty;
    //Job variable
    public JobClass jobClass;

    public NpcStats(string npcName, int npclevel, EJobType npcJob, int npcJobLevel, int npcStrength, int npcDexterity, int npcIntelligence, int npcSight)
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
    public NpcStats(int minLevel, int maxLevel)
    {
        if (minLevel <= 0)
        {
            minLevel = 1;
        }
        level = UnityEngine.Random.Range(minLevel, maxLevel);
        npcJob = (EJobType)UnityEngine.Random.Range(0, 7);
        jobClass = new JobClass(npcJob, level);
        strength = UnityEngine.Random.Range(1, level*3);
        dexterity = UnityEngine.Random.Range(1, level * 3);
        intelligence = UnityEngine.Random.Range(1, level * 3);
        sight = UnityEngine.Random.Range(1, level * 3);
        cost = 50 * level;
        SetMaxHP();
    }

    public void SetMaxHP()
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
            case EJobType.Medic:
                maxHealthPoints = 5 + strength + jobClass.jobLevel;
                break;
            case EJobType.Carpenter:
                maxHealthPoints = 10 + strength + jobClass.jobLevel;
                break;
            default:
                break;
        }
        currentHealthPoints = maxHealthPoints;
    }


    public void SetNewName(string newName)
    {
        npcName = newName;
    }

    public void SwitchJob(EJobType newJob)
    {
        jobClass.SwitchJobClass(newJob);
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
        strength -= 1;
        dexterity -= 1;
        intelligence -= 1;
        sight -= 1;
    }

    public float GetNpcMaximunHealth()
    {
        return maxHealthPoints;
    }

    public float GetNpcCurrentHealth()
    {
        return currentHealthPoints;
    }

    public void Recover()
    {
        currentHealthPoints = maxHealthPoints;
    }

    //To use when AttackDamage received > this Evade
    public void OnDamageReceived(int damage)
    {
        currentHealthPoints -= damage;
    }

    public void GainExperience(int experience)
    {
        jobClass.IncreaseJobExperience(experience);
    }
}
