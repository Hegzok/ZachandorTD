using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, IDamagable
{
    private PlayerInputActions playerInputActions;
    private Inventory inventory;

    private Calculator calculator;
    public Calculator Calculator => calculator;

    private CharacterStats baseStats;
    public CharacterStats BaseStats => baseStats;

    [SerializeField]
    private List<PlayerStatObject> playerBaseStats;
    public List<PlayerStatObject> PlayerBaseStats => playerBaseStats;

    private DynamicStats dynamicStats;
    public DynamicStats DynamicStats => dynamicStats;

    private PlayerLevel playerLevel;

    [SerializeField]
    private Ability currentBasicAttack;

    // Temporary variables change later
    public GameObject playerSkin;
    public Color color;
    public Color baseColor;
    public Coroutine ChangeColorCoroutine;



    private void Awake()
    {
        InitPlayer();
    }

    // Start is called before the first frame update
    void Start()
    {
        EventsManager.OnEnemyDeath += playerLevel.EnemyToExperience;
        EventsManager.OnBasicAttackPickUp += GrabCurrentBasicAttack;
    }

    private void OnEnable()
    {
        playerInputActions.Enable();

        playerInputActions.Land.Shoot.performed += x => UseAbility();
        playerInputActions.Land.RightClick.performed += x => playerLevel.CalculateStat(613.36f, 106f, 2f);
    }

    private void OnDisable()
    {
        playerInputActions.Disable();

        playerInputActions.Land.Shoot.performed -= x => UseAbility();
        playerInputActions.Land.RightClick.performed -= x => playerLevel.CalculateStat(613.36f, 106f, 2f);

        EventsManager.OnEnemyDeath -= playerLevel.EnemyToExperience;
        EventsManager.OnBasicAttackPickUp -= GrabCurrentBasicAttack;
    }

    // Update is called once per frame
    void Update()
    {
        LookForEnemy();
    }

    private void UseAbility()
    {
        currentBasicAttack.Use();
    }

    private void GrabCurrentBasicAttack(Ability basicAttack)
    {
        currentBasicAttack = basicAttack;
    }

    private void InitPlayer()
    {
        playerInputActions = new PlayerInputActions();
        calculator = new Calculator();
        inventory = GetComponent<Inventory>();
        currentBasicAttack = inventory.CurrentBasicAttack;

        playerLevel = new PlayerLevel();
        baseStats = new CharacterStats(playerBaseStats);

        dynamicStats.CurrentHealth = BaseStats.GetStatFinalValue(BaseStatType.MaxHealth);
        dynamicStats.CurrentMovementSpeed = BaseStats.GetStatFinalValue(BaseStatType.BaseMovementSpeed);
        dynamicStats.CurrentMovementSpeedBackwards = BaseStats.GetStatFinalValue(BaseStatType.BaseMovementSpeed) * 0.7f;
    }

    public void TakeDamage(int value)
    {
        // temporary change later
        if (ChangeColorCoroutine != null)
            StopCoroutine(ChangeColorCoroutine);

        dynamicStats.CurrentHealth -= value;

        // temporary change later
        ChangeColorCoroutine = StartCoroutine(ChangeSkinColor(color));

        Die();
    }

    // Temporary function change later
    private IEnumerator ChangeSkinColor(Color color)
    {
        SkinnedMeshRenderer skin = playerSkin.GetComponent<SkinnedMeshRenderer>();

        Material[] mats = skin.materials;

        mats[0].color = color;

        yield return new WaitForSeconds(0.2f);

        mats[0].color = baseColor;
    }

    public void Heal(int value)
    {
        if (dynamicStats.CurrentHealth <= BaseStats.GetStatFinalValue(BaseStatType.MaxHealth))
        {
            dynamicStats.CurrentHealth += value;
        }
        if (dynamicStats.CurrentHealth > BaseStats.GetStatFinalValue(BaseStatType.MaxHealth))
        {
            dynamicStats.CurrentHealth = BaseStats.GetStatFinalValue(BaseStatType.MaxHealth);
        }
    }

    public void SlowDownMovementSpeed(float mitigationPercent, float time)
    {
        float multiplier = 1f - mitigationPercent;
        dynamicStats.CurrentMovementSpeed = dynamicStats.CurrentMovementSpeed * multiplier;

        StartCoroutine(ReturnToNormalMovementSpeed(time));
    }

    private IEnumerator ReturnToNormalMovementSpeed(float time)
    {
        yield return new WaitForSeconds(time);
        dynamicStats.CurrentMovementSpeed = BaseStats.GetStatFinalValue(BaseStatType.BaseMovementSpeed);
    }

    private void Die()
    {
        if (dynamicStats.CurrentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void LookForEnemy()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, BaseStats.GetStatFinalValue(BaseStatType.VisibilityRadius));

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.GetComponent<Enemy>())
            {
                if (!hitCollider.GetComponent<Enemy>().Aware)
                {
                    Enemy enemy = hitCollider.GetComponent<Enemy>();
                    enemy.ChangeState(new ChaseState());
                }
            }
        }
    }

    public float ReturnFinalPhysicalDamage(IDamagable enemy)
    {
        float finalValue = 0f;

        float playerDamage = calculator.CalculatePlayerPhysicalDamage(inventory.CurrentBasicAttack,
        BaseStats.GetStatFinalValue(BaseStatType.AttackDamage),
        BaseStats.GetStatFinalValue(BaseStatType.CriticalStrikeChance),
        BaseStats.GetStatFinalValue(BaseStatType.CriticalStrikeDamage));

        finalValue = calculator.CalculateDamageNegated(playerDamage, enemy.BaseStats.GetStatFinalValue(BaseStatType.Armor));

        return finalValue;
    }

    public float ReturnFinalMagicalDamage(IDamagable enemy)
    {
        float finalValue = 0f;

        float playerDamage = calculator.CalculatePlayerMagicalDamage(inventory.CurrentSpell,
        BaseStats.GetStatFinalValue(BaseStatType.SpellDamage),
        BaseStats.GetStatFinalValue(BaseStatType.CriticalStrikeChance),
        BaseStats.GetStatFinalValue(BaseStatType.CriticalStrikeDamage));

        finalValue = calculator.CalculateDamageNegated(playerDamage, enemy.BaseStats.GetStatFinalValue(BaseStatType.MagicResist));

        return finalValue;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Use the same vars you use to draw your Overlap SPhere to draw your Wire Sphere.
        if (BaseStats != null)
        {
            Gizmos.DrawWireSphere(transform.position, BaseStats.GetStatFinalValue(BaseStatType.VisibilityRadius));
        }

    }
}
