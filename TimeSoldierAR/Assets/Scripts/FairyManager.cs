using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FairyManager : MonoBehaviour
{

    public GameObject mainCanvas, fairyPanel;

    public Text hpSliderText, mpSliderText, fairyText;
    public Slider hpSlider, mpSlider;

    public PlayerStats player;

    public static FairyManager instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    

    public void OpenFairy()
    {
        fairyText.text = "Be Healed!";

        AudioManager.instance.ButtonSFX();
        AudioManager.instance.PlayBGM(10);

        mainCanvas.SetActive(true);
        fairyPanel.SetActive(true);

        hpSliderText.text = Mathf.Clamp(player.currentHp, 0, int.MaxValue) + " / " + player.maxHp;
        mpSliderText.text = Mathf.Clamp(player.currentMp, 0, int.MaxValue) + " / " + player.maxMp;
        hpSlider.value = Mathf.Clamp(player.currentHp, 0, int.MaxValue);
        hpSlider.maxValue = player.maxHp;
        mpSlider.value = Mathf.Clamp(player.currentMp, 0, int.MaxValue);
        mpSlider.maxValue = player.maxMp;

        player.currentHp = player.maxHp;
        player.currentMp = player.maxMp;

        StartCoroutine(PauseCo());

        

    }

    public IEnumerator PauseCo()
    {
        
        yield return new WaitForSeconds(1f);
        fairyText.text = "";
        yield return new WaitForSeconds(0.5f);
        updateStats();
    }

    public void updateStats()
    {
        hpSliderText.text = Mathf.Clamp(player.currentHp, 0, int.MaxValue) + " / " + player.maxHp;
        mpSliderText.text = Mathf.Clamp(player.currentMp, 0, int.MaxValue) + " / " + player.maxMp;
        hpSlider.value = Mathf.Clamp(player.currentHp, 0, int.MaxValue);
        hpSlider.maxValue = player.maxHp;
        mpSlider.value = Mathf.Clamp(player.currentMp, 0, int.MaxValue);
        mpSlider.maxValue = player.maxMp;
    }

    public void Back()
    {
        AudioManager.instance.StopMusic();
        AudioManager.instance.PlayBGM(7);
        mainCanvas.SetActive(false);
        fairyPanel.SetActive(false);
    }
}
