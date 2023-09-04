using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : ShootingSystem
{
    Blaster blaster;

    [Header("Projectile")]
    [SerializeField] GameObject projectileObject;
    public Transform projectileOrigin;

    void Awake()
    {
        blaster = new Blaster(10, true, 150, 20);
    }

    public void EnemyShoot()
    {
        if (blaster.canShoot)
        {
            switch ((int)Random.Range(0, 2))
            {
                case (0):
                    AudioManager.instance.PlaySFX(AudioManager.SoundEffects.enemyBlaster1);
                    break;
                case (1):
                    AudioManager.instance.PlaySFX(AudioManager.SoundEffects.enemyBlaster2);
                    break;
                case (2):
                    AudioManager.instance.PlaySFX(AudioManager.SoundEffects.enemyBlaster3);
                    break;
            }
        }

        Shoot(projectileObject, projectileOrigin, blaster);
    }
}
