using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    [SerializeField]
    private bool _isPaused;

    public delegate void StartBreak();

    public StartBreak CallStartBreak;

    public delegate void StopBreak();

    public StopBreak CallStopBreak;

    public GameObject playerPrefab;

    public GameObject buttonParent;

    public GameObject Player;

    public GameObject Canvas;
    public GameObject CanvasIns;



    public Level currentLevel;
    public bool isInBreak
    {
        get
        {
            return this._isPaused;
        }
        set
        {
            this._isPaused = value;
            if (value == true && CallStartBreak != null) { CallStartBreak(); Debug.Log("Calling Break"); }
            else if (CallStopBreak != null) { CallStopBreak();}
            Debug.Log("set to" + _isPaused);
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
            return;
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;

    }

    public void restartLevel()
    {
        if (isInBreak)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            Player.transform.position = new Vector3(-6.44000006f, -0.879999995f, 0);
            Player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            currentLevel.noOfRestarts++;
        }
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Initialize();
    }

    private void OnSceneUnloaded(Scene scene)
    {
        CallStartBreak = null;
        CallStopBreak = null;
    }

    private void Initialize()
    {
        currentLevel = FindObjectOfType<Level>();

        if (currentLevel.isMenu)
        {
            _isPaused = true;
        }
        else
        {
            Player = Instantiate(playerPrefab);
            Camera.main.GetComponent<PlayerFollower>().SetPlayer(Player);
            _isPaused = false;
            CanvasIns = Instantiate(Canvas);
            currentLevel.breaksLeftText = CanvasIns.transform.GetChild(1).GetComponent<Text>();

            buttonParent = new GameObject("buttonContainer");
        }

        MenuManager.instance.Initialize(currentLevel.isMenu);
    }

}
