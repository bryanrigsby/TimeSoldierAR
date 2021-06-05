using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideManager : MonoBehaviour
{

    public GameObject mainCanvas, guidePanel;

    
    public GameObject exitButton;
    public GameObject nextButton;

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

        exitButton.SetActive(false);
        nextButton.SetActive(true);

        DialogManager.instance.currentLine = 0;
        DialogManager.instance.dialogText.text = DialogManager.instance.dialogLines[0];
    }

    public void CloseGuide()
    {
        AudioManager.instance.StopMusic();
        AudioManager.instance.PlayBGM(7);
        mainCanvas.SetActive(false);
        guidePanel.SetActive(false);

    }
}
