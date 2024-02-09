using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Events : MonoBehaviour
{
    public class Entity
    {
        public float health;
        public string damageSound;

        public DamageHander damageAchievement;
        public DamageHander damageLogic;
        public DamageHander damageAudio;
    }

    public delegate void DamageHander(Entity entity);

    Entity player = new Entity();
    Entity enemy = new Entity();

    void Damage(Entity entity)
    {
        if (entity.damageAchievement != null)
            entity.damageAchievement(entity);

        if (entity.damageLogic != null)
            entity.damageLogic(entity);

        if (entity.damageAudio != null)
            entity.damageAudio(entity);
    }

    // Start is called before the first frame update
    void Start()
    {
        player.health = 100.0f;
        enemy.health = 50.0f;
        player.damageLogic = DecreaseHealth;
        enemy.damageLogic = DecreaseHealth;

        player.damageSound = "Owwwwwww";
        enemy.damageSound = "Oooooooof";
        player.damageAudio = PlayAudio;
        enemy.damageAudio = PlayAudio;

        player.damageAchievement = UnlockAchievement;
        enemy.damageAchievement = null;

        Damage(player);
        Damage(enemy);
    }

    void UnlockAchievement(Entity entity)
    {
        Debug.Log("Achievement unlocked: PAIN");
    }

    void DecreaseHealth(Entity entity)
    {
        entity.health -= 10.0f;
        Debug.Log("Damage taken. Health: " + entity.health);
    }

    void PlayAudio(Entity entity)
    {
        Debug.Log(entity.damageSound);
    }
}
