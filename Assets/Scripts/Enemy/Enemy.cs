using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : StateMachine<Enemy>, IDamagable
{
    private CharacterStats baseStats;
    public CharacterStats BaseStats => baseStats;

    [SerializeField]
    private List<EnemyStatObject> enemyBaseStats;
    public List<EnemyStatObject> EnemyBaseStats => enemyBaseStats;

    protected DynamicStats dynamicStats;
    public DynamicStats DynamicStats => dynamicStats;

    protected EnemyMovement enemyMovement;
    protected NavMeshAgent navMeshAgent;
    protected Calculator calculator;

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
        navMeshAgent.speed = dynamicStats.CurrentMovementSpeed;
    }

    protected void InitEnemy()
    {
        baseStats = new CharacterStats(enemyBaseStats);

        dynamicStats.CurrentHealth = BaseStats.GetStatFinalValue(BaseStatType.MaxHealth);
        dynamicStats.CurrentMovementSpeed = BaseStats.GetStatFinalValue(BaseStatType.BaseMovementSpeed);

        allPatrolPointsParent = DataStorage.AllPatrolPointsParent;
        navMeshAgent = GetComponent<NavMeshAgent>();
        enemyMovement = new EnemyMovement(navMeshAgent, patrolPoints, timeToWaitIdleOnPatrol);
        calculator = new Calculator();

        patrolPoints[0].position = this.transform.position;
    }

    public void TakeDamage(int value)
    {
        dynamicStats.CurrentHealth -= value;

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
        if (dynamicStats.CurrentHealth <= 0)
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

            if (dist <= BaseStats.GetStatFinalValue(BaseStatType.AttackRange))
            {
                ChangeState(new AttackState());
            }
            else
            {
                ChangeState(new ChaseState());
            }
        }
    }

    public void PerformPhysicalAttack(Player player)
    {
        if (canAttack)
        {
            float CalculatedDamage = calculator.CalculateEnemyPhysicalDamage(BaseStats.GetStatFinalValue(BaseStatType.AttackDamage));
            int FinalDamageNegated = Mathf.RoundToInt(calculator.CalculateDamageNegated(CalculatedDamage, player.BaseStats.GetStatFinalValue(BaseStatType.Armor)));
             
            player.TakeDamage(FinalDamageNegated);

            canAttack = false;
            StartCoroutine(AllowToAttackAfterCooldown());
        }

        StopMoving(BaseStats.GetStatFinalValue(BaseStatType.AttackSpeed));
    }

    public void PerformMagicalAttack(Player player)
    {
        if (canAttack)
        {
            float CalculatedDamage = calculator.CalculateEnemyMagicalDamage(BaseStats.GetStatFinalValue(BaseStatType.SpellDamage));
            int FinalDamageNegated = Mathf.RoundToInt(calculator.CalculateDamageNegated(CalculatedDamage, player.BaseStats.GetStatFinalValue(BaseStatType.MagicResist)));

            player.TakeDamage(FinalDamageNegated);
            Debug.Log($"FinalDamage is {FinalDamageNegated}");

            canAttack = false;
            StartCoroutine(AllowToAttackAfterCooldown());
        }

        StopMoving(BaseStats.GetStatFinalValue(BaseStatType.AttackSpeed));
    }

    private IEnumerator AllowToAttackAfterCooldown()
    {
        yield return new WaitForSeconds(BaseStats.GetStatFinalValue(BaseStatType.AttackSpeed));
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
        dynamicStats.CurrentMovementSpeed = dynamicStats.CurrentMovementSpeed * multiplier;

        returnToNormalSpeedCoroutine = StartCoroutine(ReturnToNormalMovementSpeed(time));
    }

    protected IEnumerator ReturnToNormalMovementSpeed(float time)
    {
        yield return new WaitForSeconds(time);
        dynamicStats.CurrentMovementSpeed = BaseStats.GetStatFinalValue(BaseStatType.BaseMovementSpeed);
    }

    protected void ReturnToNormalMovementSpeed()
    {
        if (returnToNormalSpeedCoroutine != null)
            StopCoroutine(returnToNormalSpeedCoroutine);

        dynamicStats.CurrentMovementSpeed = BaseStats.GetStatFinalValue(BaseStatType.BaseMovementSpeed);
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
