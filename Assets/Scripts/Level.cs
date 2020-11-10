using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level
{
    public int RequiredExperience;

    // temporary variables change later
    float b;
    float g;
    float n;

    public Level (float b, float g, float n)
    {
        this.b = b;
        this.g = g;
        this.n = n;
    }

    public void GrantExperience(int amount, int currentLevel, int currentExperience)
    {
        currentExperience += amount;

        while (currentExperience >= RequiredExperience)
        {
            currentExperience -= RequiredExperience;
            currentLevel++;
        }
    }

    // temporary change later
    public void CalculateStat()
    {
        float current = b + g * (n - 1f) * (0.7025f + 0.0175f * (n - 1));

        int rounded = Mathf.RoundToInt(current);

        Debug.Log($"Current value is: {rounded}");
    }
}
