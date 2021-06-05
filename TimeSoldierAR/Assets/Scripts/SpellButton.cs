using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellButton : MonoBehaviour
{

    public Text spellName;
    public int buttonValue;
    public Text costText;
    public Image spellImage;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Press()
    {

        BattleManager.instance.SelectSpell(PlayerStats.instance.GetSpellDetails(PlayerStats.instance.spells[buttonValue]));
            

    }
}
