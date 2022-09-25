using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{

    private Vector3 screenPoint;
    private Vector3 offset;
    private Vector3 oldMousePos;

    public Color defaultColor;
    public Color activeColor;
    public Color inactiveColor;
    public Color dragColor;

    public ButtonAction action;

    protected SpriteRenderer sp;
    private Rigidbody2D rb;

    public bool deleteInactive = true;
    [SerializeField]
    private bool _isEnabled;

    private int sound;

    public bool IsEnabled
    {
        get
        {
            return this._isEnabled;
        }
        set
        {
            this._isEnabled = value;
            if (_isEnabled)
            {
                sp.color = defaultColor;
            } else {
                sp.color = inactiveColor;
            }
        }
    }

// Start is called before the first frame update
    public virtual void Awake()
    {
        Debug.Log("lol");
        sp = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        GetComponentInChildren<textScript>().init();

        if (IsEnabled)
        {
            sp.color = defaultColor;
        }
        else
        {
            sp.color = inactiveColor;
        }
        sound = Random.Range(0, 3);
    }

    void Start()
    {
        //Debug.Log(isPlayerOnButton);
        GameManager.instance.CallStartBreak += changeKinematic;
        GameManager.instance.CallStopBreak += changeKinematic;
        GameManager.instance.CallStopBreak += DestroyIfInactive;
    }



    public virtual void OnMouseDown()
    {
        if (GameManager.instance.isInBreak && IsEnabled)
        {
            offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
            oldMousePos = Input.mousePosition;
            sp.color = activeColor;
            SoundManager.instance.PlayClick(sound);
        }
    }

    void OnMouseDrag()
    {
        
        if (GameManager.instance.isInBreak && IsEnabled)
        {
            Vector3 cursorScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorScreenPoint) + offset;
            gameObject.transform.position = cursorPosition;
            sp.color = dragColor;

        }
    }

    void OnMouseUp()
    {
        if (GameManager.instance.isInBreak && IsEnabled)
        {
            if (oldMousePos == Input.mousePosition)
            {
                pressButton();
                releaseButton();
                SoundManager.instance.PlayRelease(sound);
            }
            sp.color = defaultColor;

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player" && IsEnabled)
        {
            if(action == ButtonAction.END)
            {
                SoundManager.instance.PlayRelease(sound);
            }
            else
            {
                SoundManager.instance.PlayClick(sound);
            }

            pressButton();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && IsEnabled)
        {
            releaseButton();

            SoundManager.instance.PlayRelease(sound);
        }
    }

    public virtual void pressButton()
    {
        sp.color = activeColor;
        switch (this.action)
        {
            case ButtonAction.RESUME:
                MenuManager.instance.resumeGame();
                break;
            case ButtonAction.BREAK:
                MenuManager.instance.breakGame(false);
                Destroy(gameObject);
                break;
            case ButtonAction.RESTART:
                MenuManager.instance.restartLevel();
                break;
            case ButtonAction.EXIT:
                MenuManager.instance.exitLevel();
                break;
            case ButtonAction.END:
                MenuManager.instance.endLevel();
                break;
            case ButtonAction.SETTINGS:
                MenuManager.instance.OpenSettings(GetComponentInParent<CounterBoi>().counteroni);
                break;
            case ButtonAction.QUIT:
                MenuManager.instance.quitGame();
                break;
            case ButtonAction.LEVELS:
                MenuManager.instance.ShowLevels();
                break;
            case ButtonAction.DIFFICULTYEASY:
                MenuManager.instance.ChangeDifficulty(Difficulty.Easy);
                break;
            case ButtonAction.DIFFICULTYMEDIUM:
                MenuManager.instance.ChangeDifficulty(Difficulty.Medium);
                break;
            case ButtonAction.DIFFICULTYHARD:
                MenuManager.instance.ChangeDifficulty(Difficulty.Hard);
                break;
            case ButtonAction.COLOR:
                MenuManager.instance.changeColors();
                break;
            case ButtonAction.NEWGAME:
                MenuManager.instance.CreateNewSaveFile();
                break;
            default:
                Debug.Log("Not Implemented Yet");
                break;
        }
    }
    private void releaseButton()
    {
        sp.color = defaultColor;
        Destroy(gameObject);
    }


    public void changeKinematic()
    {
        Debug.Log("cinematicswitched");
        if (rb != null)
        {
            if (GameManager.instance.isInBreak)
            {
                rb.bodyType = RigidbodyType2D.Dynamic;
            }
            else
            {
                rb.bodyType = RigidbodyType2D.Kinematic;
            }
        }
        
    }

    private void DestroyIfInactive()
    {
        if (!IsEnabled && deleteInactive)
        {
            Destroy(gameObject);
            GameManager.instance.CallStopBreak -= DestroyIfInactive;
        }
    }

}

public enum ButtonAction
{
    RESUME,
    BREAK,
    RESTART,
    EXIT,
    COLOR,
    MUSIC,
    LANGUAGE,
    LEVELS,
    QUIT,
    SIZE,
    LOADLEVEL,
    DIFFICULTYEASY,
    DIFFICULTYMEDIUM,
    DIFFICULTYHARD,
    SETTINGS,
    END,
    INFO,
    NEWGAME
}