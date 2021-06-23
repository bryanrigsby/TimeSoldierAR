using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public GameObject mainCanvas, battlePanel, coverPanel, magicPanel, magicDescriptionPanel, battleItemPanel, battleItemDescriptionPanel, gameOverPanel, rewardPanel, levelUpPanel;

    public Slider hpSlider, mpSlider, monsterHpSlider;

    public Text hpSliderText, monsterHpSliderText, mpSliderText, monsterNameText, battleDialog, mpPanelCurrentMp, mpDescriptionPanelSpellName, mpDescriptionPanelDescription, mpDescriptionPanelEffect, mpDescriptionPanelEffectAmt, mpDescriptionPanelCost, itemDescriptionName, itemDescriptionDescription, itemDescriptionEffectLabelText, itemDescriptionSellAmtLabelText, itemDescriptionEffectValueText, itemDescriptionSellAmtValueText, goldRewardText, expRewardText, goldLostText, expLostText, levelUpLevel, levelUpHp, levelUpMp, levelUpStr, levelUpDef, levelUpExp;

    public Image monsterImage, mpDescriptionPanelImage, itemDescriptionImage, rewardImage;

    public BattleChar[] enemies;
    public BattleChar currentEnemy;
    public PlayerStats player;
    public SpellButton[] spellButtons;
    public ItemButton[] battleItemButtons;
    public MagicSpells activeSpell;
    public Item activeItem;

    private int expEarned;
    private int goldEarned;
    public int currentTurn;
    private bool turnWaiting;
    public int chanceToFlee = 35;
    private bool fleeSuccessBool;



    public static BattleManager instance;


    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        fleeSuccessBool = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (turnWaiting && fleeSuccessBool != true)
        {
            if(currentTurn == 1)//player turn
            {
                coverPanel.SetActive(false);
            }
            else//enemy turn
            {
                coverPanel.SetActive(true);
                StartCoroutine(EnemyMoveCo());
            }
        }

    }

    public void StartBattle(string enemy)
    {
        AudioManager.instance.ButtonSFX();
        
        //choose random enemy from enemies array and pass to BattleStart
        //System.Random strRan = new System.Random();
        //int ranEnemy = strRan.Next(0, enemies.Length);
        //currentEnemy = enemies[ranEnemy];

        if(enemy == "Shaman")
        {
            currentEnemy = enemies[6];
            AudioManager.instance.PlayBGM(1);
        }
        else
        {
            AudioManager.instance.PlayBGM(0);
            for (int i = 0; i < enemies.Length; i++)
            {
                if (enemy == enemies[i].charName)
                {
                    currentEnemy = enemies[i];
                }
            }
        }


        //choose random turn (1 for player, 2 for enemy) to start
        System.Random turnRan = new System.Random();
        currentTurn = turnRan.Next(1, 3);//give 1 and 2

        if(currentTurn == 1)
        {
            battleDialog.text = "Strike First!";
            chanceToFlee = 100;
        }
        else
        {
            battleDialog.text = "Monster Attack!";
        }

        turnWaiting = true;

        //open battle panel
        mainCanvas.SetActive(true);
        battlePanel.SetActive(true);

        //set player stats

        UpdateUIStats();

        //set enemy name and image
        monsterNameText.text = currentEnemy.charName;
        monsterImage.sprite = currentEnemy.charImage;
        monsterHpSliderText.text = Mathf.Clamp(currentEnemy.currentHp, 0, int.MaxValue) + " / " + currentEnemy.maxHp;
        monsterHpSlider.value = Mathf.Clamp(currentEnemy.currentHp, 0, int.MaxValue);
        monsterHpSlider.maxValue = currentEnemy.maxHp;

    }

    //public IEnumerator ReadyForBattleTimer()
    //{
    //    while (GameManager.instance.betweenBattleCounter > 0)
    //    {
    //        yield return new WaitForSeconds(0.001f);
    //        GameManager.instance.betweenBattleCounter -= Time.deltaTime;
    //        Debug.Log(GameManager.instance.betweenBattleCounter);
    //    }

    //    StartBattle();

    //}

    public void NextTurn()
    {
        UpdateUIStats();
        chanceToFlee = 35;

        if (currentEnemy.currentHp <= 0 || player.currentHp <= 0)
        {
            if (currentEnemy.currentHp <= 0)
            {
                //end battle in victory
                monsterImage.gameObject.SetActive(false);
                coverPanel.SetActive(true);
                battleDialog.text = "You have successfully defeated " + currentEnemy.charName + "!";
                StartCoroutine(EndBattleCo());

            }
            else if (player.currentHp <= 0)
            {
                //end battle in failure
                coverPanel.SetActive(true);
                battleDialog.text = currentEnemy.charName + " has destroyed you!";
                StartCoroutine(GameOverCo());

            }


        }else
        {
            currentTurn++;
            if (currentTurn > 2)
            {
                currentTurn = 1;
            }

            turnWaiting = true;
        }
        
    }

    public void UpdateUIStats()
    {
        hpSliderText.text = Mathf.Clamp(player.currentHp, 0, int.MaxValue) + " / " + player.maxHp;
        mpSliderText.text = Mathf.Clamp(player.currentMp, 0, int.MaxValue) + " / " + player.maxMp;
        hpSlider.value = Mathf.Clamp(player.currentHp, 0, int.MaxValue);
        hpSlider.maxValue = player.maxHp;
        mpSlider.value = Mathf.Clamp(player.currentMp, 0, int.MaxValue);
        mpSlider.maxValue = player.maxMp;

        monsterHpSliderText.text = Mathf.Clamp(currentEnemy.currentHp, 0, int.MaxValue) + " / " + currentEnemy.maxHp;
        monsterHpSlider.value = Mathf.Clamp(currentEnemy.currentHp, 0, int.MaxValue);
        monsterHpSlider.maxValue = currentEnemy.maxHp;
        mpPanelCurrentMp.text = player.currentMp.ToString();
    }


    public IEnumerator EnemyMoveCo()
    {
        turnWaiting = false;
        yield return new WaitForSeconds(1f);
        EnemyAttack();
        yield return new WaitForSeconds(1.5f);
        battleDialog.text = "";
        NextTurn();
    }

    public void EnemyAttack()
    {

        float damageCalc = (currentEnemy.strength / player.defense) * Random.Range(0.1f, 2.9f);
        int damageToGive = Mathf.RoundToInt(damageCalc);

        if (damageToGive == 0)
        {
            battleDialog.text = currentEnemy.charName + " missed!";
            AudioManager.instance.PlaySFX(16);
        }
        else
        {
            battleDialog.text = currentEnemy.charName + " dealt " + damageToGive + " damage to you!";
            AudioManager.instance.PlaySFX(12);
        }

        player.currentHp -= damageToGive;

        UpdateUIStats();
    }

    public void PlayerAttack()
    {
        coverPanel.SetActive(true);
        float damageCalc = ((player.strength + player.wpnPower) / currentEnemy.defense) * Random.Range(0.1f, 2.9f);
        int damageToGive = Mathf.RoundToInt(damageCalc);
        if(damageToGive == 0)
        {
            AudioManager.instance.PlaySFX(16);
            battleDialog.text = "You missed!"; 
        }
        else
        {
            AudioManager.instance.PlaySFX(13);
            battleDialog.text = "You dealt " + damageToGive + " damage to " + currentEnemy.charName + "!";  
        }
        
        currentEnemy.currentHp -= damageToGive;
        UpdateUIStats();
        StartCoroutine(BattlePauseCo());
    }

    public IEnumerator BattlePauseCo()
    {
        turnWaiting = false;
        yield return new WaitForSeconds(1f);
        battleDialog.text = "";
        yield return new WaitForSeconds(0.5f);
        NextTurn();
    }

    public IEnumerator MagicPauseCo()
    {
        turnWaiting = false;
        yield return new WaitForSeconds(2.0f);
        battleDialog.text = "";
        yield return new WaitForSeconds(0.5f);
        NextTurn();
    }

    public void OpenMagicPanel()
    {
        magicPanel.SetActive(true);
        ShowSpells();
    }

    public void CloseMagicPanel()
    {
        magicPanel.SetActive(false);
    }

    public void CloseMagicDescriptionPanel()
    {
        magicDescriptionPanel.SetActive(false);
    }

    public void ShowSpells()
    {
        for (int j = 0; j < spellButtons.Length; j++)
        {
            if (player.referenceSpells[j].spellCost <= player.maxMp)
            {
                spellButtons[j].buttonValue = j;
                spellButtons[j].spellName.text = player.referenceSpells[j].spellName;
                spellButtons[j].costText.text = player.referenceSpells[j].spellCost.ToString();
            }
            else
            {
                spellButtons[j].gameObject.SetActive(false);
                
            }
        }
    }

    

    public void SelectSpell(MagicSpells spell)
    {
        Debug.Log(spell);
        activeSpell = spell;
        if (player.currentMp >= spell.spellCost)
        {
            magicDescriptionPanel.SetActive(true);
            mpDescriptionPanelSpellName.text = activeSpell.spellName;
            mpDescriptionPanelImage.sprite = activeSpell.spellImage;
            mpDescriptionPanelCost.text = activeSpell.spellCost.ToString();
            mpDescriptionPanelEffectAmt.text = activeSpell.spellPower.ToString();
            mpDescriptionPanelEffect.text = "Hit Points:";
            if (activeSpell.isHeal)
            {
                mpDescriptionPanelDescription.text = "This spell heals by " + activeSpell.spellPower + " Hp";
            }
            else
            {
                mpDescriptionPanelDescription.text = "This spell damages by " + activeSpell.spellPower + " Hp";
            }
        }
        else
        {
            battleDialog.text = "You do not have enough Mp to cast " + activeSpell.spellName;
            AudioManager.instance.PlaySFX(15);
            magicPanel.SetActive(false);
            return;
        }
    }

    public void CastSpell()
    {
        activeSpell.Cast();
        magicDescriptionPanel.SetActive(false);
        magicPanel.SetActive(false);
        coverPanel.SetActive(true);
        UpdateUIStats();
        StartCoroutine(MagicPauseCo());
    }


    public void ShowItems()
    {
        GameManager.instance.SortItems();
        battleItemPanel.SetActive(true);
        

        for (int i = 0; i < battleItemButtons.Length; i++)
        {
            battleItemButtons[i].buttonValue = i;

            if (GameManager.instance.itemsHeld[i] != "")
            {
                battleItemButtons[i].gameObject.SetActive(true);
                battleItemButtons[i].buttonImage.sprite = GameManager.instance.GetItemDetails(GameManager.instance.itemsHeld[i]).itemSprite;
                battleItemButtons[i].amountText.text = GameManager.instance.numberOfItems[i].ToString();
            }
            else
            {
                battleItemButtons[i].gameObject.SetActive(false);
                battleItemButtons[i].amountText.text = "";
            }
        }
    }

    public void SelectItem(Item newItem)
    {
        activeItem = newItem;

        if (activeItem.isArmor || activeItem.isWeapon)
        {
            battleDialog.text = "Cannot equip during battle";
            AudioManager.instance.PlaySFX(15);
            battleItemPanel.SetActive(false);
        }

        if (activeItem.isItem)
        {

            battleItemDescriptionPanel.SetActive(true);
            itemDescriptionName.text = activeItem.itemName;
            itemDescriptionImage.sprite = activeItem.itemSprite;
            itemDescriptionDescription.text = activeItem.description;
            itemDescriptionSellAmtValueText.text = Mathf.FloorToInt(activeItem.value / 2).ToString();

            if (activeItem.affectHp)
            {
                itemDescriptionEffectLabelText.text = "Hit Points:";
            }

            if (activeItem.affectMp)
            {
                itemDescriptionEffectLabelText.text = "Magic Points:";
            }

            itemDescriptionEffectValueText.text = "+" + activeItem.amountToChange.ToString();
        }
    }

    public void CloseBattleItemMenu()
    {
        battleItemPanel.SetActive(false);
    }

    public void UseItem()
    {
        activeItem.Use();
        battleItemDescriptionPanel.SetActive(false);
        battleItemPanel.SetActive(false);
        GameMenu.instance.itemPanel.SetActive(false);
        GameMenu.instance.UpdateMainStats();
        UpdateUIStats();
        NextTurn();
    }

    public void DropItem()
    {
        activeItem.Drop();
        battleItemDescriptionPanel.SetActive(false);
        battleItemPanel.SetActive(false);
        GameMenu.instance.UpdateMainStats();
        UpdateUIStats();
        NextTurn();
    }

    public void CloseItemDescriptionMenu()
    {
        battleItemDescriptionPanel.SetActive(false);
    }

    public void Flee()
    {
        
        int fleeSuccess = Random.Range(0, 100);
        if(fleeSuccess < chanceToFlee)
        {
            //end the battle
            fleeSuccessBool = true;
            coverPanel.SetActive(true);
            int fleeGoldLost = Mathf.FloorToInt(GameManager.instance.currentGold * 0.02f);
            GameManager.instance.currentGold -= fleeGoldLost;
            AudioManager.instance.PlaySFX(18);
            battleDialog.text = "You successfully ran away but dropped " + fleeGoldLost + " gold!";
            StartCoroutine(FleeCo());
        }
        else
        {
            AudioManager.instance.PlaySFX(17);
            battleDialog.text = "Couldn't escape!";
            NextTurn();
        }
    }

    public IEnumerator EndBattleCo() //player won
    {
        coverPanel.SetActive(true);
        int playerLevel = player.playerLevel;
        AudioManager.instance.StopMusic();
        AudioManager.instance.PlayBGM(6);
        yield return new WaitForSeconds(4f);

        expRewardText.text = "You have won " + currentEnemy.exp + " exp!";
        goldRewardText.text = "You have won " + currentEnemy.gold + " gold!";
        
        GameManager.instance.currentGold += currentEnemy.gold;
        rewardPanel.SetActive(true);

        player.AddExp(currentEnemy.exp);

        if (playerLevel < player.playerLevel)
        {
            AudioManager.instance.PlaySFX(19);
            yield return new WaitForSeconds(3f);
            levelUpLevel.text = player.playerLevel.ToString();
            levelUpHp.text = player.maxHp.ToString();
            levelUpMp.text = player.maxMp.ToString();
            levelUpStr.text = player.strength.ToString();
            levelUpDef.text = player.defense.ToString();
            levelUpExp.text = player.expToNextLevel[player.playerLevel].ToString();
            rewardPanel.SetActive(false);
            levelUpPanel.SetActive(true);
            yield return new WaitForSeconds(4f);
        }
        else
        {
            yield return new WaitForSeconds(6f);
        }

        UpdateUIStats();
        currentEnemy.currentHp = currentEnemy.maxHp;//reset prefab hp after battle
        AudioManager.instance.StopMusic();
        AudioManager.instance.PlayBGM(7);
        levelUpPanel.SetActive(false);
        rewardPanel.SetActive(false);
        battlePanel.SetActive(false);
        mainCanvas.SetActive(false);
        battleDialog.text = "";
        monsterImage.gameObject.SetActive(true);
    }

    public IEnumerator FleeCo()
    {
        yield return new WaitForSeconds(3f);
        currentEnemy.currentHp = currentEnemy.maxHp;//reset prefab hp after battle
        AudioManager.instance.StopMusic();
        AudioManager.instance.PlayBGM(7);
        battlePanel.SetActive(false);
        mainCanvas.SetActive(false);
        battleDialog.text = "";
        fleeSuccessBool = false;
    }

    public IEnumerator GameOverCo()
    {
        coverPanel.SetActive(true);
        AudioManager.instance.StopMusic();
        AudioManager.instance.PlayBGM(8);
        yield return new WaitForSeconds(3f);

        int goldLost = Mathf.FloorToInt(GameManager.instance.currentGold * 0.1f);
        int expLost = Mathf.FloorToInt(player.currentExp * 0.05f);

        goldLostText.text = "You lost " + goldLost + " gold!";
        expLostText.text = "You lost " + expLost + " exp!";
        player.currentExp -= expLost;
        GameManager.instance.currentGold -= goldLost;
        if (player.currentExp < 0)
        {
            player.currentExp = 0;
        }
        if(GameManager.instance.currentGold < 0)
        {
            GameManager.instance.currentGold = 0;
        }
        gameOverPanel.SetActive(true);
        player.currentHp = Mathf.FloorToInt(player.maxHp * 0.1f);

        yield return new WaitForSeconds(3f);

        currentEnemy.currentHp = currentEnemy.maxHp;//reset prefab hp after battle
        AudioManager.instance.StopMusic();
        AudioManager.instance.PlayBGM(7);
        gameOverPanel.SetActive(false);
        battlePanel.SetActive(false);
        mainCanvas.SetActive(false);
        battleDialog.text = "";

    }

}
