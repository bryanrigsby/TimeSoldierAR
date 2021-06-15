using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{

    public GameObject arCanvas, uiCanvas, statsPanel, itemPanel, itemDescriptionPanel, errorPanel;
    private PlayerStats playerStats;
    public Text hpSliderText, mpSliderText, expSliderText, levelText, strengthText, defenseText, eqpdWpnText, wpnPwrText, eqpdAmrText, amrPwrText, itemDescriptionName, itemDescriptionDescription, itemDescriptionEffectLabelText, itemDescriptionSellAmtLabelText, itemDescriptionEffectValueText, itemDescriptionSellAmtValueText, useButtonText, goldText, errorPanelText;
    public Slider hpSlider, mpSlider, expSlider;
    public Image itemDescriptionImage;

    public ItemButton[] itemButtons;
    public string selectedItem;
    public Item activeItem;

    public static GameMenu instance;

    private float errorPanelTimer = 1.0f;
    public bool errorPanelTimerBool;




    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        UpdateMainStats();
        errorPanelTimerBool = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (arCanvas.activeInHierarchy)
        {
            UpdateMainStats();
        }

        if (errorPanelTimerBool)
        {
            errorPanelTimer -= Time.deltaTime;
            //Debug.Log(errorPanelTimer);
            if (errorPanelTimer <= 0)
            {
                errorPanelText.text = "";
                errorPanel.SetActive(false);
                errorPanelTimerBool = false;
                errorPanelTimer = 1f;
            }
        }


    }

    public void UpdateMainStats()
    {
        playerStats = GameManager.instance.playerStats;

        hpSliderText.text = Mathf.Clamp(playerStats.currentHp, 0, int.MaxValue) + " / " + playerStats.maxHp;
        mpSliderText.text = Mathf.Clamp(playerStats.currentMp, 0, int.MaxValue) + " / " + playerStats.maxMp;
        expSliderText.text = playerStats.currentExp + " / " + playerStats.expToNextLevel[playerStats.playerLevel];
        hpSlider.value = Mathf.Clamp(playerStats.currentHp, 0, int.MaxValue);
        hpSlider.maxValue = playerStats.maxHp;
        expSlider.value = playerStats.currentExp;
        expSlider.maxValue = playerStats.expToNextLevel[playerStats.playerLevel];
        mpSlider.value = Mathf.Clamp(playerStats.currentMp, 0, int.MaxValue);
        mpSlider.maxValue = playerStats.maxMp;
        levelText.text = playerStats.playerLevel.ToString();
        strengthText.text = playerStats.strength.ToString();
        defenseText.text = playerStats.defense.ToString();
        if(playerStats.equippedWpn != "")
        {
            eqpdWpnText.text = playerStats.equippedWpn;
        }
        wpnPwrText.text = playerStats.wpnPower.ToString();
        if (playerStats.equippedArmr != "")
        {
            eqpdAmrText.text = playerStats.equippedArmr;
        }
        amrPwrText.text = playerStats.armrPower.ToString();

        goldText.text = GameManager.instance.currentGold.ToString();

    }

    public void OpenMenu()
    {

        AudioManager.instance.PlayBGM(2);
        arCanvas.SetActive(false);
        uiCanvas.SetActive(true);
        statsPanel.SetActive(true);
    }

    public void CloseMenu()
    {
        AudioManager.instance.ButtonSFX();
        AudioManager.instance.StopMusic();
        AudioManager.instance.PlayBGM(7);
        arCanvas.SetActive(true);
        uiCanvas.SetActive(false);
        statsPanel.SetActive(false);
    }

    public void CloseItemMenu()
    {
        AudioManager.instance.ButtonSFX();
        itemPanel.SetActive(false);
    }

    public void OpenItemDescriptionMenu()
    {
        AudioManager.instance.ButtonSFX();
        itemDescriptionPanel.SetActive(true);//for testing
    }

    public void CloseItemDescriptionMenu()
    {
        AudioManager.instance.ButtonSFX();
        itemDescriptionPanel.SetActive(false);
    }


    public void ShowItems()
    {
        GameManager.instance.SortItems();
        AudioManager.instance.ButtonSFX();
        itemPanel.SetActive(true);
        
        for(int i = 0; i < itemButtons.Length; i++)
        {
            itemButtons[i].buttonValue = i;

            if(GameManager.instance.itemsHeld[i] != "")
            {
                itemButtons[i].gameObject.SetActive(true);
                itemButtons[i].buttonImage.sprite = GameManager.instance.GetItemDetails(GameManager.instance.itemsHeld[i]).itemSprite;
                itemButtons[i].amountText.text = GameManager.instance.numberOfItems[i].ToString();
            }
            else
            {
                itemButtons[i].gameObject.SetActive(false);
                itemButtons[i].amountText.text = "";
            }
        }
    }

    public void SelectItem(Item newItem)
    {
        AudioManager.instance.ButtonSFX();
        itemDescriptionPanel.SetActive(true);

        activeItem = newItem;

        if (activeItem.isItem)
        {
            useButtonText.text = "Use";

            if (activeItem.affectHp)
            {
                itemDescriptionEffectLabelText.text = "Hit Points";
            }

            if (activeItem.affectMp)
            {
                itemDescriptionEffectLabelText.text = "Magic Points";
            }

            if (activeItem.affectStr)
            {
                itemDescriptionEffectLabelText.text = "Strength";
            }

            if (activeItem.affectDef)
            {
                itemDescriptionEffectLabelText.text = "Defense";
            }

            itemDescriptionEffectValueText.text = "+" + activeItem.amountToChange.ToString();
        }

        if(activeItem.isArmor || activeItem.isWeapon)
        {
            useButtonText.text = "Equip";

            if (activeItem.affectStr)
            {
                itemDescriptionEffectLabelText.text = "Strength";
                itemDescriptionEffectValueText.text = "+" + activeItem.weaponStr.ToString();
            }

            if (activeItem.affectDef)
            {
                itemDescriptionEffectLabelText.text = "Defense";
                itemDescriptionEffectValueText.text = "+" + activeItem.armorStr.ToString();
            }
        }

        itemDescriptionName.text = activeItem.itemName;
        itemDescriptionImage.sprite = activeItem.itemSprite;
        itemDescriptionDescription.text = activeItem.description;
        itemDescriptionSellAmtValueText.text = Mathf.FloorToInt(activeItem.value / 2).ToString();
        
    }

    public void UseItem()
    {
        activeItem.Use();
        itemDescriptionPanel.SetActive(false);
    }

    public void DropItem()
    {
        activeItem.Drop();
        itemDescriptionPanel.SetActive(false);
    }

    


}
