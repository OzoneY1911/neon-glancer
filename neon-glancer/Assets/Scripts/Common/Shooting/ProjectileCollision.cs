using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollision : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(DelayedDestroy());
    }

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
    }

    IEnumerator DelayedDestroy()
    {
        yield return new WaitForSeconds(10f);

        Destroy(gameObject);
    }
}
