using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Main_Menu_Level : Level
{
    public GameObject titleText;

    static public GameObject levelButtonsParent;
    static private GameObject[] levelButtons;

    public float speed;

    void Awake()
    {
        levelButtons = new GameObject[0];
        titleText.GetComponent<textScript>().init();
        GameManager.instance.isInBreak = false;
    }

    //public static void getButtons()
    //{
    //    levelButtons = new GameObject[levelButtonsParent.transform.childCount];
    //    for (int i = 0; i < levelButtons.Length; i++)
    //    {
    //        levelButtons[i] = levelButtonsParent.transform.GetChild(i).gameObject;
    //        startPos = levelButtons[i].transform.position;
    //        endPos = new Vector2(levelButtons[i].transform.position.x + Random.Range(1, 3), levelButtons[i].transform.position.y);
    //    }
    //    Debug.Log(levelButtons.Length);
    //}

    //static Vector2 startPos, endPos;
    //protected override void Update()
    //{
    //    if(levelButtons.Length > 0)
    //    {
    //        foreach (GameObject levelButton in levelButtons)
    //        {
                
    //            if (Vector2.Distance(levelButton.transform.position, endPos) < 0.01)
    //            {
    //                startPos = levelButton.transform.position;
    //                endPos = new Vector2(levelButton.transform.position.x + Random.Range(1, 3), levelButton.transform.position.y);
    //            }
    //            else { levelButton.transform.position = Vector2.Lerp(startPos, endPos, speed); }
    //        }
    //    }
    //    base.Update();
    //}
}
