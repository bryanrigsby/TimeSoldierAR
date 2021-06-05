using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMainSceneChOne : MonoBehaviour
{
    void OnEnable()
    {
        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
    }
}
