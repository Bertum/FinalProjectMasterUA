using System;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]

public class NpcBase : MonoBehaviour
{

    enum EState
    {
        Idle,
        Busy,
        Sliding
    }

    public NpcStats stats;
    //Team definition
    private bool bPlayerTeam;
    //Combat variables
    private Vector3 startPosition;
    private Vector3 slideTargetPosition;
    private EState state;
    private Action onSlideCompleted;
    private Action onAttackCompleted;
    //Health Slider
    public Slider healthBarSlider;


    private void Start()
    {
        this.stats.jobClass = new JobClass(stats.npcJob, stats.jobLevel);
        stats.SetMaxHP();
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

    public void AttackEnemy(NpcBase defender, Action onAttackCompleted)
    {
        if (this.AttackDamage() > defender.EvadeDamage())
        {
            //transform.LookAt(defender.transform.position);
            Vector3 slideTargetPosition = defender.transform.position;
            SlideToPosition(slideTargetPosition, () =>
            {
                state = EState.Busy;
                defender.stats.OnDamageReceived(this.AttackDamage() - stats.dexterity / 2);
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
                print(this.stats.npcName + " Missed " + defender.stats.npcName + "!");
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
        return stats.strength + stats.jobClass.jobLevel + UnityEngine.Random.Range(0, stats.strength);
    }

    //To use in a battle to avoid damage
    private int EvadeDamage()
    {
        return stats.dexterity + stats.jobClass.jobLevel;
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


    private float CalculateHealthBarValue()
    {
        return (this.stats.currentHealthPoints / stats.maxHealthPoints);
    }

    private void SlideToPosition(Vector3 position, Action onSlideComplete)
    {
        this.slideTargetPosition = position;
        this.onSlideCompleted = onSlideComplete;
        state = EState.Sliding;
    }

}
