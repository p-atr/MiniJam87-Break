using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelButton : ButtonController
{
    public int level;
    private GameObject infoButton;
    // Start is called before the first frame update
    public override void Awake()
    {
        base.Awake();
        infoButton = GameObject.Find("InfoButton");

        if (LevelManager.instance.progress[level] == '1'){
            IsEnabled = true; 
            sp.color = defaultColor;
        } else if(level == -1)
        {
            IsEnabled = true;
            sp.color = defaultColor;
        }
        
        else
        {
            IsEnabled = false;
            sp.color = inactiveColor;
        }

    }

    public void ResetButton()
    {
        Debug.Log("SAFASFFSFSAFAS");
        Debug.Log(LevelManager.instance.progress);
        if (LevelManager.instance.progress[level] == '1')
        {
            Debug.Log(level);
            IsEnabled = true;
            sp.color = defaultColor;
        }
        else
        {
            IsEnabled = false;
            sp.color = inactiveColor;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(infoButton != null && infoButton.GetComponent<infoButton>().levelButtonActive)
        {
            IsEnabled = true;
        }
    }

    public override void pressButton()
    {
        sp.color = activeColor;

        if(level == -1)
        {
            SceneManager.LoadScene(11);
        }
        else
        {
            SceneManager.LoadScene(level);
        }
    }
}
