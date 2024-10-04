using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance;
    private AudioSource _audioSource; 

    private void OnEnable()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this); 
        }
        _audioSource = GetComponent<AudioSource>(); 
    }

    public void PlaySound(AudioClip clip)
    {
        _audioSource.PlayOneShot(clip); 
    }
}
