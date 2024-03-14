using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    RIFLE,
    SHOTGUN,
    GRENADE
}

public class Player : MonoBehaviour
{
    Timer shootTimer = new Timer();
    Timer reloadTimer = new Timer();
    const int clipSize = 30;
    int clip = clipSize;

    public GameObject projectilePrefab;
    Weapon weapon = null;

    float moveSpeed = 10.0f;    // Move at 10 units per second
    float turnSpeed = 360.0f;   // Turn at 360 degrees per seconds

    void Start()
    {
        // Shoot our weapon every 0.5 seconds
        shootTimer.total = 0.5f;
        reloadTimer.total = 2.0f;
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

        // Old approach where we spammed space to fire our weapon
        //if (Input.GetKeyDown(KeyCode.Space) && weapon != null)

        // Shoot if we have amo and have finished reloading, otherwise reload
        if (clip > 0 && reloadTimer.Expired())
        {
            shootTimer.Tick(dt);
            if (Input.GetKey(KeyCode.Space) && weapon != null && shootTimer.Expired())
            {
                shootTimer.Reset();
                weapon.Fire(transform.position + transform.right, transform.right);
                clip--;
            }

            // Ensure we reset our reload timer only once when we run out of amo
            if (clip <= 0)
            {
                clip = clipSize;
                reloadTimer.Reset();
            }
        }
        else
        {
            reloadTimer.Tick(dt);
        }
        Debug.Log(clip);

        transform.position += velocity * moveSpeed * dt;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
        if (collision.CompareTag("Rifle"))
        {
            weapon = new Rifle();
        }
        else if (collision.CompareTag("Shotgun"))
        {
            weapon = new Shotgun();
        }
        else if (collision.CompareTag("Grenade"))
        {
            weapon = new Grenade();
        }
        weapon.prefab = projectilePrefab;
    }
}
