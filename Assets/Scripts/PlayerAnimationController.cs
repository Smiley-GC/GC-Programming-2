using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    // An enum is a more "correct" solution, but C# doesn't like casting enums to integers (for array access)...
    const int IDLE = 0;
    const int WALK = 1;
    const int RUN = 2;
    const int JUMP = 3;

    Animation animation;
    List<AnimationClip> clips = new List<AnimationClip>();


    void Start()
    {
        animation = GetComponent<Animation>();
        clips.Add(animation.GetClip("Idle"));
        clips.Add(animation.GetClip("Walking"));
        clips.Add(animation.GetClip("Fast Run"));
        clips.Add(animation.GetClip("Jump"));
        animation.clip = clips[IDLE];
    }

    void Update()
    {
        animation.Play();
    }
}
