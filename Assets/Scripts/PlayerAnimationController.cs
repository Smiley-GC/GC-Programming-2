using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    // An enum is a more "correct" solution, but C# doesn't like casting enums to integers (for array access)...
    //const int IDLE = 0;
    //const int WALK = 1;
    //const int RUN = 2;
    //const int JUMP = 3;
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
    float moveSpeed = 5.0f;

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
        speeds.Add(69.0f);  // jump height?
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

        type = translation == 0.0f ? AnimationType.IDLE : AnimationType.WALK;
        if (type == AnimationType.WALK && Input.GetKey(KeyCode.LeftShift))
            type = AnimationType.RUN;

        animation.clip = clips[(int)type];
        translation *= speeds[(int)type];

        // Manual locomotion (involves understanding vectors & quaternions
        transform.rotation *= Quaternion.Euler(0.0f, rotation * dt, 0.0f);
        transform.position += transform.forward * translation * dt;

        // Automatic locomotion (doesn't involve anything xD)
        //transform.Rotate(0.0f, rotation * dt, 0.0f);
        //transform.Translate(Vector3.forward * translation * dt);
        //transform.Translate(transform.forward * translation * dt, Space.World);
        animation.Play();
    }
}
