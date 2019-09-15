using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Constants;

public class BattleScript : MonoBehaviour {

    public enum EBattleStatus {
        PreparingTurn,
        ActiveTurn,
        NpcAttacking,
        EndTurn
    }

    public List<NpcBase> goodBois = new List<NpcBase>();
    public List<NpcBase> baddies = new List<NpcBase>();
    public int turn;
    private List<NpcBase> orderList = new List<NpcBase>();
    private List<NpcBase> playerTeam = new List<NpcBase>();
    private List<NpcBase> enemyTeam = new List<NpcBase>();
    private List<NpcBase> playerTeamTurn = new List<NpcBase>();
    private List<NpcBase> enemyTeamTurn = new List<NpcBase>();
    private EBattleStatus battleStatus;
    private int combatant;

    // Start is called before the first frame update
    void Start() {
        /*for (int i = 0; i < 5; i++) {
            goodBois.Add(new NpcBase(UnityEngine.Random.Range(0, 3), EJobType.Rookie, UnityEngine.Random.Range(0, 5), UnityEngine.Random.Range(0, 6), UnityEngine.Random.Range(0, 6)));
            goodBois[i].SetPlayerTeam(true);
            baddies.Add(new NpcBase(UnityEngine.Random.Range(0, 3), EJobType.Rookie, UnityEngine.Random.Range(0, 5), UnityEngine.Random.Range(0, 6), UnityEngine.Random.Range(0, 6)));
            baddies[i].SetPlayerTeam(false);
        }*/
        foreach (NpcBase npc in goodBois) {
            npc.SetPlayerTeam(true);
        }
        turn = 0;
        PrepareBattle();
    }

    // Update is called once per frame
    void Update() {
        switch (BattleHasEnded()) {
            case EEndBattleStatus.Ongoing:
                switch (battleStatus) {
                    case EBattleStatus.PreparingTurn:
                        PrepareTurn();
                        break;
                    case EBattleStatus.ActiveTurn:
                        DoBattle(playerTeamTurn, enemyTeamTurn);
                        break;
                    case EBattleStatus.NpcAttacking:
                        break;
                    case EBattleStatus.EndTurn:
                        PrintTurn();
                        print("End of Turn: " + turn);
                        turn++;
                        battleStatus = EBattleStatus.PreparingTurn;
                        break;
                }
                break;
            case EEndBattleStatus.PlayerWon:
                print("Battle has ended!, the player Won, do something!");
                break;
            case EEndBattleStatus.PlayerLost:
                print("Battle has ended!, the player Lost, do something!");
                break;
            default:
                break;
        }
    }

    private void PrepareBattle() {
        orderList.AddRange(goodBois);
        orderList.AddRange(baddies);
        orderList.Sort(SetBattleOrder);
        //PrintOrderList();

        foreach (NpcBase npc in orderList) {
            if (npc.GetPlayerTeam()) {
                playerTeam.Add(npc);
            } else {
                enemyTeam.Add(npc);
            }
        }

        battleStatus = EBattleStatus.PreparingTurn;
    }

    private void PrepareTurn() {
        //Limpiamos lista de npcs del turno anterior
        playerTeamTurn.Clear();
        enemyTeamTurn.Clear();
        //Solo lucharan aquellos que sigan vivos dentro del equipo
        for (int i = 0; i < playerTeam.Count; i++) {
            if (playerTeam[i].GetNpcCurrentHealth() > 0) {
                playerTeamTurn.Add(playerTeam[i]);
            }
        }
        for (int i = 0; i < enemyTeam.Count; i++) {
            if (enemyTeam[i].GetNpcCurrentHealth() > 0) {
                enemyTeamTurn.Add(enemyTeam[i]);
            }
        }
        battleStatus = EBattleStatus.ActiveTurn;
    }

    private void DoBattle(List<NpcBase> playerList, List<NpcBase> enemyList) {
        NpcBase defender;
        NpcBase fighter = orderList[combatant];
        if (fighter.GetNpcCurrentHealth() > 0) {
            battleStatus = EBattleStatus.NpcAttacking;
            if (fighter.GetPlayerTeam()) {
                int position = UnityEngine.Random.Range(0, enemyList.Count);
                defender = enemyList[position];
                fighter.AttackEnemy(defender, () => {
                    if(defender.GetNpcCurrentHealth() <= 0) {
                        enemyList.Remove(defender);
                    }
                    NextCombatant();
                });
                print(fighter.npcName + " has attacked " + defender.npcName);
            } else {
                int position = UnityEngine.Random.Range(0, playerList.Count);
                defender = playerList[position];
                fighter.AttackEnemy(defender, () => {
                    if (defender.GetNpcCurrentHealth() <= 0) {
                        playerList.Remove(defender);
                    }
                    NextCombatant();
                });
                print(fighter.npcName + " has attacked " + defender.npcName);
            }
        } else {
            NextCombatant();
        }
    }

