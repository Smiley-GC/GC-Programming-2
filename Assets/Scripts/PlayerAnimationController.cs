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
    float turnSpeed = 250.0f;
    float moveSpeed = 5.0f;

    void Start()
    {
        animation = GetComponent<Animation>();
        clips.Add(animation.GetClip("Idle"));
        clips.Add(animation.GetClip("Walking"));
        clips.Add(animation.GetClip("Fast Run"));
        clips.Add(animation.GetClip("Jump"));
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
            translation += moveSpeed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            translation -= moveSpeed;
        }

        type = translation == 0.0f ? AnimationType.IDLE : AnimationType.WALK;
        animation.clip = clips[(int)type];

        // Locomotion test
        //float tt = Time.realtimeSinceStartup;
        //transform.rotation = Quaternion.Euler(0.0f, 100.0f * tt, 0.0f);
        //transform.Translate(transform.forward * moveSpeed * dt);

        transform.Rotate(0.0f, rotation * dt, 0.0f);
        transform.Translate(transform.forward * translation * dt);
        animation.Play();
    }
}
