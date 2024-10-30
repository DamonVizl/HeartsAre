using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance;
    private AudioSource _audioSource;

    [SerializeField] private SO_SFX[] _audioClips;
    private Dictionary<SFXName, SO_SFX> _sfxDictionary; 

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
        CreateSFXDictionary(); 
    }
    private void CreateSFXDictionary()
    {
        _sfxDictionary = new Dictionary<SFXName, SO_SFX>();
        foreach(SO_SFX clip in _audioClips)
        {
            if (System.Enum.TryParse(clip.name, out SFXName name))
            {
                _sfxDictionary[name] = clip;
            }
            else Debug.LogWarning($"Clip name {clip.name} does not match an enum value ");
        }
    }
    public void PlayFirstSound(SFXName clipName)
    {
        PlaySound(_sfxDictionary[clipName].GetFirstAudioClip());
    }
    public void PlayRandomSound(SFXName clipName)
    {
        PlaySound(_sfxDictionary[clipName].GetRandomAudioClip());
    }
    public void PlaySoundAtIndex(SFXName clipName, int index) 
    {
        PlaySound(_sfxDictionary[clipName].GetClipAtIndex(index));
    }
    private void PlaySound(AudioClip clip)
    {
        if(_audioSource.isPlaying)
        {
            if(clip == _audioSource.clip)
            {
                //if the clip is being played, play any futher ones of the same clip quieter
                _audioSource.PlayOneShot(clip, 0.75f); 
            }    
        }
        else _audioSource.PlayOneShot(clip);
    }
}
public enum SFXName
{
    CardFlip,
    CardSelect,
    CardDeselect,
    CardDraw,
    DealCards,
    DefenderDefends,
    DefenderKilled,
    DestroyDefender,
    Discard,
    EnemyDoesDamage,
    LevelUpDefender,
    PlayerLose,
    PlayerTakeDamage,
    PlayerWin,
    PurchaseFailed,
    PurchaseSucceeded,
    SelectDefender,
    Shuffle,
    SpecialCardPlayed,
    TrickFailed,
    TrickSucceeded,
}
