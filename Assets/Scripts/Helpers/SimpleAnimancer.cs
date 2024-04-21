using Animancer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAnimancer : MonoBehaviour
{
    [Header("Animator Settings")]
    [SerializeField] private AnimancerComponent animancer;
    [SerializeField] private float fadeDuration = 0.1f;
    [SerializeField] private AnimationClip[] clips;
    [SerializeField] private bool playDefault = false;

    private AnimancerState currentState;

    private void Start()
    {
        if (playDefault)
        {
            PlayAnimation(clips[0]);
        }
    }

    public void Stop()
    {
        //_animancer.Stop();
        animancer.States.Current.Stop();
        animancer.States.Current.Weight = 1;
    }

    public void MakeRandomKeyframe()
    {
        currentState.NormalizedTime = Random.Range(0f, 1f);
    }

    public void PlayAnimation(string clipName)
    {
        AnimationClip clip = GetAnimationClipByName(clipName);
        if (animancer != null && clip != null)
        {
            currentState = animancer.Play(clip, fadeDuration);
        }
    }

    public void PlayAnimation(AnimationClip clip)
    {
        if (animancer != null && clip != null)
        {
            currentState = animancer.Play(clip, fadeDuration);
        }
    }

    public void PlayMixer(LinearMixerTransition transition, float speed)
    {
        currentState = animancer.Play(transition, fadeDuration);
    }

    public void SetStateSpeed(float speed)
    {
        if (currentState == null)
        {
            return;
        }
        currentState.Speed = speed;
    }

    AnimationClip GetAnimationClipByName(string clipName)
    {
        for (int i = 0; i < clips.Length; i++)
        {
            if (clips[i].name.Equals(clipName))
            {
                return clips[i];
            }
        }
        return null;
    }

    public Transform GetAnimatorTransform()
    {
        return animancer.transform;
    }

    public float GetRemainingDuration()
    {
        return currentState.RemainingDuration;
    }

    public float GetDuration()
    {
        return currentState.Duration;
    }
}
