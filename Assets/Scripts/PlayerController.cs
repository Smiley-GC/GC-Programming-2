using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    enum AnimationType
    {
        IDLE,
        WALK,
        RUN,
        JUMP,
        COUNT
    }

    Animation animation;
    AnimationType type = AnimationType.IDLE;
    AnimationClip[] clips = new AnimationClip[(int)AnimationType.COUNT];

    void Start()
    {
        animation = GetComponent<Animation>();
        clips[(int)AnimationType.IDLE] = animation.GetClip("Idle");
        clips[(int)AnimationType.WALK] = animation.GetClip("Walking");
        clips[(int)AnimationType.RUN] = animation.GetClip("Fast Run");
        clips[(int)AnimationType.JUMP] = animation.GetClip("Jump");
        animation.clip = clips[(int)AnimationType.IDLE];
    }

    void Update()
    {
        animation.Play();
    }
}
