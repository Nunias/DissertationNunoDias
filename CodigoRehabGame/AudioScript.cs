using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour
{
    public AudioSource src;
    public AudioClip sfwrong;
    public AudioClip sfgood;

    public void wrongSound()
    {
        src.clip = sfwrong;
        src.Play();
    }

    public void GoodSound()
    {
        src.clip = sfgood;
        src.Play();
    }
}
