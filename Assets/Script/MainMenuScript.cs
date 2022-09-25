using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : Level
{
    public GameObject titleText;
    // Start is called before the first frame update
    void Awake()
    {
        titleText.GetComponent<textScript>().init();

    }
}
