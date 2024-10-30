using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SFX", menuName = "Audio/SFX")]
public class SO_SFX : ScriptableObject
{
    [SerializeField] AudioClip[] _clips; 
    /// <summary>
    /// returns the first or only audio clip in the array
    /// </summary>
    /// <returns></returns>
    public AudioClip GetFirstAudioClip()
    {
        if (_clips == null) return null;
        return _clips[0];
    }
    /// <summary>
    /// returns a random audio clip from the array (for audio variance)
    /// </summary>
    /// <returns></returns>
    public AudioClip GetRandomAudioClip()
    {
        if (_clips == null) return null;
        int randomIndex = Random.Range(0, _clips.Length);
        return _clips[randomIndex];
    }
    /// <summary>
    /// return audio clip at index passed
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public AudioClip GetClipAtIndex(int index)
    {
        if (_clips == null) return null;
        if (index < _clips.Length)
        {
            return _clips[index];
        }
        else return _clips[0]; //return default at 0
    }
}
