using System;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]

public class NpcBase : MonoBehaviour {

    enum EState {
        Idle,
        Busy,
        Sliding,
        Attacking
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
    private Action onAttackAnimCompleted;
    //Health Slider
    public Slider healthBarSlider;
    //Sickness variable
    private int sickLevel;
    private bool bSick;
    //Loyalty variable
    private int loyalty;
    //Animator
    private Animator animator;
    private float atAttackTime;
    private float atAttack;

    private void Start() {
        animator = gameObject.GetComponent<Animator>();
        this.stats.jobClass = new JobClass(stats.npcJob, stats.jobLevel);
        stats.SetMaxHP();
        state = EState.Idle;
        atAttackTime = 2f;
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
                if (Vector3.Distance(transform.position, slideTargetPosition) < 1f) {
                    transform.position = slideTargetPosition;
                    onSlideCompleted();
                }
                break;
            case EState.Attacking:
                if (GetAnimator().GetCurrentAnimatorStateInfo(0).IsName("Attack") && Time.time > atAttack) {
                    onAttackAnimCompleted();
                }
                break;
        }
        this.healthBarSlider.value = CalculateHealthBarValue();
    }

    public void AttackEnemy(NpcBase defender, Action onAttackCompleted) {
        this.GetAnimator().SetBool("isWalking", true);
        if (this.AttackDamage() > defender.EvadeDamage()) {
            //transform.LookAt(defender.transform.position);
            Vector3 slideTargetPosition = defender.transform.position;
            SlideToPosition(slideTargetPosition, () => {
                state = EState.Busy;                
                defender.OnDamageReceived(this.AttackDamage() - stats.dexterity / 2);
                AnimAttack(() => {
                    this.GetAnimator().SetBool("isAttacking", false);
                    SlideToPosition(startPosition, () => {
                        state = EState.Idle;
                        this.GetAnimator().SetBool("isWalking", false);
                        onAttackCompleted();
                    });
                });                    
            });
        } else {
            Vector3 slideTargetPosition = defender.transform.position;
            SlideToPosition(slideTargetPosition, () => {
                state = EState.Busy;
                defender.GetAnimator().SetBool("Duck", true);
                print(this.stats.npcName + " Missed " + defender.stats.npcName + "!");
                AnimAttack(() => {
                    this.GetAnimator().SetBool("isAttacking", false);
                    SlideToPosition(startPosition, () => {
                        state = EState.Idle;
                        defender.GetAnimator().SetBool("Duck", false);
                        this.GetAnimator().SetBool("isWalking", false);
                        onAttackCompleted();
                    });
                });
            });
        }
    }

    //To use in a battle to hurt enemies
    private int AttackDamage() {
        return stats.strength + stats.jobClass.jobLevel + UnityEngine.Random.Range(0, stats.strength);
    }

    //To use in a battle to avoid damage
    private int EvadeDamage() {
        return stats.dexterity + stats.jobClass.jobLevel;
    }

    //To use when AttackDamage received > this Evade
    public void OnDamageReceived(int damage) {
        this.stats.currentHealthPoints -= damage;
        if (this.stats.currentHealthPoints <= 0) {
            this.GetAnimator().SetBool("isDead", true);
        }
    }

    public void GainExperience(int experience) {
        stats.jobClass.IncreaseJobExperience(experience);
    }

    public bool GetPlayerTeam() {
        return bPlayerTeam;
    }

    public void SetPlayerTeam(bool isInPlayerTeam) {
        bPlayerTeam = isInPlayerTeam;
    }

    public void SetStartPosition(Vector3 mainPosition) {
        transform.position = mainPosition;
        startPosition = transform.position;
    }

    public float GetNpcMaximunHealth() {
        return this.stats.maxHealthPoints;
    }

    public float GetNpcCurrentHealth() {
        return this.stats.currentHealthPoints;
    }

    public void Recover() {
        this.stats.currentHealthPoints = stats.maxHealthPoints;
    }

    private float CalculateHealthBarValue() {
        return (this.stats.currentHealthPoints / stats.maxHealthPoints);
    }

    private void SlideToPosition(Vector3 position, Action onSlideComplete) {
        this.GetAnimator().SetBool("isWalking", true);
        this.slideTargetPosition = position;
        this.onSlideCompleted = onSlideComplete;
        state = EState.Sliding;
    }

    private void AnimAttack(Action onAnimCompleted) {
        this.GetAnimator().SetBool("isWalking", false);
        this.GetAnimator().SetBool("isAttacking", true);
        this.onAttackAnimCompleted = onAnimCompleted;
        atAttack = Time.time + atAttackTime;
        state = EState.Attacking;
    }

    public Animator GetAnimator() {
        return this.animator;
    }

}
