using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum WeaponType
{
    RIFLE,
    SHOTGUN,
    GRENADE
}

public class Player : MonoBehaviour
{
    public GameObject projectilePrefab;
    Weapon weapon = new Shotgun();

    const int clipSize = 10;
    int clip = clipSize;

    Timer shootCooldown = new Timer();
    Timer reloadCooldown = new Timer();

    float moveSpeed = 10.0f;    // Move at 10 units per second
    float turnSpeed = 360.0f;   // Turn at 360 degrees per seconds
    public float health = 100.0f;

    void Start()
    {
        weapon.owner = Owner.PLAYER;
        weapon.prefab = projectilePrefab;

        // Shoot every half-second
        shootCooldown.total = 0.5f;
        reloadCooldown.total = 2.0f;
    }

    void Update()
    {
        float dt = Time.deltaTime;

        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(0.0f, 0.0f, -turnSpeed * dt);
        }

        else if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(0.0f, 0.0f, turnSpeed * dt);
        }

        Debug.DrawLine(transform.position, transform.position + transform.right * 10.0f);

        Vector3 velocity = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            velocity += transform.right;
        }
        
        else if (Input.GetKey(KeyCode.S))
        {
            velocity -= transform.right;
        }
        
        if (Input.GetKey(KeyCode.A))
        {
            velocity += transform.up;
        }
        
        else if (Input.GetKey(KeyCode.D))
        {
            velocity -= transform.up;
        }

        shootCooldown.Tick(dt);
        // Shoot if space is pressed, we have a weapon, we're off cooldown, and we have amo!
        if (Input.GetKey(KeyCode.Space) && weapon != null && shootCooldown.Expired() && reloadCooldown.Expired())
        {
            shootCooldown.Reset();
            weapon.Fire(transform.position, transform.right);
            clip--;
            //Debug.Log(clip);

            // Check if we've shot our last bullet, begin reloading if so!
            if (clip <= 0)
            {
                reloadCooldown.Reset();
                clip = clipSize;
                Debug.Log("Reloading...");
            }
        }
        else
        {
            reloadCooldown.Tick(dt);
        }

        transform.position += velocity * moveSpeed * dt;
    }
}
