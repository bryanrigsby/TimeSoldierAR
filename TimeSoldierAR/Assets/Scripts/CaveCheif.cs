using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveCheif : MonoBehaviour
{
    public GameObject mainCanvas, CaveCheifPanel;

    public static CaveCheif instance;

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
        CaveCheifPanel.SetActive(true);
    }

    public void CloseGuide()
    {
        AudioManager.instance.StopMusic();
        AudioManager.instance.PlayBGM(7);
        mainCanvas.SetActive(false);
        CaveCheifPanel.SetActive(false);
        GameManager.instance.arCanvas.SetActive(true);
    }
}
