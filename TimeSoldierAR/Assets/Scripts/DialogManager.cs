using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{

    public Text dialogText;
    public GameObject dialogBox;
    public GameObject exitButton;
    public GameObject nextButton;

    public string[] dialogLines;

    public int currentLine;

    public static DialogManager instance;

    // Start is called before the first frame update
    void Start()
    {

        instance = this;
        dialogText.text = dialogLines[currentLine];

        exitButton.SetActive(false);
        nextButton.SetActive(true);

        Debug.Log(currentLine);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Next()
    {
        if (dialogBox.activeInHierarchy)
        {
            Debug.Log(currentLine);
            currentLine++;

            if(currentLine >= dialogLines.Length-1)
            {
                dialogText.text = dialogLines[currentLine];
                exitButton.SetActive(true);
                nextButton.SetActive(false);
            }
            else
            {
                dialogText.text = dialogLines[currentLine];
            }
            
        }
    }


}
