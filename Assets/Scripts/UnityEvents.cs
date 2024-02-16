using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
// Type the above otherwise we have to write "UnityEngine.Events.UnityEvent onPlayerDeath;"

public class UnityEvents : MonoBehaviour
{
    UnityEvent onPlayerDeath = new UnityEvent();
    int deathCount = 0;
    Vector3 spawn = Vector3.right * -5.0f;

    // Start is called before the first frame update
    void Start()
    {
        // Subscribe to the "player death" topic
        onPlayerDeath.AddListener(PlayerDeathCounter);
        onPlayerDeath.AddListener(PlayerRespawn);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.right * Time.deltaTime;

        // Dispatch player death event when space is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            onPlayerDeath.Invoke();
        }
    }

    void PlayerDeathCounter()
    {
        deathCount++;
        Debug.Log("You've died " + deathCount + " times.");
    }

    void PlayerRespawn()
    {
        Debug.Log("Respawning player at location " + spawn);
        transform.position = spawn;
    }
}
