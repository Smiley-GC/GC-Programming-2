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
    public GameObject projectilePrefab;
    WeaponType weaponType = WeaponType.RIFLE;

    float moveSpeed = 10.0f;    // Move at 10 units per second
    float turnSpeed = 360.0f;   // Turn at 360 degrees per seconds
    float projectileSpeed = 5.0f;

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

        // A quaternion represents a rotation.
        // A vector represents a direction.
        // We get our direction vector by taking whatever direction we want a rotation of 0 to be (Vector3.right in this case),
        // and multiply it by our transfor's rotation which is a quaternion.
        Vector3 direction = transform.right;//transform.rotation * Vector3.right;

        // We can use trigonometry to convert directions to angles and angles to directions
        // (This is unnecessary for the actual implementation, just review)
        //float angle = Mathf.Atan2(direction.y, direction.x);
        //direction = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle));
        //Debug.Log(angle * Mathf.Rad2Deg);
        Debug.DrawLine(transform.position, transform.position + direction * 10.0f);

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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            switch (weaponType)
            {
                case WeaponType.RIFLE:
                    CreateRifle(direction, projectileSpeed);
                    break;

                case WeaponType.SHOTGUN:
                    CreateShotgun(direction, projectileSpeed);
                    break;

                case WeaponType.GRENADE:
                    CreateGrenade(direction, projectileSpeed);
                    break;
            }
        }

        transform.position += velocity * moveSpeed * dt;
    }

    // Homework 1 hint: take each weapon-specific function and add it to each weapon-specific class
    // Ensure you use polymorphism by making the base Weapon class have a virtual Fire() method,
    // and overriding it with weapon-specific functionality!
    GameObject CreateProjectile(Vector3 direction, float speed, Color color)
    {
        GameObject projectile = Instantiate(projectilePrefab);
        projectile.transform.position = transform.position + direction;
        projectile.GetComponent<Rigidbody2D>().velocity = direction * speed;
        projectile.GetComponent<SpriteRenderer>().color = color;
        return projectile;
    }

    void CreateRifle(Vector3 direction, float speed)
    {
        CreateProjectile(direction, speed, Color.red);
    }

    void CreateShotgun(Vector3 direction, float speed)
    {
        CreateProjectile(direction, speed, Color.green);
        CreateProjectile(Quaternion.Euler(0.0f, 0.0f,  30.0f) * direction, speed, Color.green);
        CreateProjectile(Quaternion.Euler(0.0f, 0.0f, -30.0f) * direction, speed, Color.green);
    }

    void CreateGrenade(Vector3 direction, float speed)
    {
        GameObject explosion = CreateProjectile(direction, speed, Color.blue);
        explosion.GetComponent<Explosion>().weaponType = weaponType;
        Destroy(explosion, 1.0f);
    }

    // Homework 1 hint: change weapon in here (weaponType is no longer necessary)
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
        if (collision.CompareTag("Rifle"))
        {
            weaponType = WeaponType.RIFLE;
        }
        else if (collision.CompareTag("Shotgun"))
        {
            weaponType = WeaponType.SHOTGUN;
        }
        else if (collision.CompareTag("Grenade"))
        {
            weaponType = WeaponType.GRENADE;
        }
    }
}
