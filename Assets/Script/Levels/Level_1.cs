using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_1 : Level
{
    public GameObject tutorialText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (noOfRestarts >= 2) { tutorialText.GetComponent<textScript>().init(); }
    }
}
