using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DidCollide : MonoBehaviour
{



    public GameObject uiCanvas;

    private void OnTriggerEnter(Collider other)
    {
        if (uiCanvas.activeInHierarchy)
        {
            return;
        }
        else
        { 
            if (other.tag == "Fairy")
            {
                Debug.Log(other.tag);
                FairyManager.instance.OpenFairy();
            }
            else if (other.tag == "Shop")
            {
                Debug.Log(other.tag);
                Shop.instance.OpenShop();
            }
            else if (other.tag == "Guide")
            {
                Debug.Log(other.tag);
                GuideManager.instance.OpenGuide();
            }
            else
            {
                Debug.Log(other.tag);
                BattleManager.instance.StartBattle(other.tag);
            }
        }
    }
}
