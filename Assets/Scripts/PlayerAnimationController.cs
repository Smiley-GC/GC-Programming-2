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

    AnimationType type = AnimationType.IDLE;

    Animation animation;
    List<AnimationClip> clips = new List<AnimationClip>();
    List<float> speeds = new List<float>();
    float turnSpeed = 250.0f;

    // Optional homework 1: add a mechanism to prevent animation-switching while the player is jumping.
    // Optional homework 2: implement a way to change animation speed that uses our AnimationType enum instead of strings.
    void Start()
    {
        animation = GetComponent<Animation>();
        clips.Add(animation.GetClip("Idle"));
        clips.Add(animation.GetClip("Walking"));
        clips.Add(animation.GetClip("Fast Run"));
        clips.Add(animation.GetClip("Jump"));

        speeds.Add(0.0f);
        speeds.Add(5.0f);
        speeds.Add(10.0f);
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

        // Determine state based on locomotion values & input
        type = translation == 0.0f ? AnimationType.IDLE : AnimationType.WALK;
        if (type == AnimationType.WALK && Input.GetKey(KeyCode.LeftShift))
            type = AnimationType.RUN;

        // Animate the player via state
        animation.clip = clips[(int)type];
        animation.Play();

        // Locomote the player via state
        translation *= speeds[(int)type];
        transform.rotation *= Quaternion.Euler(0.0f, rotation * dt, 0.0f);
        transform.position += transform.forward * translation * dt;
    }
}
