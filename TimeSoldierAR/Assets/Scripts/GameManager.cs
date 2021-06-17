using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{


    public GameObject arCanvas, uiCanvas, orientationPanel, arCanvasTextGO, loadingPanel;

    public float arTextTimer = 1f;
    private bool arTextTimerBool;

    public Text arCanvasText;

    public PlayerStats playerStats;

    public string[] itemsHeld;
    public int[] numberOfItems;
    public Item[] referenceItems;

    public int currentGold;
    //public float timeBetweenBattles = 10f;
    //public float betweenBattleCounter;
    public float loadingPanelTimer = 3.0f;
    private bool loadingPanelTimerBool;

    public bool continueGame = false; 
    public static GameManager instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        if(continueGame == true)
        {
            arCanvas.SetActive(true);
            uiCanvas.SetActive(false);
        }
        else
        {
            arCanvas.SetActive(false);
            uiCanvas.SetActive(true);
            orientationPanel.SetActive(true);
        }


        arTextTimerBool = false;
        loadingPanelTimerBool = true;

    }

    // Update is called once per frame
    void Update()
    {

        if (arTextTimerBool)
        {
            arTextTimer -= Time.deltaTime;
            //Debug.Log(timer);
            if (arTextTimer <= 0)
            {
                arCanvasText.text = "";
                arCanvasTextGO.SetActive(false);
                arTextTimerBool = false;
                arTextTimer = 1f;
            }
        }

        if (loadingPanelTimerBool)
        {
            loadingPanelTimer -= Time.deltaTime;
            Debug.Log(loadingPanelTimer);
            if (loadingPanelTimer <= 0)
            {
                loadingPanel.SetActive(false);
                loadingPanelTimerBool = false;
                loadingPanelTimer = 3f;
            }
        }
    }



    public void SaveGame()
    {
        AudioManager.instance.ButtonSFX();
        AudioManager.instance.PlaySFX(20);
        Debug.Log("Save clicked");
        arCanvasText.text = "Saved Successfully!";
        arCanvasTextGO.SetActive(true);
        SaveData();
        arTextTimerBool = true;
        //QuestManager.instance.SaveQuestData(); //for mini quests
    }


    public Item GetItemDetails(string itemToGrab)
    {
        for(int i = 0; i < referenceItems.Length; i++)
        {
            if(referenceItems[i].itemName == itemToGrab)
            {
                return referenceItems[i];
            }
        }

        return null;
    }

    public void SortItems()
    {
        bool itemAfterSpace = true;

        while (itemAfterSpace)
        {
            itemAfterSpace = false;

            for (int i = 0; i < itemsHeld.Length - 1; i++)
            {
                if (itemsHeld[i] == "")
                {
                    itemsHeld[i] = itemsHeld[i + 1];
                    itemsHeld[i + 1] = "";

                    numberOfItems[i] = numberOfItems[i + 1];
                    numberOfItems[i + 1] = 0;

                    if(itemsHeld[i] != "")
                    {
                        itemAfterSpace = true;
                    }
                }
            }
        }

        
    }


    public void AddItem(string itemToAdd)
    {
        int newItemPosition = 0;
        bool foundSpace = false;

        for(int i = 0; i < itemsHeld.Length; i++)
        {
            if(itemsHeld[i] == "" || itemsHeld[i] == itemToAdd)
            {
                newItemPosition = i;
                i = itemsHeld.Length;
                foundSpace = true;
            }
        }

        if (foundSpace)
        {
            bool itemExists = false;

            for(int i = 0; i< referenceItems.Length; i++)
            {
                if(referenceItems[i].itemName == itemToAdd)
                {
                    itemExists = true;
                    i = referenceItems.Length;
                }
            }

            if (itemExists)
            {
                itemsHeld[newItemPosition] = itemToAdd;
                numberOfItems[newItemPosition]++;
            }
            else
            {
                Debug.LogError(itemToAdd + " Does not exist!");
            }
        }

        GameMenu.instance.ShowItems();
    }

    public void RemoveItem(string itemToRemove)
    {
        bool foundItem = false;
        int itemPosition = 0;

        for(int i = 0; i < itemsHeld.Length; i++)
        {
            if (itemsHeld[i] == itemToRemove)
            {
                foundItem = true;
                itemPosition = i;
            }
        }

        if (foundItem)
        {
            numberOfItems[itemPosition]--;
            if(numberOfItems[itemPosition] <= 0)
            {
                itemsHeld[itemPosition] = "";
            }

            GameMenu.instance.ShowItems();
        }
        else
        {
            Debug.LogError("Couldn't find " + itemToRemove);
        }
    }


    public void SaveData()
    {
        PlayerPrefs.SetString("Current_Scene", SceneManager.GetActiveScene().name); //if decide to use scenes
        //PlayerPrefs.SetFloat("Player_Position_x", PlayerController.instance.transform.position.x); //if decide to use 2d player for mini quests
        //PlayerPrefs.SetFloat("Player_Position_x", PlayerController.instance.transform.position.y);
        //PlayerPrefs.SetFloat("Player_Position_x", PlayerController.instance.transform.position.z);

        //save character info
        PlayerPrefs.SetInt("Player_Level", playerStats.playerLevel);
        PlayerPrefs.SetInt("Player_CurrentExp", playerStats.currentExp);
        PlayerPrefs.SetInt("Player_CurrentHp", playerStats.currentHp);
        PlayerPrefs.SetInt("Player_CurrentMp", playerStats.currentMp);
        PlayerPrefs.SetInt("Player_MaxHp", playerStats.maxHp);
        PlayerPrefs.SetInt("Player_MaxMp", playerStats.maxMp);
        PlayerPrefs.SetInt("Player_Strength", playerStats.strength);
        PlayerPrefs.SetInt("Player_Defense", playerStats.defense);
        PlayerPrefs.SetInt("Player_WpnPower", playerStats.wpnPower);
        PlayerPrefs.SetInt("Player_ArmrPower", playerStats.armrPower);
        PlayerPrefs.SetString("Player_EquippedWpn", playerStats.equippedWpn);
        PlayerPrefs.SetString("Player_EquippedArmr", playerStats.equippedArmr);
        PlayerPrefs.SetInt("Player_Gold", GameManager.instance.currentGold);

        //store inventory data
        for(int i = 0; i < itemsHeld.Length; i++)
        {
            PlayerPrefs.SetString("ItemInInventory_" + i, itemsHeld[i]);
            PlayerPrefs.SetInt("ItemAmount_" + i, numberOfItems[i]);
        }


    }

    public void LoadData()
    {
        //PlayerController.instance.transform.position = new Vector3(PlayerPrefs.GetFloat("Player_Position_x"), PlayerPrefs.GetFloat("Player_Position_y"), PlayerPrefs.GetFloat("Player_Position_z")); //if decide to use 2d player for mini quests

        //get character info
        playerStats.playerLevel = PlayerPrefs.GetInt("Player_Level");
        playerStats.currentExp = PlayerPrefs.GetInt("Player_CurrentExp");
        playerStats.currentHp = PlayerPrefs.GetInt("Player_CurrentHp");
        playerStats.currentMp = PlayerPrefs.GetInt("Player_CurrentMp");
        playerStats.maxHp = PlayerPrefs.GetInt("Player_MaxHp");
        playerStats.maxMp = PlayerPrefs.GetInt("Player_MaxMp");
        playerStats.strength = PlayerPrefs.GetInt("Player_Strength");
        playerStats.defense = PlayerPrefs.GetInt("Player_Defense");
        playerStats.wpnPower = PlayerPrefs.GetInt("Player_WpnPower");
        playerStats.armrPower = PlayerPrefs.GetInt("Player_ArmrPower");
        playerStats.equippedWpn = PlayerPrefs.GetString("Player_EquippedWpn");
        playerStats.equippedArmr = PlayerPrefs.GetString("Player_EquippedArmr");
        GameManager.instance.currentGold = PlayerPrefs.GetInt("Player_Gold");


        //load inventory data
        for (int i = 0; i < itemsHeld.Length; i++)
        {
            itemsHeld[i] = PlayerPrefs.GetString("ItemInInventory_" + i);
            numberOfItems[i] = PlayerPrefs.GetInt("ItemAmount_" + i);
        }
    }


    public void QuitGame()
    {
        Application.Quit();
    }


}