    private void NextCombatant() {
        battleStatus = EBattleStatus.ActiveTurn;
        if (combatant < orderList.Count - 1) {
            combatant++;
        } else {
            combatant = 0;
            battleStatus = EBattleStatus.EndTurn;
        }
    }

    private static int SetBattleOrder(NpcBase APirate, NpcBase BPirate) {
        if (APirate == null) {
            if (BPirate == null) {
                //If both null equals
                return 0;
            } else {
                //if APirate null then smaller
                return -1;
            }
        } else {
            if (BPirate == null) {
                //if BPirate null then A bigger
                return 1;
            } else {
                //else compare level+dexterity
                int aOrder = APirate.level + APirate.dexterity;
                int bOrder = BPirate.level + BPirate.dexterity;
                if (bOrder.CompareTo(aOrder) == 0) {
                    if (BPirate.level > APirate.level) {
                        return 1;
                    } else {
                        return -1;
                    }
                }
                return bOrder.CompareTo(aOrder);
            }
        }
    }


    private EEndBattleStatus BattleHasEnded() {
        bool playerTeamTotalLife = false;
        bool enemyTeamTotalLife = false;
        foreach (NpcBase npc in playerTeam) {
            if (npc.GetNpcCurrentHealth() > 0) {
                playerTeamTotalLife = true;
            }
        }
        //If none are alive, battle has ended
        if (!playerTeamTotalLife) {
            return EEndBattleStatus.PlayerLost;
        } else {
            //If 1 player was alive, we look the enemy
            foreach (NpcBase npc in enemyTeam) {
                if (npc.GetNpcCurrentHealth() > 0) {
                    enemyTeamTotalLife = true;
                }
            }
            //if none are alive, battle has ended
            if (!enemyTeamTotalLife) {
                return EEndBattleStatus.PlayerWon;
            }
        }
        //If both teams have at least 1 alive, keep going
        return EEndBattleStatus.Ongoing;
    }

    /*
    private bool BattleHasEnded() {
        if (playerTeam.Count == 0) {
            return true;
        } else {
            if (enemyTeam.Count == 0) {
                return true;
            }
        }
        return false;
    }
    */

    private void PrintOrderList() {
        for (int i = 0; i < orderList.Count; i++) {
            int order = orderList[i].level + orderList[i].dexterity;
            print("Order is " + i + ", total is: " + order + " and level is: " + orderList[i].level);
        }
    }

    private void PrintOrderWithDamage() {
        for (int i = 0; i < orderList.Count; i++) {
            print("For NPC " + i + " max health is: " + orderList[i].getNpcMaximunHealth() + " and currently is " + orderList[i].GetNpcCurrentHealth());
        }
    }

    private void PrintEndOfBattle() {
        for (int i = 0; i < playerTeam.Count; i++) {
            print("Player " + i + " max health is: " + playerTeam[i].getNpcMaximunHealth() + " and currently is " + playerTeam[i].GetNpcCurrentHealth());
        }
        for (int i = 0; i < enemyTeam.Count; i++) {
            print("Enemy " + i + " max health is: " + enemyTeam[i].getNpcMaximunHealth() + " and currently is " + enemyTeam[i].GetNpcCurrentHealth());
        }
    }

    private void PrintTurn() {
        for (int i = 0; i < playerTeam.Count; i++) {
            print("Turn " + turn + " for Player " + i + " max health is: " + playerTeam[i].getNpcMaximunHealth() + " and currently is " + playerTeam[i].GetNpcCurrentHealth());
        }
        for (int i = 0; i < enemyTeam.Count; i++) {
            print("Turn " + turn + " for Enemy " + i + " max health is: " + enemyTeam[i].getNpcMaximunHealth() + " and currently is " + enemyTeam[i].GetNpcCurrentHealth());
        }
    }

}
