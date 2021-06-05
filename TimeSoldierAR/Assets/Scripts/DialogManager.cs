using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{

    public Text dialogText;
    public Text nameText;
    public GameObject dialogBox;
    public GameObject nameBox;

    public string[] dialogLines;

    public int currentLine;

    public static DialogManager instance;

    // Start is called before the first frame update
    void Start()
    {

        instance = this;
        dialogText.text = dialogLines[currentLine];

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Next()
    {
        if (dialogBox.activeInHierarchy)
        {
            //Debug.Log("clicked");
            currentLine++;

            if(currentLine >= dialogLines.Length-1)
            {
                dialogText.text = dialogLines[currentLine];
                GuideManager.instance.exitButton.SetActive(true);
                GuideManager.instance.nextButton.SetActive(false);
            }
            else
            {
                dialogText.text = dialogLines[currentLine];
            }
            
        }
    }


}
