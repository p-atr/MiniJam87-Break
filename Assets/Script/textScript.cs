using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class textScript : MonoBehaviour
{
    private string targetString;
    public float timePerChar;

    private TextMesh textObj;
    private float lastTime = Mathf.Infinity;
    int i;
    public bool hasStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        textObj = GetComponent<TextMesh>();
        targetString = textObj.text;
        textObj.text = "";
        GetComponent<MeshRenderer>().sortingLayerName = "Buttons";
        GetComponent<MeshRenderer>().sortingOrder = 1;
    }

    // Update is called once per frame
    public void init()
    {
        if (!hasStarted) {
            i = 0;
            lastTime = Time.time;
            hasStarted = true;
        }
        
    }

    void Update()
    {
        if (hasStarted) {
            if (Time.time >= lastTime + timePerChar)
            {
                if (i <= targetString.Length)
                {
                    textObj.text = targetString.Substring(0, i);
                    i++;

                    lastTime = Time.time;
                }
                else
                {
                    lastTime = Mathf.Infinity;
                }

            }
        }
        
    }
}
