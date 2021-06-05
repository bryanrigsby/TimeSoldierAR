using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadIntroSceneChOne : MonoBehaviour
{
    void OnEnable()
    {
        SceneManager.LoadScene("IntroScene", LoadSceneMode.Single);
    }
}
