using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource source;

    public void Init()
    {
        ActionManager.PlaySound += OnPlaySound;
    }

    public void DeInit()
    {
        ActionManager.PlaySound -= OnPlaySound;
    }

    private void OnPlaySound(AudioClip clip)
    {
        source.PlayOneShot(clip);
    }
}
