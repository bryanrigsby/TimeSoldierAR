using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class MagicSpells : MonoBehaviour
{
    public string spellName;
    public int spellPower;
    public int spellCost;
    public bool isHeal;
    public Sprite spellImage;

    public void Cast()
    {

        PlayerStats playerStats = GameManager.instance.playerStats;

        if (isHeal)
        {
            if (playerStats.currentHp >= playerStats.maxHp)
            {
                BattleManager.instance.battleDialog.text = "You have full HP!";
                AudioManager.instance.PlaySFX(15);
            }
            else
            {
                playerStats.currentHp += spellPower;
                playerStats.currentMp -= spellCost;
                BattleManager.instance.battleDialog.text = "You were healed by " + spellPower + " Hp!";
                AudioManager.instance.PlaySFX(9);
            }

            if(playerStats.currentHp >= playerStats.maxHp)
            {
                playerStats.currentHp = playerStats.maxHp;
            }
            
        }
        else
        {
            playerStats.currentMp -= spellCost;
            BattleManager.instance.battleDialog.text = "You used " + spellName + " and hit " + BattleManager.instance.currentEnemy.charName + " by " + spellPower + " Hp!";
            BattleManager.instance.currentEnemy.currentHp -= spellPower;
            AudioManager.instance.PlaySFX(10);
        }
    }
}
