using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    [HideInInspector] public int noOfRestarts = 0;
    public int breaksLeft;
    public int allowedBreaks;

    public Text breaksLeftText;

    public bool isMenu;
    public int MenuType;

    public int levelNumber;

    protected virtual void Update()
    {
        if (!isMenu)
        {
            if(breaksLeft > 0)
            {
                breaksLeftText.text = "Breaks left: " + breaksLeft.ToString();
            }
            else
            {

                breaksLeftText.text = "Press R to Restart";
            }
        }
    }
}
