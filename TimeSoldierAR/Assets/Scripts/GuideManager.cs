using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideManager : MonoBehaviour
{

    public GameObject mainCanvas, guidePanel, orientationGuidePanel;

    


    public static GuideManager instance;


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
        guidePanel.SetActive(true);
    }

    public void CloseGuide()
    {
        //AudioManager.instance.StopMusic();
        //AudioManager.instance.PlayBGM(7);
        mainCanvas.SetActive(false);
        guidePanel.SetActive(false);
        GameManager.instance.arCanvas.SetActive(true);
    }

    public void CloseOrientationGuide()
    {
        //AudioManager.instance.StopMusic();
        //AudioManager.instance.PlayBGM(7);
        mainCanvas.SetActive(false);
        orientationGuidePanel.SetActive(false);
        GameManager.instance.arCanvas.SetActive(true);
    }
}
