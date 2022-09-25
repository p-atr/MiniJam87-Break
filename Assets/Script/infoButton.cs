using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class infoButton : MonoBehaviour
{
    public GameObject infoText;

    public bool levelButtonActive;
    // Start is called before the first frame update
    void Awake()
    {
        GetComponentInChildren<textScript>().init();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            infoText.GetComponent<textScript>().init();
            levelButtonActive = true;
        }
    }
}
