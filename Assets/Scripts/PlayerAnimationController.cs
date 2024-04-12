using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    enum AnimationType
    {
        IDLE,
        WALK,
        RUN,
        JUMP
    }

    Animation animation;
    AnimationType type = AnimationType.IDLE;
    List<AnimationClip> clips = new List<AnimationClip>();
    List<float> speeds = new List<float>();
    float turnSpeed = 250.0f;

    void Start()
    {
        animation = GetComponent<Animation>();
        clips.Add(animation.GetClip("Idle"));
        clips.Add(animation.GetClip("Walking"));
        clips.Add(animation.GetClip("Fast Run"));
        clips.Add(animation.GetClip("Jump"));

        speeds.Add(0.0f);
        speeds.Add(5.0f);
        speeds.Add(15.0f);
        speeds.Add(69.0f);  // jump not in use currently
        animation.clip = clips[(int)AnimationType.IDLE];
    }

    void Update()
    {
        float dt = Time.deltaTime;
        float rotation = 0.0f;
        float translation = 0.0f;
        if (Input.GetKey(KeyCode.D))
        {
            rotation += turnSpeed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            rotation -= turnSpeed;
        }
        if (Input.GetKey(KeyCode.W))
        {
            translation += 1.0f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            translation -= 1.0f;
        }

        // Slow-motion
        bool slow = Input.GetKey(KeyCode.LeftControl);
        foreach (AnimationState playback in animation)
            playback.speed = slow ? 0.25f : 1.0f;

        // Determine state based on locomotion values & input
        type = Mathf.Abs(translation) <= Mathf.Epsilon ?
            AnimationType.IDLE : AnimationType.WALK;
        if (type == AnimationType.WALK && Input.GetKey(KeyCode.LeftShift))
            type = AnimationType.RUN;

        // Animate the player via state (cross-fade for simple blending)
        animation.clip = clips[(int)type];
        animation.CrossFade(animation.clip.name);

        // Locomote the player via state
        translation *= speeds[(int)type];
        transform.rotation *= Quaternion.Euler(0.0f, rotation * dt, 0.0f);
        transform.position += transform.forward * translation * dt;
    }
}
