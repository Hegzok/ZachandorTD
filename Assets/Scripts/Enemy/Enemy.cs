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
    public bool Aware => aware;

    [SerializeField] private Transform[] patrolPoints;
    private Transform allPatrolPointsParent;

    private Coroutine returnToNormalSpeedCoroutine;

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
        navMeshAgent.speed = stats.CurrentMovementSpeed;
    }

    private void InitEnemy()
    {
        allPatrolPointsParent = DataStorage.AllPatrolPointsParent;
        navMeshAgent = GetComponent<NavMeshAgent>();
        enemyMovement = new EnemyMovement(navMeshAgent, patrolPoints, timeToWaitIdleOnPatrol);

        stats.CurrentHealth = enemyStats.MaxHealth;
        stats.Damage = enemyStats.Damage;
        stats.BaseMovementSpeed = enemyStats.CurrentMovementSpeed;
        stats.CurrentMovementSpeed = enemyStats.CurrentMovementSpeed;
        stats.MaxHealth = enemyStats.MaxHealth;

        patrolPoints[0].position = this.transform.position;
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

    public void SlowDownMovementSpeed(float mitigationPercent, float time)
    {
        ReturnToNormalMovementSpeed();

        float multiplier = 1f - mitigationPercent;
        stats.CurrentMovementSpeed = stats.CurrentMovementSpeed * multiplier;

        returnToNormalSpeedCoroutine = StartCoroutine(ReturnToNormalMovementSpeed(time));
    }

    private IEnumerator ReturnToNormalMovementSpeed(float time)
    {
        yield return new WaitForSeconds(time);
        stats.CurrentMovementSpeed = stats.BaseMovementSpeed;
    }

    private void ReturnToNormalMovementSpeed()
    {
        if (returnToNormalSpeedCoroutine != null)
            StopCoroutine(returnToNormalSpeedCoroutine);

        stats.CurrentMovementSpeed = stats.BaseMovementSpeed;
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
