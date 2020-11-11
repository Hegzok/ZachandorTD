using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamagable
{
    private CharacterStats baseStats;
    public CharacterStats BaseStats => baseStats;

    [SerializeField]
    private List<PlayerStatObject> playerBaseStats;
    public List<PlayerStatObject> PlayerBaseStats => playerBaseStats;

    private DynamicStats dynamicStats;
    public DynamicStats DynamicStats => dynamicStats;

    private Level playerLevel;

    [SerializeField] 
    private Ability activeAbility;

    // Temporary variables change later
    public GameObject playerSkin;
    public Color color;

    private void Awake()
    {
        InitPlayer();
    }

    // Start is called before the first frame update
    void Start()
    {
        EventsManager.OnAbilityChosen += GetActiveAbility;
        EventsManager.OnEnemyDeath += playerLevel.EnemyToExperience;
    }

    private void OnDisable()
    {
        EventsManager.OnAbilityChosen -= GetActiveAbility;
        EventsManager.OnEnemyDeath -= playerLevel.EnemyToExperience;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            UseAbility();
        }

        LookForEnemy();

        // temporary change later
        if (Input.GetMouseButtonDown(1))
        {
            playerLevel.CalculateStat(613.36f, 106f, 2f); 
        }
    }

    private void UseAbility()
    {
        activeAbility.Use();
    }

    private void GetActiveAbility(Ability ability)
    {
        activeAbility = ability;
    }

    private void InitPlayer()
    {
        playerLevel = new Level();
        baseStats = new CharacterStats(playerBaseStats);

        dynamicStats.CurrentHealth = BaseStats.GetStatFinalValue(BaseStatType.MaxHealth);
        dynamicStats.CurrentMovementSpeed = BaseStats.GetStatFinalValue(BaseStatType.BaseMovementSpeed);
        dynamicStats.CurrentMovementSpeedBackwards = BaseStats.GetStatFinalValue(BaseStatType.BaseMovementSpeed) * 0.7f;
    }

    public void TakeDamage(float value)
    {
        dynamicStats.CurrentHealth -= value;
        StartCoroutine(ChangeSkinColor(color));

        Die();
    }

    // Temporary function change later
    private IEnumerator ChangeSkinColor(Color color)
    {
        SkinnedMeshRenderer skin = playerSkin.GetComponent<SkinnedMeshRenderer>();

        Material[] mats = skin.materials;

        Color tempColor = mats[0].color;

        mats[0].color = color;

        yield return new WaitForSeconds(0.2f);

        mats[0].color = tempColor;  
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Use the same vars you use to draw your Overlap SPhere to draw your Wire Sphere.
        Gizmos.DrawWireSphere(transform.position, BaseStats.GetStatFinalValue(BaseStatType.VisibilityRadius));
    }
}
