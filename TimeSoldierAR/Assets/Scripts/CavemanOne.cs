using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CavemanOne : MonoBehaviour
{
    public GameObject mainCanvas, CavemanOnePanel;

    public static CavemanOne instance;

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
        AudioManager.instance.PlayBGM(9);
        mainCanvas.SetActive(true);
        CavemanOnePanel.SetActive(true);
    }

    public void CloseGuide()
    {
        AudioManager.instance.StopMusic();
        AudioManager.instance.PlayBGM(7);
        mainCanvas.SetActive(false);
        CavemanOnePanel.SetActive(false);
        GameManager.instance.arCanvas.SetActive(true);
    }
}
