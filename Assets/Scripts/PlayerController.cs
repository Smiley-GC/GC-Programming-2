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

    float moveSpeed = 10.0f;
    float turnSpeed = 250.0f;

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

        transform.rotation *= Quaternion.Euler(0.0f, rotation * turnSpeed * dt, 0.0f);
        //transform.Rotate(0.0f, rotation * turnSpeed * dt, 0.0f); <-- same as above

        transform.position += transform.forward * translation * moveSpeed * dt;
        //transform.Translate(Vector3.forward * translation * moveSpeed * dt, Space.World); <-- same as above

        type = translation == 0.0f ? AnimationType.IDLE : AnimationType.WALK;

        animation.clip = clips[(int)type];
        animation.Play();
    }
}
