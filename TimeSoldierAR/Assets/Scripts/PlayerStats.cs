using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    //Level and Experience
    public int playerLevel = 1;
    public int currentExp;
    public int[] expToNextLevel;
    public int maxLevel = 100;
    public int baseExp = 1000;

    //Health, Magic and Equipment
    public int maxHp = 1000;
    public int maxMp = 500;
    public int currentHp, currentMp, strength, defense, wpnPower, armrPower;
    public string equippedWpn;
    public string equippedArmr;

    //Spells
    public string[] spells;
    public MagicSpells[] referenceSpells;

    public static PlayerStats instance;

    // Start is called before the first frame update
    void Start()
    {

        instance = this;
        expToNextLevel = new int[maxLevel];
        expToNextLevel[1] = baseExp;

        for (int i = 2; i < expToNextLevel.Length; i++)
        {
            expToNextLevel[i] = Mathf.FloorToInt(expToNextLevel[i - 1] * 1.05f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddExp(int expToAdd)
    {
        currentExp += expToAdd;

        if(playerLevel < maxLevel)
        {
            if (currentExp >= expToNextLevel[playerLevel])
            {

                currentExp -= expToNextLevel[playerLevel];

                playerLevel++;

                //Hp
                maxHp = Mathf.FloorToInt(maxHp * 1.05f);
                currentHp = maxHp;

                //Mp
                maxMp = Mathf.FloorToInt(maxMp * 1.05f);
                currentMp = maxMp;

                //Strength
                System.Random strRan = new System.Random();
                strength += strRan.Next(1, 10);

                //Defense
                System.Random defRan = new System.Random();
                defense += defRan.Next(1, 10);

            }
        }

        if(playerLevel >= maxLevel)
        {
            currentExp = 0;
        }

        
    }

    public MagicSpells GetSpellDetails(string spellToGrab)
    {

        Debug.Log(spellToGrab);
        for (int i = 0; i < referenceSpells.Length; i++)
        {
            if (referenceSpells[i].spellName == spellToGrab)
            {
                Debug.Log(referenceSpells[i].spellName);
                return referenceSpells[i];
            }
        }

        return null;
    }


}
