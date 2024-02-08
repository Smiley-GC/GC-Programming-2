using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Events : MonoBehaviour
{
    struct Player
    {
        // Offload behaviour to different parts of our program
        // (ie our AudioManager should handle audio. Not the Player)
        public PlayerDamageHandler damageAchievement;
        public PlayerDamageHandler damageLogic;
        public PlayerDamageHandler damageAudio;

        // All player stores its its necessary data
        // It might not even need this data depending on its delegates!
        public string achievementText;
        public float health;
        public AudioClip clip;
    }

    delegate void PlayerDamageHandler(Player player);

    void DamagePlayer(Player player)
    {
        player.damageAchievement(player);
        player.damageLogic(player);
        player.damageAudio(player);
    }

    Player player = new Player();

    // Start is called before the first frame update
    void Start()
    {
        player.achievementText = "Congradulations, you just took damage!";
        player.health = 100.0f;

        player.damageAchievement = OnDamageAcheivement;
        player.damageLogic = OnDamageLogic;
        player.damageAudio = OnDamageAudio;

        DamagePlayer(player);
    }

    void OnDamageAcheivement(Player player)
    {
        Debug.Log(player.achievementText);
    }

    void OnDamageLogic(Player player)
    {
        player.health -= 10.0f;
        Debug.Log(player.health);
    }

    void OnDamageAudio(Player player)
    {
        Debug.Log("*Pain noises*");
    }
}
