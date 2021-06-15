using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{

    public GameObject mainCanvas, shopPanel, buyPanel, buyDescriptionPanel, sellPanel, sellDescriptionPanel;

    public Text shopGoldText, buyGoldText, sellGoldText;

    public string[] itemsForSale = new string[24];

    public ItemButton[] buyItemButtons;
    public ItemButton[] sellItemButtons;

    public Item selectItem;
    public Text buyItemNameText, buyItemDescriptionText, buyItemEffectLabelText, buyItemEffectValueText, buyItemValueText;
    public Text sellItemNameText, sellItemDescriptionText, sellItemEffectLabelText, sellItemEffectValueText, sellItemValueText;

    public Image buyItemImage, sellItemImage;

    public static Shop instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenShop()
    {
        AudioManager.instance.ButtonSFX();
        AudioManager.instance.PlayBGM(4);

        mainCanvas.SetActive(true);
        shopPanel.SetActive(true);

        shopGoldText.text = GameManager.instance.currentGold.ToString();
    }

    public void CloseShop()
    {
        AudioManager.instance.StopMusic();
        AudioManager.instance.PlayBGM(7);
        shopPanel.SetActive(false);
        GameMenu.instance.itemPanel.SetActive(false);
        mainCanvas.SetActive(false);
    }

    public void OpenBuyPanel()
    {
        buyPanel.SetActive(true);
        buyGoldText.text = GameManager.instance.currentGold.ToString();

        GameManager.instance.SortItems();

        for (int i = 0; i < buyItemButtons.Length; i++)
        {
            buyItemButtons[i].buttonValue = i;

            if (itemsForSale[i] != "")
            {
                buyItemButtons[i].buttonImage.gameObject.SetActive(true);
                buyItemButtons[i].buttonImage.sprite = GameManager.instance.GetItemDetails(itemsForSale[i]).itemSprite;
                buyItemButtons[i].amountText.text = "";
            }
            else
            {
                buyItemButtons[i].gameObject.SetActive(false);
                buyItemButtons[i].amountText.text = "";
            }
        }
    }

    public void CloseBuyPanel()
    {
        buyPanel.SetActive(false);
    }

    public void CloseBuyDescriptionPanel()
    {
        buyDescriptionPanel.SetActive(false);
    }

    public void CloseSellDescriptionPanel()
    {
        sellDescriptionPanel.SetActive(false);
    }

    public void OpenSellPanel()
    {
        sellPanel.SetActive(true);
        sellGoldText.text = GameManager.instance.currentGold.ToString();

        ShowSellItems();
    }

    private void ShowSellItems()
    {
        GameManager.instance.SortItems(); 

        for (int i = 0; i < sellItemButtons.Length; i++)
        {
            sellItemButtons[i].buttonValue = i;

            if (GameManager.instance.itemsHeld[i] != "")
            {
                sellItemButtons[i].gameObject.SetActive(true);
                sellItemButtons[i].buttonImage.sprite = GameManager.instance.GetItemDetails(GameManager.instance.itemsHeld[i]).itemSprite;
                sellItemButtons[i].amountText.text = GameManager.instance.numberOfItems[i].ToString();
            }
            else
            {
                sellItemButtons[i].gameObject.SetActive(false);
                sellItemButtons[i].amountText.text = "";
            }
        }
    }

    public void CloseSellPanel()
    {
        sellPanel.SetActive(false);
    }

    public void SelectBuyItem(Item buyItem)
    {
        if(buyItem != null)
        {
            selectItem = buyItem;

            if (selectItem.isItem)
            {

                if (selectItem.affectHp)
                {
                    buyItemEffectLabelText.text = "Hit Points";
                }

                if (selectItem.affectMp)
                {
                    buyItemEffectLabelText.text = "Magic Points";
                }

                if (selectItem.affectStr)
                {
                    buyItemEffectLabelText.text = "Strength";
                }

                if (selectItem.affectDef)
                {
                    buyItemEffectLabelText.text = "Defense";
                }

                buyItemEffectValueText.text = "+" + selectItem.amountToChange.ToString();
            }

            if (selectItem.isArmor || selectItem.isWeapon)
            {

                if (selectItem.affectStr)
                {
                    buyItemEffectLabelText.text = "Strength";
                    buyItemEffectValueText.text = "+" + selectItem.weaponStr.ToString();
                }

                if (selectItem.affectDef)
                {
                    buyItemEffectLabelText.text = "Defense";
                    buyItemEffectValueText.text = "+" + selectItem.armorStr.ToString();
                }
            }

            buyItemNameText.text = selectItem.itemName;
            buyItemImage.sprite = selectItem.itemSprite;
            buyItemDescriptionText.text = selectItem.description;
            buyItemValueText.text = selectItem.value.ToString();

            buyDescriptionPanel.SetActive(true);
        }

        
 
    }

    public void SelectSellItem(Item sellItem)
    {
        if(sellItem != null)
        {
            selectItem = sellItem;

            if (selectItem.isItem)
            {

                if (selectItem.affectHp)
                {
                    sellItemEffectLabelText.text = "Hit Points";
                }

                if (selectItem.affectMp)
                {
                    sellItemEffectLabelText.text = "Magic Points";
                }

                if (selectItem.affectStr)
                {
                    sellItemEffectLabelText.text = "Strength";
                }

                if (selectItem.affectDef)
                {
                    sellItemEffectLabelText.text = "Defense";
                }

                sellItemEffectValueText.text = "+" + selectItem.amountToChange.ToString();
            }

            if (selectItem.isArmor || selectItem.isWeapon)
            {

                if (selectItem.affectStr)
                {
                    sellItemEffectLabelText.text = "Strength";
                    sellItemEffectValueText.text = "+" + selectItem.weaponStr.ToString();
                }

                if (selectItem.affectDef)
                {
                    sellItemEffectLabelText.text = "Defense";
                    sellItemEffectValueText.text = "+" + selectItem.armorStr.ToString();
                }
            }

            sellItemNameText.text = selectItem.itemName;
            sellItemImage.sprite = selectItem.itemSprite;
            sellItemDescriptionText.text = selectItem.description;
            sellItemValueText.text = Mathf.FloorToInt(selectItem.value / 2).ToString();

            sellDescriptionPanel.SetActive(true);
        }

    }

    public void BuyItem()
    {
        if(selectItem != null)
        {
            if(GameManager.instance.currentGold >= selectItem.value)
            {
                GameManager.instance.currentGold -= selectItem.value;

                AudioManager.instance.ShopButtonSFX();

                GameManager.instance.AddItem(selectItem.itemName);

                shopGoldText.text = GameManager.instance.currentGold.ToString();
                buyGoldText.text = GameManager.instance.currentGold.ToString();
                sellGoldText.text = GameManager.instance.currentGold.ToString();

                GameMenu.instance.UpdateMainStats();
            }
            else
            {
                GameMenu.instance.errorPanelTimerBool = true;
                GameMenu.instance.errorPanelText.text = "You do not have enough gold!";
                GameMenu.instance.errorPanel.SetActive(true);
                AudioManager.instance.PlaySFX(15);
            }
        }

        

        buyDescriptionPanel.SetActive(false);

        
    }

    public void SellItem()
    {
        if(selectItem != null)
        {
            GameManager.instance.currentGold += Mathf.FloorToInt(selectItem.value / 2);

            GameManager.instance.RemoveItem(selectItem.itemName);
        }

        shopGoldText.text = GameManager.instance.currentGold.ToString();
        buyGoldText.text = GameManager.instance.currentGold.ToString();
        sellGoldText.text = GameManager.instance.currentGold.ToString();

        ShowSellItems();

        sellDescriptionPanel.SetActive(false);
    }

}
