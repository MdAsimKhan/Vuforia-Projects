using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneAudioSource : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioSource exception;
    void Start()
    {
        // Get the AudioSource component attached to this game object
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayAudio()
    {
        // Stop any other AudioSources that are currently playing
        AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource otherAudioSource in allAudioSources)
        {
            if (otherAudioSource != audioSource && otherAudioSource != exception && otherAudioSource.isPlaying)
            {
                otherAudioSource.Stop();
            }
        }
        // Play the current AudioSource
        audioSource.Play();
    }
}
