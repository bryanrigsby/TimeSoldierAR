using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleSceneManager : MonoBehaviour
{
    public GameObject continueButton, mainMenuCanvas, loadingCanvas;

    public float timer = 3f;
    private bool timerBool;

    public static TitleSceneManager instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        timerBool = false;

        mainMenuCanvas.SetActive(true);
        loadingCanvas.SetActive(false);

        if (PlayerPrefs.HasKey("Current_Scene"))
        {
            continueButton.SetActive(true);
        }
        else
        {
            continueButton.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (timerBool)
        {
            timer -= Time.deltaTime;
            //Debug.Log(timer);
            if (timer <= 0)
            {
                mainMenuCanvas.SetActive(false);                
                AudioManager.instance.StopMusic();
                if (PlayerPrefs.HasKey("Current_Scene"))
                {
                    GameManager.instance.continueGame = true;
                    AudioManager.instance.PlayBGM(4);
                    SceneManager.LoadScene("MainScene");
                }
                else
                {
                    SceneManager.LoadScene("IntroScene");
                }
                timerBool = false;
            }
        }
    }


    public void Continue()
    {
        Debug.Log("continue clicked clicked");
        AudioManager.instance.ButtonSFX();
        AudioManager.instance.StopMusic();
        GameManager.instance.LoadData();
        mainMenuCanvas.SetActive(false);
        loadingCanvas.SetActive(true);
        timerBool = true;
    }

    public void NewGame()
    {
        Debug.Log("new game clicked");
        AudioManager.instance.ButtonSFX();
        AudioManager.instance.StopMusic();
        mainMenuCanvas.SetActive(false);
        loadingCanvas.SetActive(true);
        timerBool = true;
    }






}
