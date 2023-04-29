using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundtest : MonoBehaviour
{
    public AudioClip clip;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            PlaySound();
        }
        
    }

    private void PlaySound()
    {
        SoundManager.Instance.Play(clip);
    }
}
