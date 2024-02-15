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
    float current = 0.0f;
    float total = 0.25f;

    public GameObject projectilePrefab;
    Weapon weapon = null;

    float moveSpeed = 10.0f;    // Move at 10 units per second
    float turnSpeed = 360.0f;   // Turn at 360 degrees per seconds

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

        // New approach where we fire our weapon based on a timer
        current += dt;
        bool canFire = current >= total;
        if (Input.GetKey(KeyCode.Space) && weapon != null && canFire)
        {
            weapon.Fire(transform.position + transform.right, transform.right);
            current = 0.0f;
        }

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
