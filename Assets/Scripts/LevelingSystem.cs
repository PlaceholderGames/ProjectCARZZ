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
    private List<AICollision> AI;
    private int nAi;

    // Use this for initialization
    void Start()
    {
        nAi = GameObject.FindObjectsOfType<AICollision>().Length;
        for (int i = 0; i < nAi; i++)
        {
            AI[i] = GameObject.FindObjectsOfType<AICollision>()[i];
        }
        totalExperience(currentLevel);
        //Debug.Log("This is the exp" + currentXP);
    }

    // Update is called once per frame
    void Update()
    {
        if (nAi <= GameObject.FindObjectsOfType<AICollision>().Length)
        {

            nAi = GameObject.FindObjectsOfType<AICollision>().Length;
            for (int i = 0; i < nAi; i++)
            {
                Debug.Log(nAi);
                AI[i] = GameObject.FindObjectsOfType<AICollision>()[i];
                if (AI[i].isDead)
                {
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
            Debug.Log("This is the exp 2" + currentXP);
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