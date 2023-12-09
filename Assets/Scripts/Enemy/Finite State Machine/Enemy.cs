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
  [HideInInspector] public NavMeshAgent navMeshAgent;
  [HideInInspector] public Animator animator;

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

  public void Dead() {
    Destroy(gameObject);
  }
  private void Awake() {
    animator = GetComponent<Animator>();
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

  private void OnCollisionEnter(Collision other) {
    if (currentState != retreatState)
    {
      if (other.gameObject.CompareTag("Player"))
      {
        other.gameObject.GetComponent<Player>().Dead();
      }
    }
  }


}
