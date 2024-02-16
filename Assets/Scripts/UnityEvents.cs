using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
// Type the above otherwise we have to write "UnityEngine.Events.UnityEvent onPlayerDeath;"

public class PlayerDeathEvent : EventArgs
{
    public Vector3 deathPosition = Vector3.zero;
}

public class UnityEvents : MonoBehaviour
{
    UnityEvent<PlayerDeathEvent> onPlayerDeath = new UnityEvent<PlayerDeathEvent>();
    int deathCount = 0;
    Vector3 spawn = Vector3.right * -5.0f;

    void Start()
    {
        // Subscribe to the "player death" topic
        onPlayerDeath.AddListener(PlayerDeathCounter);
        onPlayerDeath.AddListener(PlayerRespawn);
    }

    void Update()
    {
        transform.position += Vector3.right * Time.deltaTime;

        // Dispatch player death event when space is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerDeathEvent evt = new PlayerDeathEvent();
            evt.deathPosition = transform.position;
            onPlayerDeath.Invoke(evt);
        }
    }

    void PlayerDeathCounter(PlayerDeathEvent evt)
    {
        deathCount++;
        Debug.Log("You've died " + deathCount + " times.");
        if (deathCount == 5)
        {
            Debug.Log("Achievemnt unlock: die 5 times xD xD xD xD xD");
        }
    }

    void PlayerRespawn(PlayerDeathEvent evt)
    {
        Debug.Log("Player died at location " + evt.deathPosition);
        Debug.Log("Respawning player at location " + spawn);
        transform.position = spawn;
    }
}
