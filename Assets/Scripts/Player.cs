using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Rigidbody2D rb;
    float moveSpeed = 10.0f;    // Move at 10 units per second
    float turnSpeed = 360.0f;   // Turn at 360 degrees per second

    void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
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

        // A quaternion represents a rotation.
        // A vector represents a direction.
        // We get our direction vector by taking whatever direction we want a rotation of 0 to be (Vector3.right in this case),
        // and multiply it by our transfor's rotation which is a quaternion.
        Vector3 direction = transform.rotation * Vector3.right;

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

        transform.position += velocity * moveSpeed * dt;
    }
}
