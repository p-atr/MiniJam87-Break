using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance { get; private set; }
    public string progress;

    public static char[] Reverse(string s)
    {
        char[] charArray = s.ToCharArray();
        return charArray;
    }

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

    // Start is called before the first frame update
    void Start()
    {
        LevelButton[] btns = GetComponentsInChildren<LevelButton>();
        if (PlayerPrefs.HasKey("progress")) {
            progress = PlayerPrefs.GetString("progress");
        }
        else {
            progress = "110000000000000000";
            PlayerPrefs.SetString("progress", progress);
            PlayerPrefs.Save();
        }
    }

    public void unlockLvl(int lvl) {
        char[] prgrss = Reverse(progress);
        prgrss[lvl] = '1';

        PlayerPrefs.SetString("progress", new string(prgrss));
        PlayerPrefs.Save();

        Start();
    }

    public void unlockNextLvl(){
        char[] prgrss = Reverse(progress);

        for (int i=0; i<prgrss.Length; i++) {
            if (prgrss[i] == '0') {
                prgrss[i] = '1';
                break;
            }
        }

        PlayerPrefs.SetString("progress", new string(prgrss));
        PlayerPrefs.Save();

        Start();
    }

    public void ResetSave()
    {
        PlayerPrefs.DeleteKey("progress");
        progress = "110000000000000000";
        PlayerPrefs.SetString("progress", progress);
        PlayerPrefs.Save();
    }
}
