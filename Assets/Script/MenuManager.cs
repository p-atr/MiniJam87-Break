using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum Difficulty
{
    Easy, Medium, Hard
}

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance { get; private set; }
    public GameObject breakMenuSettings;
    public GameObject breakMenuNoSettings;
    public GameObject breakMenuExit;

    public GameObject LevelMenu;
    public GameObject SettingsMenu;

    private List<GameObject> breakMenuIns = new List<GameObject>();
    private List<GameObject> settingsMenuIns = new List<GameObject>();
    private GameObject levelMenuIns;

    public Color Orange;
    public Color White;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (GameManager.instance.isInBreak)
            {
                resumeGame();
            }
            else
            {
                if (GameManager.instance.currentLevel.breaksLeft > 0)
                {
                    breakGame(true);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.R) && GameManager.instance.currentLevel.breaksLeft <= 0)
        {
            restartLevelComplete();
        }
    }

    public void resumeGame()
    {
        GameManager.instance.isInBreak = false;
    }

    public void breakGame(bool minus)
    {
        GameManager.instance.isInBreak = true;
        if (minus)
        {
            GameManager.instance.currentLevel.breaksLeft--;
        }

        switch (GameManager.instance.currentLevel.MenuType)
        {
            case 0:
                breakMenuIns.Add(Instantiate(breakMenuSettings, GameManager.instance.Player.transform.position + Vector3.up * 4.3f, Quaternion.identity));
                break;
            case 1:
                breakMenuIns.Add(Instantiate(breakMenuNoSettings, GameManager.instance.Player.transform.position + Vector3.up * 4.3f, Quaternion.identity));
                break;
            case 2:
                breakMenuIns.Add(Instantiate(breakMenuExit, GameManager.instance.Player.transform.position + Vector3.up * 4.3f, Quaternion.identity));
                break;
            default:
                Debug.LogError("No valid Menu");
                break;
        }
       
        breakMenuIns[breakMenuIns.Count-1].transform.parent = GameManager.instance.buttonParent.transform;
        breakMenuIns[breakMenuIns.Count - 1].GetComponentInParent<CounterBoi>().counteroni = breakMenuIns.Count - 1;
    }

    public void restartLevel()
    {
        GameManager.instance.restartLevel();
    }

    public void restartLevelComplete()
    {

        GameManager.instance.ResetLevel();
    }

    public void OpenSettings(int index)
    {
        settingsMenuIns.Add(Instantiate(SettingsMenu, breakMenuIns[index].transform.position, Quaternion.identity));
        settingsMenuIns[settingsMenuIns.Count-1].transform.parent = GameManager.instance.buttonParent.transform;
        Destroy(breakMenuIns[index]);
    }

    private bool reversed;
    private List<Collider2D> whiteGO;
    private List<Collider2D> orangeGO;

    private List<Text> texts;
    public void changeColors()
    {
        if (reversed)
        {
            GameManager.instance.Player.transform.GetComponentInChildren<SpriteRenderer>().color = White;
            Camera.main.backgroundColor = Orange;
            reversed = false;

            foreach (Text text in texts)
            {
                text.color = White;
                text.GetComponent<Outline>().effectColor = Orange;
            }
        }
        else 
        {
            GameManager.instance.Player.transform.GetComponentInChildren<SpriteRenderer>().color = Orange;
            Camera.main.backgroundColor = White;
            reversed = true;


            foreach (Text text in texts)
            {
                text.color = Orange;
                text.GetComponent<Outline>().effectColor = White;
            }
        }

        foreach (Collider2D go in whiteGO)
        {
            go.enabled = !reversed;
        }
        foreach (Collider2D go in orangeGO)
        {
            go.enabled = reversed;
        }
    }

    public void toggleMusic()
    {
        //TODO
        //SceneManager.LoadScene("MainGame");
    }

    public void exitLevel()
    {
        SceneManager.LoadScene("Main_Menu");
    }

    public void endLevel()
    {
        LevelManager.instance.unlockLvl(GameManager.instance.currentLevel.levelNumber + 1);
        SceneManager.LoadScene("Main_Menu");
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public Transform spawnPoint;
    public void ShowLevels()
    {
        levelMenuIns = Instantiate(LevelMenu, new Vector3(spawnPoint.position.x, spawnPoint.position.y, 0), Quaternion.identity);
    }

    public void ChangeDifficulty(Difficulty difficulty)
    {
        GameObject[] gos = GameObject.FindObjectsOfType(typeof(GameObject)) as GameObject[];
        foreach (GameObject go in easyGO)
        {
            go.SetActive(difficulty == Difficulty.Easy);
        }
        foreach (GameObject go in mediumGO)
        {
            go.SetActive(difficulty == Difficulty.Medium);
        }
        foreach (GameObject go in hardGO)
        {
            go.SetActive(difficulty == Difficulty.Hard);
        }
    }

    List<GameObject> easyGO;
    List<GameObject> mediumGO;
    List<GameObject> hardGO;

    public void Initialize(bool isMenu)
    {
        reversed = false;
        if (!isMenu)
        {
            easyGO = new List<GameObject>();
            mediumGO = new List<GameObject>();
            hardGO = new List<GameObject>();
            whiteGO = new List<Collider2D>();
            orangeGO = new List<Collider2D>();

            GameObject[] gos = GameObject.FindObjectsOfType(typeof(GameObject)) as GameObject[];

            foreach (GameObject go in gos)
            {
                if (go.tag == "Easy")
                {
                    easyGO.Add(go);
                    go.SetActive(false);
                }
                else if (go.tag == "Medium")
                {
                    mediumGO.Add(go);
                }
                else if (go.tag == "Hard")
                {
                    hardGO.Add(go);
                    go.SetActive(false);
                }


                if (go.tag == "White")
                {
                    whiteGO.Add(go.GetComponent<Collider2D>());
                }
                else if (go.tag == "Orange")
                {
                    orangeGO.Add(go.GetComponent<Collider2D>());
                    go.GetComponent<Collider2D>().enabled = false;
                }
            }

            texts = GameManager.instance.CanvasIns.GetComponent<UITextContainer>().texts;
        }
    }

    public void CreateNewSaveFile()
    {
        LevelManager.instance.ResetSave();
        if(levelMenuIns != null)
        {
            for (int i = 0; i < levelMenuIns.transform.childCount; i++)
            {
                levelMenuIns.transform.GetChild(i).GetComponent<LevelButton>().ResetButton();
            }
        }
    }
}
