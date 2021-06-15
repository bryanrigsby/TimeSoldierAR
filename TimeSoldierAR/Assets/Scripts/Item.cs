using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

    public bool isItem, isWeapon, isArmor, affectHp, affectMp, affectStr, affectDef;
    public string itemName, description;
    public int value, amountToChange, weaponStr, armorStr;
    public Sprite itemSprite;





    public void Use()
    {

        PlayerStats playerStats = GameManager.instance.playerStats;

        if (isItem)
        {
            if (affectHp)
            {
                if(playerStats.currentHp >= playerStats.maxHp)
                {
                    GameMenu.instance.errorPanelTimerBool = true;
                    GameMenu.instance.errorPanelText.text = "You have full HP!";
                    GameMenu.instance.errorPanel.SetActive(true);
                    AudioManager.instance.PlaySFX(15);
                }
                else
                {
                    playerStats.currentHp += amountToChange;

                    if (playerStats.currentHp >= playerStats.maxHp)
                    {
                        playerStats.currentHp = playerStats.maxHp;
                    }

                    GameManager.instance.RemoveItem(itemName);
                    GameMenu.instance.UpdateMainStats();
                }
                

                
            }

            if (affectMp)
            {
                if (playerStats.currentMp >= playerStats.maxMp)
                {
                    GameMenu.instance.errorPanelTimerBool = true;
                    GameMenu.instance.errorPanelText.text = "You have full MP!";
                    GameMenu.instance.errorPanel.SetActive(true);
                    AudioManager.instance.PlaySFX(15);
                }
                else
                {
                    playerStats.currentMp += amountToChange;

                    if (playerStats.currentMp > playerStats.maxMp)
                    {
                        playerStats.currentMp = playerStats.maxMp;
                    }

                    GameManager.instance.RemoveItem(itemName);
                    GameMenu.instance.UpdateMainStats();
                }

                
            }

            if (affectStr)
            {
                playerStats.strength += amountToChange;

                GameManager.instance.RemoveItem(itemName);
                GameMenu.instance.UpdateMainStats();
            }

            if (affectDef)
            {
                playerStats.defense += amountToChange;

                GameManager.instance.RemoveItem(itemName);
                GameMenu.instance.UpdateMainStats();
            }
        }

        if (isWeapon)
        {
            if(playerStats.equippedWpn != "" && playerStats.equippedWpn != itemName)
            {
                GameManager.instance.AddItem(playerStats.equippedWpn);
                playerStats.equippedWpn = itemName;
                playerStats.wpnPower = weaponStr;
                playerStats.strength += weaponStr;

                GameManager.instance.RemoveItem(itemName);
                GameMenu.instance.UpdateMainStats();
            }
            else
            {
                GameMenu.instance.errorPanelTimerBool = true;
                GameMenu.instance.errorPanelText.text = "You are already equipped with that!";
                GameMenu.instance.errorPanel.SetActive(true);
                AudioManager.instance.PlaySFX(15);
            }

            
        }

        if (isArmor)
        {
            if (playerStats.equippedArmr != "" && playerStats.equippedArmr != itemName)
            {
                GameManager.instance.AddItem(playerStats.equippedArmr);
                playerStats.equippedArmr = itemName;
                playerStats.armrPower = armorStr;
                playerStats.defense += armorStr;

                GameManager.instance.RemoveItem(itemName);
                GameMenu.instance.UpdateMainStats();
            }
            else
            {
                GameMenu.instance.errorPanelTimerBool = true;
                GameMenu.instance.errorPanelText.text = "You are already equipped with that!";
                GameMenu.instance.errorPanel.SetActive(true);
                AudioManager.instance.PlaySFX(15);
            }

        }

    }

    public void Drop()
    {
        AudioManager.instance.PlaySFX(11);
        GameManager.instance.RemoveItem(itemName);
        GameMenu.instance.UpdateMainStats();
    }
}
