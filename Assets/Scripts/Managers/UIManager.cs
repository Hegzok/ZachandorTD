using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private Slot[] spellsBar;
    [SerializeField] private Slot basicAttackSlot;

    [SerializeField]
    private Slider healthBarSlider;
    [SerializeField]
    private Text healthBarText;
    [SerializeField]
    private GameObject enemyDamageTextPrefab;

    private float elapsedTime;

    private Slot lastActiveSlot;

    // Start is called before the first frame update
    void Start()
    {
        HandleEvents(true);
        SetMaxHealth(DataStorage.Player.BaseStats.GetStat(BaseStatType.MaxHealth).GetCalculatedStatValue());
        SetAllIcons();
    }

    private void OnDisable()
    {
        HandleEvents(false);
    }

    private void HandleEvents(bool switcher)
    {
        if (switcher)
        {
            EventsManager.OnAbilityPickUpUI += SetIcon;
            EventsManager.OnBasicAttackPickUp += SetIcon;
            EventsManager.OnEnemyHealthBarChange += ChangeEnemyHealthBar;
        }
        else
        {
            EventsManager.OnAbilityPickUpUI -= SetIcon;
            EventsManager.OnBasicAttackPickUp -= SetIcon;
            EventsManager.OnEnemyHealthBarChange += ChangeEnemyHealthBar;
        }
    }

    // Update is called once per frame
    void Update()
    {
        SetHealth(DataStorage.Player.DynamicStats.CurrentHealth);
    }

    public void SetMaxHealth(float value)
    {
        healthBarSlider.maxValue = value;
        healthBarSlider.value = value;
        healthBarText.text = value.ToString();
    }

    public void SetHealth(float value)
    {
        healthBarSlider.value = value;
        healthBarText.text = value.ToString();
    }

    private void SetAllIcons()
    {
        for (int i = 0; i < inventory.Spells.Count; i++)
        {
            if (inventory.Spells[i] != DataStorage.EmptyAbility)
            spellsBar[i].SetIcon(inventory.Spells[i]);
        }

        SetIcon(inventory.CurrentBasicAttack);
    }

    private void SetIcon(int index, Ability ability)
    {
        spellsBar[index].SetIcon(ability);
    }

    private void SetIcon(Ability ability)
    {
        basicAttackSlot.SetIcon(ability);
    }

    public void AbilityDiscard(int index)
    {
        EventsManager.CallOnAbilityDiscard(index);
    }

    private void ChangeEnemyHealthBar(float currentHealth, float maxHealth, Image healthBarImage)
    {
        float percentage = currentHealth / maxHealth;

        healthBarImage.fillAmount = percentage;
    }

    //private void InstantiateEnemyDamageTakenText(float damage, Transform parentForDamageText)
    //{
    //    GameObject tempTextObject = Instantiate(enemyDamageTextPrefab, parentForDamageText.position, Quaternion.identity, parentForDamageText);

    //    Text tempText = tempTextObject.GetComponent<Text>();

    //    Mathf.RoundToInt(damage);

    //    tempText.text = damage.ToString();

    //    StartCoroutine(ScaleDownDamageText(tempTextObject, 3f));
    //}

    //private IEnumerator ScaleDownDamageText(GameObject damageTextObject, float timee)
    //{
    //    float elapsedTime = 0f;
    //    Vector3 tempDamageTextScale = damageTextObject.transform.localScale;
    //    Vector3 tempDamageTextPosition = damageTextObject.transform.position;
    //    Destroy(damageTextObject, timee);

    //    while (elapsedTime < timee)
    //    {
    //        elapsedTime += Time.deltaTime;
    //        tempDamageTextPosition.y = Mathf.Lerp(tempDamageTextPosition.y, (tempDamageTextPosition.y + 0.01f), elapsedTime / timee);
    //        damageTextObject.transform.position = tempDamageTextPosition;

    //        tempDamageTextScale.x = Mathf.Lerp(1f, 0f, elapsedTime / timee);
    //        tempDamageTextScale.y = Mathf.Lerp(1f, 0f, elapsedTime / timee);

    //        damageTextObject.transform.localScale = tempDamageTextScale;

    //        yield return null;
    //    }
    //}
}
