using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Level_4 : Level
{
    public Rigidbody2D[] dynamicButtons;
    // Start is called before the first frame update
    void Awake()
    {
    }

    void setDynamic()
    {
        foreach (Rigidbody2D dynamicButton in dynamicButtons)
        {
            dynamicButton.bodyType = RigidbodyType2D.Dynamic;
        }
    }
    // Update is called once per frame
    protected override void Update()
    {
        setDynamic();
        base.Update();
    }
}
