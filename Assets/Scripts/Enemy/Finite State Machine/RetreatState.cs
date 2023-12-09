using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class RetreatState : BaseState
{
    public void EnterState(Enemy enemy) {
        Debug.Log("Start Retreat");
        enemy.animator.SetTrigger("RetreatState");
    }

    public void UpdateState (Enemy enemy) {
        if (enemy.player != null)
        {
            enemy.navMeshAgent.destination = enemy.transform.position - enemy.player.transform.position;
        }
    }

    public void ExitState (Enemy enemy) {
        Debug.Log("Stop Retreat");
    }
}
