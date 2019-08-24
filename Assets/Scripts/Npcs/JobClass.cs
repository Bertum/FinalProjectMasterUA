using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Constants;

public class JobClass : MonoBehaviour {
    public EJobType jobType = 0;
    public int jobLevel = 0;
    private int jobExperience;

    public JobClass(EJobType jobType, int jobLevel) {
        this.jobType = jobType;
        this.jobLevel = jobLevel;
        this.jobExperience = 0;
    }

    public void IncreaseJobExperience(int experience) {
        jobExperience += experience;
        checkForLevelUp();
    }

    public void SwitchJobClass(EJobType newJob) {
        jobType = newJob;
        jobLevel = 0;
        jobExperience = 0;
    }

    //Job Level update each 20 exp + level*25 except rookie
    public void checkForLevelUp() {
        switch (jobType) {
            case EJobType.Rookie:
                if((jobExperience/20) % jobLevel > jobLevel) {
                    //TODO LevelUp
                    //Esto igual debería ir en la clase Npc?
                    //Si subes de nivel Aumentas en +1 todos tus stats menos un principal a +2
                }
                break;
            default:
                if (((jobExperience / 20) + (25 * jobLevel)) % jobLevel > jobLevel) {
                    //TODO LevelUp
                }
                break;
        }
    }
}
