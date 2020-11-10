using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level
{
    private int currentLevel = 1;
    public int CurrentLevel => currentLevel;

    private int currentExperience = 0;
    public int CurrentExperience => currentExperience;

    public int requiredExperience;
    private int RequiredExperience => requiredExperience;

    public void EnemyToExperience(Enemy enemy)
    {
        GrantExperience(enemy.EnemyStats.CurrentExperience);
    }

    public void GrantExperience(int amount)
    {
        currentExperience += amount;

        while (currentExperience >= requiredExperience)
        {
            currentExperience -= requiredExperience;
            currentLevel++;
            requiredExperience = CalculateRequiredExperience(currentLevel);
        }

        Debug.Log(CurrentLevel);
    }

    // temporary change later
    public void CalculateStat(float b, float g, float n)
    {
        float current = b + g * (n - 1f) * (0.7025f + 0.0175f * (n - 1));

        int rounded = Mathf.RoundToInt(current);

        Debug.Log($"Current value is: {rounded}");
    }

    private int CalculateRequiredExperience(int level)
    {
        return level * 25;
    }
}
