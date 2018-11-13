using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelingSystem : MonoBehaviour
{

    public int currentLevel = 1;
    private int maxLevel = 20;
    public int currentXP = 0;
    public int totalXP = 10;
    public float MaxDamage;
    public float MinDamage;
    [HideInInspector]
    public float RecievedDamage; //Players/Vehicles health


    // Use this for initialization
    void Start()
    {
        
        TotalExperience(currentLevel);
    }

    // Update is called once per frame
    public void UpdateLevelingSystems()
    {
        
           
            for (int i = 0; i < FindObjectsOfType<AICollision>().Length; i++)
            {
                if (FindObjectsOfType<AICollision>()[i].isDead)
                {
                    currentXP += 1;
                    RecievedDamage = Random.Range(MinDamage, MaxDamage);
                    FindObjectsOfType<AICollision>()[i].isDead = false;

                }
            }
        
        if (currentXP >= totalXP)
        {
            if (currentXP > totalXP)
            {
                currentXP = totalXP - currentXP;
                currentLevel++;
            }
            else
            {
                currentXP = 0;
                currentLevel++;
            }
            TotalExperience(currentLevel);
        }
    }

    private bool TotalExperience(int lvl)
    {
        if (lvl == 1)
        {
            totalXP = totalXP * lvl;
            return true;
        }
        else if (lvl > 1)
        {
            totalXP = totalXP * lvl + (int)((float)totalXP / 2f);
            return true;
        }
        else if (lvl == maxLevel + 1)
        {
            currentXP = totalXP;
            return true;
        }
        else
        {
            return false;
        }
    }
}