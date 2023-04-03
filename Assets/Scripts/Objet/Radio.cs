using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class Radio : Electronics
{
    private AudioSource audioSource = null;

    public override void Awake()
    {
        base.Awake();
        audioSource = GetComponent<AudioSource>();
    }

    public override void Use()
    {
        if (!GetIsPowerOn()) return;

        if (audioSource.isPlaying)
        {
            audioSource.Play();
            Debug.Log(productName + " Play");
        }
        else 
        { 
            audioSource.Stop();
            Debug.Log(productName + " Stop");
        }
    }


}
