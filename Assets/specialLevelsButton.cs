using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class specialLevelsButton : ButtonController
{

    //public GameObject levelscript;
    public GameObject levelsMenuButton;
    // Start is called before the first frame update

    public override void pressButton()
    {
        sp.color = activeColor;

        Main_Menu_Level.levelButtonsParent = Instantiate(levelsMenuButton, gameObject.transform.position, Quaternion.identity);
        //Main_Menu_Level.getButtons();


    }

    public override void OnMouseDown() { }

}
