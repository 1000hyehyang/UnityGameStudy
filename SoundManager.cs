using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource[] bgmSources;
    public AudioSource[] sfxSources;

    public void SetBGMVolume(float volume)
    {
        foreach (var source in bgmSources)
        {
            source.volume = volume;
        }
    }

    public void SetSfxVolume(float volume)
    {
        foreach (var source in bgmSources)
        {
            source.volume = volume;
        }
    }
}
