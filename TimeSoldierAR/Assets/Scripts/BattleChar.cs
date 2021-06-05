using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleChar : MonoBehaviour
{

    public bool isEnemy;
    public string charName;
    public Sprite charImage;
    public int currentHp, maxHp, strength, defense;
    public int gold;
    public int exp;
}
