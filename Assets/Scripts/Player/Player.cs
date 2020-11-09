using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamagable
{
    [SerializeField] private Ability activeAbility;

    [SerializeField] private PlayerStats playerStats;

    [SerializeField] private Stats stats;
    public Stats Stats => stats;

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
    }

    private void OnDisable()
    {
        EventsManager.OnAbilityChosen -= GetActiveAbility;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            UseAbility();
        }

        LookForEnemy();
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
        stats.CurrentHealth = playerStats.MaxHealth;
        stats.BaseMovementSpeed = playerStats.CurrentMovementSpeed;
        stats.CurrentMovementSpeed = playerStats.CurrentMovementSpeed;
        stats.CurrentMovementSpeedBackwards = playerStats.MovementSpeedBackwards;
        stats.MaxHealth = playerStats.MaxHealth;
        stats.VisibilityRadius = playerStats.VisibilityRadius;
    }

    public void TakeDamage(int value)
    {
        stats.CurrentHealth -= value;
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
        if (stats.CurrentHealth <= stats.MaxHealth)
        {
            stats.CurrentHealth += value;
        }
        if (stats.CurrentHealth > stats.MaxHealth)
        {
            stats.CurrentHealth = stats.MaxHealth;
        }
    }

    public void SlowDownMovementSpeed(float mitigationPercent, float time)
    {
        float multiplier = 1f - mitigationPercent;
        stats.CurrentMovementSpeed = stats.CurrentMovementSpeed * multiplier;

        StartCoroutine(ReturnToNormalMovementSpeed(time));
    }

    private IEnumerator ReturnToNormalMovementSpeed(float time)
    {
        yield return new WaitForSeconds(time);
        stats.CurrentMovementSpeed = stats.BaseMovementSpeed;
    }

    private void Die()
    {
        if (stats.CurrentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void LookForEnemy()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, stats.VisibilityRadius);

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
        Gizmos.DrawWireSphere(transform.position, stats.VisibilityRadius);
    }
}
