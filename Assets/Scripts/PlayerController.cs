using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum AnimationType
    {
        IDLE,
        WALK,
        RUN,
        JUMP,
        COUNT
    }

    Animation animation;
    public AnimationType type = AnimationType.IDLE;
    AnimationClip[] clips = new AnimationClip[(int)AnimationType.COUNT];

    void Start()
    {
        animation = GetComponent<Animation>();
        clips[(int)AnimationType.IDLE] = animation.GetClip("Idle");
        clips[(int)AnimationType.WALK] = animation.GetClip("Walking");
        clips[(int)AnimationType.RUN] = animation.GetClip("Fast Run");
        clips[(int)AnimationType.JUMP] = animation.GetClip("Jump");
    }

    void Update()
    {
        animation.clip = clips[(int)type];
        animation.Play();
    }
}
