using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CavemanThree : MonoBehaviour
{
    public GameObject mainCanvas, CavemanThreePanel;

    public static CavemanThree instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenGuide()
    {
        AudioManager.instance.ButtonSFX();
        AudioManager.instance.PlayBGM(4);
        mainCanvas.SetActive(true);
        CavemanThreePanel.SetActive(true);
    }

    public void CloseGuide()
    {
        //AudioManager.instance.StopMusic();
        //AudioManager.instance.PlayBGM(7);
        mainCanvas.SetActive(false);
        CavemanThreePanel.SetActive(false);
        GameManager.instance.arCanvas.SetActive(true);
    }
}
