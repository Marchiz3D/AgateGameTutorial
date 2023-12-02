using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : BaseState
{
    private bool isMoving;
    private Vector3 destination;
    public void EnterState(Enemy enemy) {
        isMoving = false;
    }

    public void UpdateState (Enemy enemy) {
        if (Vector3.Distance(enemy.transform.position, enemy.player.transform.position) < enemy.chaseDistance)
        {
            enemy.SwitchState(enemy.chaseState);    
        }

        if (!isMoving)
        {
            isMoving = true;
            int index = UnityEngine.Random.Range(0, enemy.Waypoints.Count); //Mengambil index dari list Waypoints
            destination = enemy.Waypoints[index].position;
            enemy.navMeshAgent.destination = destination;
        }
        else if (Vector3.Distance(enemy.transform.position, destination) < 0.1f)
        {
            isMoving = false;
        }
    }

    public void ExitState (Enemy enemy) {
        Debug.Log("Stop Patrol");
    }
}
