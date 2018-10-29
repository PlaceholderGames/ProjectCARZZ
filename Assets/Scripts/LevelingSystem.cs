using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelingSystem : MonoBehaviour {

    public int currentLevel = 1;
    private int maxLevel = 20;
    public int givenXP;
    public int currentXP;
    public int totalXP = 10;
    private AiBehavior[] aiBehavior;
    private AiBehavior nAi;

	// Use this for initialization
	void Start () {
        nAi = GameObject.FindGameObjectsWithTag("Zombie").GetLength;
		for(int i = 0; i < nAi; i++)
        {
            aiBehavior[i] = GameObject.FindGameObjectsWithTag("Zombie")[i];
        }
        totalExperience(currentLevel);
	}
	
	// Update is called once per frame
	void Update () {
        if (nAi < GameObject.FindGameObjectsWithTag("Zombie").GetLength)
        { 
            nAi = GameObject.FindGameObjectsWithTag("Zombie").GetLength;
            for (int i = 0; i < nAi; i++)
            {
                aiBehavior[i] = GameObject.FindGameObjectsWithTag("Zombie")[i];
                if (aiBehavior[i].isDead)
                {
                    currentXP += 1;
                }
            }
        }
        if(currentXP >= totalXP)
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
        else if (lvl == maxLevel+1)
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
