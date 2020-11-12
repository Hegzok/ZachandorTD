using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevel
{
    private float currentLevel = 1;
    public float CurrentLevel => currentLevel;

    private float currentExperience = 0;
    public float CurrentExperience => currentExperience;

    public float requiredExperience;
    private float RequiredExperience => requiredExperience;

    public void EnemyToExperience(Enemy enemy)
    {
        GrantExperience(enemy.BaseStats.GetStatFinalValue(BaseStatType.BaseExperience));
    }

    public void GrantExperience(float amount)
    {
        currentExperience += amount;

        while (currentExperience >= requiredExperience)
        {
            currentExperience -= requiredExperience;
            currentLevel++;
            Debug.Log($"You advanced from level {currentLevel - 1} to level {currentLevel}");
            requiredExperience = CalculateRequiredExperience(currentLevel);
        }
    }

    // temporary change later
    public void CalculateStat(float b, float g, float n)
    {
        float current = b + g * (n - 1f) * (0.7025f + 0.0175f * (n - 1));

        int rounded = Mathf.RoundToInt(current);

        Debug.Log($"Current value is: {rounded}");
    }

    private float CalculateRequiredExperience(float level)
    {
        return level * 25;
    }
}
