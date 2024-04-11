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
    float turnSpeed = 250.0f;
    float moveSpeed = 5.0f;

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
        transform.Rotate(0.0f, rotation * dt, 0.0f);
        transform.Translate(transform.forward * translation * dt);
        animation.Play();
    }
}
