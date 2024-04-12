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
        // W = 1.0, S = -1.0, nothing = 0.0
        float moveSpeed = Input.GetAxis("Vertical");

        // Slow-motion
        bool slow = Input.GetKey(KeyCode.LeftControl);
        foreach (AnimationState playback in animation)
            playback.speed = slow ? 0.25f : 1.0f;

        // Determine state based on locomotion values & input
        type = Mathf.Abs(moveSpeed) <= Mathf.Epsilon ?
            AnimationType.IDLE : AnimationType.WALK;
        if (type == AnimationType.WALK && Input.GetKey(KeyCode.LeftShift))
            type = AnimationType.RUN;

        // Animate the player via state (cross-fade for simple blending)
        animation.clip = clips[(int)type];
        animation.CrossFade(animation.clip.name);

        // Locomote the player via state
        moveSpeed *= speeds[(int)type];
        if (Input.GetMouseButton(1))
        {
            float yaw = Input.GetAxis("Mouse X");
            float sensitivity = 5.0f;
            transform.Rotate(0.0f, yaw * sensitivity, 0.0f);
        }
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }
}
