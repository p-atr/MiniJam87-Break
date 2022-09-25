using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Level_10 : Level
{
    public GameObject tutorialText;
    // Start is called before the first frame update
    void Awake()
    {
        tutorialText.GetComponent<textScript>().init();
    }
}
