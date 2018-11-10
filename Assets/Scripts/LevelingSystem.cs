using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelingSystem : MonoBehaviour
{

    public int currentLevel = 1;
    private int maxLevel = 20;
    public int givenXP;
    public int currentXP;
    public int totalXP = 10;
    private AICollision[] AI;
    private int nAi;

    // Use this for initialization
    void Start()
    {
        AI = FindObjectsOfType<AICollision>();
        Debug.Log(FindObjectsOfType<AICollision>());
        Debug.Log(AI);
        nAi = FindObjectsOfType<AICollision>().Length;
        totalExperience(currentLevel);
    }

    // Update is called once per frame
    void UpdateLevelingSystem()
    {
        if (nAi <= FindObjectsOfType<AICollision>().Length)
        {
            AI = FindObjectsOfType<AICollision>();
            nAi = FindObjectsOfType<AICollision>().Length;
            for (int i = 0; i < nAi; i++)
            {
                if (AI[i].isDead)
                {
                    Debug.Log("Zombie died!");
                    currentXP += 1;

                }
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
            totalExperience(currentLevel);
        }
    }

    private bool totalExperience(int lvl)
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