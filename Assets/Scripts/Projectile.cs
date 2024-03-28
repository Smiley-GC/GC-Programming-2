using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Owner
{
    NONE,
    PLAYER,
    ENEMY
}

public class Projectile : MonoBehaviour
{
    public Owner owner = Owner.NONE;

    public GameObject particlePrefab;
    public WeaponType weaponType;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && owner != Owner.PLAYER)
        {
            Player player = collision.GetComponent<Player>();
            player.health -= 25.0f;
            if (player.health <= 0.0f)
                Debug.LogWarning("Player died");
        }

        if (collision.name == "HurtBox" && owner != Owner.ENEMY)
        {
            Enemy enemy = collision.GetComponentInParent<Enemy>();
            enemy.health -= 25.0f;
            if (enemy.health <= 0.0f)
                 Debug.LogWarning("Enemy died");

            Destroy(gameObject);
        }
    }

    // Explode if grenade
    void OnDestroy()
    {
        switch (weaponType)
        {
            case WeaponType.RIFLE:
            {

            }
            break;

            case WeaponType.SHOTGUN:
            {

            }
            break;

            case WeaponType.GRENADE:
            {
                float step = 360.0f / 8.0f;
                for (int i = 0; i < 8; i++)
                {
                    float rotation = step * i;
                    GameObject particle = Instantiate(particlePrefab);
                    particle.transform.position = transform.position;
                    particle.transform.Rotate(new Vector3(0.0f, 0.0f, rotation));
                    particle.GetComponent<Rigidbody2D>().velocity = particle.transform.right * 5.0f;
                    Destroy(particle, 0.5f);
                }
            }
            break;
        }
    }
}
