using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollision : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Walls"))
        {
            Destroy(gameObject);
        }
        else if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyStats>().TakeDamage(PlayerShooting.instance.blaster.damage);
            Destroy(gameObject);
        }
        else if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerStats>().TakeDamage(10);
            HUDController.instance.UpdateHealthBar();
            Destroy(gameObject);
        }
    }
}