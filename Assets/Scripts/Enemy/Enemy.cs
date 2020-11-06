using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : StateMachine<Enemy>, IDamagable
{
    [SerializeField] private EnemyStats enemyStats;
    private EnemyMovement enemyMovement;
    private NavMeshAgent navMeshAgent;

    private Stats stats;
    public Stats Stats => stats;

    [SerializeField] private float timeToWaitIdleOnPatrol = 3f;
    private bool aware;

    [SerializeField] private Transform[] patrolPoints;
    private Transform allPatrolPointsParent;

    // Start is called before the first frame update
    void Start()
    {
        InitEnemy();
        ChangePatrolPointsParent();
        ChangeState(new PatrolState());
    }

    // Update is called once per frame
    void Update()
    {
        UpdateState();
    }

    private void InitEnemy()
    {
        allPatrolPointsParent = DataStorage.AllPatrolPointsParent;
        navMeshAgent = GetComponent<NavMeshAgent>();
        enemyMovement = new EnemyMovement(navMeshAgent, patrolPoints, timeToWaitIdleOnPatrol);

        patrolPoints[0].position = this.transform.position;
        stats.CurrentHealth = enemyStats.MaxHealth;
        stats.Damage = enemyStats.Damage;
        stats.MovementSpeed = enemyStats.MovementSpeed;
        stats.MaxHealth = enemyStats.MaxHealth;
    }

    public void TakeDamage(int value)
    {
        stats.CurrentHealth -= value;

        Die();
    }

    private void Die()
    {
        if (stats.CurrentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }


    public void MakeEnemyAwareOfPlayer(bool inRange)
    {
        aware = inRange;
    }

    public void FollowPlayerWhenInRange(Vector3 position)
    {
        if (aware)
        {
            enemyMovement.MoveTo(position);
        }
    }

    public void ReturnToBasePosition(Vector3 position)
    {
        if (!aware)
        {
            enemyMovement.MoveTo(position);
        }
    }

    public void StartPatrolling()
    {
        enemyMovement.Patrol(this.gameObject.transform.position);
    }

    private void ChangePatrolPointsParent()
    {
        foreach (var patrolPoint in patrolPoints)
        {
            /* Sets a different parent than enemy so patrol points doesnt move with enemy and we can organize 
            hierarchy better cuz points sits in enemy object while in editor. */
            patrolPoint.SetParent(allPatrolPointsParent); 
        }
    }

    #region -- StateMachine methods implementation --

    public override void ChangeState(State<Enemy> newState)
    {
        currentState?.DeinitState(this);
        currentState = newState;
        currentState?.InitState(this);
    }

    public override void UpdateState()
    {
        currentState?.UpdateState(this);
    }

    #endregion


}
