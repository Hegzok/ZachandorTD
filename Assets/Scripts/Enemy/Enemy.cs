using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : StateMachine<Enemy>, IDamagable
{
    [SerializeField]
    private CharacterStats baseStats;
    public CharacterStats BaseStats => baseStats;

    [SerializeField] 
    protected EnemyStats enemyStats;
    public EnemyStats EnemyStats => enemyStats;

    protected EnemyMovement enemyMovement;
    protected NavMeshAgent navMeshAgent;

    protected DynamicStats stats;
    public DynamicStats Stats => stats;

    [SerializeField] 
    protected float timeToWaitIdleOnPatrol = 3f;

    protected bool aware;
    public bool Aware => aware;

    protected bool canMove;
    public bool CanMove => canMove;

    protected bool canAttack = true;
    public bool CanAttack => canAttack;

    [SerializeField] protected Transform[] patrolPoints;
    protected Transform allPatrolPointsParent;

    protected Coroutine returnToNormalSpeedCoroutine;

    // temporary variables change later
    public GameObject body;
    protected Coroutine changeBodyColourCoroutine;
    protected Color baseColor = Color.red;

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

    protected void InitEnemy()
    {
        allPatrolPointsParent = DataStorage.AllPatrolPointsParent;
        navMeshAgent = GetComponent<NavMeshAgent>();
        enemyMovement = new EnemyMovement(navMeshAgent, patrolPoints, timeToWaitIdleOnPatrol);

        patrolPoints[0].position = this.transform.position;
    }

    public void TakeDamage(float value)
    {
        stats.CurrentHealth -= value;

        Die();
    }

    #region -- Temporary functions change later --

    public void ChangeBodyColorCall(Color color, float slowDuration)
    {
        if (changeBodyColourCoroutine != null)
        {
            StopCoroutine(changeBodyColourCoroutine);
        }

       changeBodyColourCoroutine = StartCoroutine(ChangeBodyColor(color, slowDuration));
    }

    private IEnumerator ChangeBodyColor(Color color, float slowDuration)
    {
        MeshRenderer enemyBody = body.GetComponent<MeshRenderer>();

        Material[] mats = enemyBody.materials;

        Color tempColor = baseColor;

        mats[0].color = color;

        yield return new WaitForSeconds(slowDuration);

        mats[0].color = tempColor;
        Debug.Log("Changed color");
    }

    #endregion

    protected void Die()
    {
        if (stats.CurrentHealth <= 0)
        {
            EventsManager.CallOnEnemyDeath(this);
            Destroy(this.gameObject);
        }
    }

    public void CheckIfPlayerInRangeToAttack(Player player)
    {
        if (Aware)
        {
            float dist = Vector3.Distance(transform.position, player.transform.position);

            if (dist <= BaseStats.GetStat(BaseStatType.AttackRange).GetCalculatedStatValue())
            {
                ChangeState(new AttackState());
            }
            else
            {
                ChangeState(new ChaseState());
            }
        }
    }

    public void PerformAttack(Player player)
    {
        if (canAttack)
        {
            player.TakeDamage(BaseStats.GetStat(BaseStatType.AttackDamage).GetCalculatedStatValue());
            canAttack = false;
            StartCoroutine(AllowToAttackAfterCooldown());
        }

        StopMoving(BaseStats.GetStat(BaseStatType.AttackSpeed).GetCalculatedStatValue());
    }

    private IEnumerator AllowToAttackAfterCooldown()
    {
        yield return new WaitForSeconds(BaseStats.GetStat(BaseStatType.AttackSpeed).GetCalculatedStatValue());
        canAttack = true;
    }

    private void StopMoving(float time)
    {
        SlowDownMovementSpeed(1f, time);
    }

    public void SlowDownMovementSpeed(float mitigationPercent, float time)
    {
        ReturnToNormalMovementSpeed();

        float multiplier = 1f - mitigationPercent;
        stats.CurrentMovementSpeed = stats.CurrentMovementSpeed * multiplier;

        returnToNormalSpeedCoroutine = StartCoroutine(ReturnToNormalMovementSpeed(time));
    }

    protected IEnumerator ReturnToNormalMovementSpeed(float time)
    {
        yield return new WaitForSeconds(time);
        stats.CurrentMovementSpeed = BaseStats.GetStat(BaseStatType.BaseMovementSpeed).GetCalculatedStatValue();
    }

    protected void ReturnToNormalMovementSpeed()
    {
        if (returnToNormalSpeedCoroutine != null)
            StopCoroutine(returnToNormalSpeedCoroutine);

        stats.CurrentMovementSpeed = BaseStats.GetStat(BaseStatType.BaseMovementSpeed).GetCalculatedStatValue();
    }

    public void ChangeStoppingDistance(float value)
    {
        navMeshAgent.stoppingDistance = value;
    }

    public void MakeEnemyAwareOfPlayer(bool inRange)
    {
        aware = inRange;
    }

    public void FollowPlayer(Vector3 position)
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

    protected void ChangePatrolPointsParent()
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
