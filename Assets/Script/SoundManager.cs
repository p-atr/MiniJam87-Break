using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{


    public static SoundManager instance { get; private set; }


    public List<AudioClip> click;
    public List<AudioClip> release;


    public AudioSource source;

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

        source = GetComponent<AudioSource>();
    }



    public void PlayClick(int i)
    {
        source.PlayOneShot(click[i], 0.8f);
    }

    public void PlayRelease(int i)
    {
        source.PlayOneShot(release[i], 0.8f);
    }
}
