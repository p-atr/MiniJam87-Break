using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class breakText : MonoBehaviour
{
    public float blinkSpeed;
    Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponentInChildren<Text>();
    }

    float brightness = 1f;

    void Update()
    {
        text.enabled = GameManager.instance.isInBreak;

        text.color = new Color(text.color.r, text.color.g, text.color.b, ((Mathf.Sin(Time.time * blinkSpeed))+brightness));
    }
}
