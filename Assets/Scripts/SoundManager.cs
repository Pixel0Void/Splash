using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    private AudioSource m_AudioSource;

    public AudioClip CompleteLevelAudio;

    private void Awake()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

    public void LevelCompleted()
    {
        PlayAudio(CompleteLevelAudio);
    }

    private void PlayAudio(AudioClip audioClip)
    {
        m_AudioSource.clip = audioClip;
        m_AudioSource.Play();
    }
}
