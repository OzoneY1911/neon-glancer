using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blaster
{
    public int damage;
    public bool isAutomatic;
    public float fireRate;
    public int projectileSpeed;

    public bool canShoot = true;

    public Blaster(int dmg, bool isAuto, float rate, int projSpeed)
    {
        damage = dmg;
        isAutomatic = isAuto;
        fireRate = rate;
        projectileSpeed = projSpeed;
    }
}
