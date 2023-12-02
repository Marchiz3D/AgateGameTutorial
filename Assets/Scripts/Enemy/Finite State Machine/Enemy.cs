using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
  [SerializeField] public List<Transform> Waypoints = new List<Transform>();
  public float chaseDistance;
  public Player player;
  private BaseState currentState;

  public PatrolState patrolState = new PatrolState();
  public ChaseState chaseState = new ChaseState();
  public RetreatState retreatState = new RetreatState();
  [HideInInspector]public NavMeshAgent navMeshAgent;


  public void SwitchState(BaseState state) {
    currentState.ExitState(this);
    currentState = state;
    currentState.EnterState(this);
  }

  private void StartRetreating()
  {
    SwitchState(retreatState);
  }
  private void StopRetreating()
  {
    SwitchState(patrolState);
  }
  private void Awake() {
    currentState = patrolState;
    currentState.EnterState(this);
    navMeshAgent = GetComponent<NavMeshAgent>();
  }

  private void Start() {
    if (player != null)
    {
      player.OnPowerUpStart += StartRetreating;
      player.OnPowerUpStop += StopRetreating;
    }
  }
  private void Update() {
    currentState.UpdateState(this);
  }


}
