using System.Collections;
using UnityEngine;

public class ShootingSystem : MonoBehaviour
{
    public void Shoot(GameObject projObject, Transform projOrigin, Blaster shootingBlaster)
    {
        if (shootingBlaster.canShoot)
        {
            GameObject projectile_clone = Instantiate(projObject, projOrigin.position, projOrigin.rotation);
            projectile_clone.GetComponent<Rigidbody>().AddRelativeForce(shootingBlaster.projectileSpeed * Vector3.up, ForceMode.Impulse);

            StartCoroutine(LoadNextShot(shootingBlaster)) ;
        }
    }

    IEnumerator LoadNextShot(Blaster enumBlaster) {
        enumBlaster.canShoot = false;
        yield return new WaitForSeconds(60 / enumBlaster.fireRate);
        enumBlaster.canShoot = true;
    }
}
