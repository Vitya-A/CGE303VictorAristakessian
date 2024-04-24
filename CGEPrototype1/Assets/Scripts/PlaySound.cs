using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Require the AudioSource component to be attached to whatever GameObject this script this is attached to
[RequireComponent(typeof(AudioSource))]

public class PlaySound : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip soundToPlay;
    
    // Add a slider in Unity to allow changing the volume
    [Range(0, 1)]
    public float volume = 1f;
    
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        audioSource.PlayOneShot(soundToPlay, volume);
    }
}
