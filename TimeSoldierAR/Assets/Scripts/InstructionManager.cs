using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionManager : MonoBehaviour
{
    public GameObject instructionPanel;

    public static InstructionManager instance;


    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        instructionPanel.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void CloseInstruction()
    {
        instructionPanel.SetActive(false);
        GameManager.instance.arCanvas.SetActive(true);
    }
}
