using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    List<int> numbers = new List<int>();

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

    float[] speeds = new float[(int)AnimationType.COUNT];
    float turnSpeed = 250.0f;

    void Start()
    {
        animation = GetComponent<Animation>();
        clips[(int)AnimationType.IDLE] = animation.GetClip("Idle");
        clips[(int)AnimationType.WALK] = animation.GetClip("Walking");
        clips[(int)AnimationType.RUN] = animation.GetClip("Fast Run");
        clips[(int)AnimationType.JUMP] = animation.GetClip("Jump");

        speeds[0] = 0.0f;
        speeds[1] = 5.0f;
        speeds[2] = 10.0f;
        speeds[3] = 69.420f;    // <-- jumping unused
    }

    void Update()
    {
        // 1. Update Input
        float dt = Time.deltaTime;
        float translation = 0.0f;
        float rotation = 0.0f;
        if (Input.GetKey(KeyCode.A))
        {
            rotation = -1.0f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            rotation = 1.0f;
        }
        if (Input.GetKey(KeyCode.W))
        {
            translation = 1.0f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            translation = -1.0f;
        }

        // 2. Update animation
        type = translation == 0.0f ? AnimationType.IDLE : AnimationType.WALK;
        if (type == AnimationType.WALK && Input.GetKey(KeyCode.LeftShift))
            type = AnimationType.RUN;

        float moveSpeed = speeds[(int)type];
        animation.clip = clips[(int)type];
        animation.Play();

        // 3. Update locomotion
        transform.rotation *= Quaternion.Euler(0.0f, rotation * turnSpeed * dt, 0.0f);
        transform.position += transform.forward * translation * moveSpeed * dt;

        //transform.Rotate(0.0f, rotation * turnSpeed * dt, 0.0f); <-- same as above
        //transform.Translate(Vector3.forward * translation * moveSpeed * dt, Space.World); <-- same as above
    }
}

// TODO 1: -- add state to prevent animation from defaulting to idle or walk
//if (Input.GetKey(KeyCode.Space))
//    type = AnimationType.JUMP;

// TODO 2: Add blending (state machine needed)
