using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : ShootingSystem
{
    internal static PlayerShooting instance;

    public Blaster blaster;
    public Blaster blasterLeft;
    public Blaster blasterRight;

    [Header("Shooting Color")]
    public Material blasterMaterial;
    public Material projectileMaterial;
    public Material sideBlasterMaterial;
    public Material sideProjectileMaterial;

    [Header("Additional Blasters")]
    public GameObject leftBlasterObject;
    public GameObject rightBlasterObject;

    [Header("Projectile Object")]
    [SerializeField] GameObject projectileObject;
    [SerializeField] GameObject sideProjectileObject;

    [Header("Projectile Origins")]
    [SerializeField] Transform centralProjectileOrigin;
    [SerializeField] Transform leftProjectileOrigin;
    [SerializeField] Transform rightProjectileOrigin;

    private void Awake()
    {
        instance = this;

        blaster = new Blaster(50, false, 300, 20);
        InitBlasterColor();

        blasterLeft = new Blaster(50, false, 300, 20);
        blasterRight = new Blaster(50, false, 300, 20);
    }

    void Update()
    {
        if (!blaster.isAutomatic)
        {
            if (!UpgradeShopHUD.instance.isOpened && !PauseMenuController.gamePaused && blaster.canShoot && Input.GetMouseButtonDown(0))
            {
                PlayerShoot();
            }
        }
        else
        {
            if (!UpgradeShopHUD.instance.isOpened && !PauseMenuController.gamePaused && blaster.canShoot && Input.GetMouseButton(0))
            {
                PlayerShoot();
            }
        }
    }

    public void InitBlasterColor()
    {
        MaterialColorChanger.SetMaterialColor(blasterMaterial, Color.black, new Color(0.5f, 1f, 0f));
        MaterialColorChanger.SetMaterialColor(sideBlasterMaterial, Color.yellow, Color.yellow);
        MaterialColorChanger.SetMaterialColor(projectileMaterial, Color.white, Color.white);
        MaterialColorChanger.SetMaterialColor(sideProjectileMaterial, Color.cyan, Color.cyan);
    }

    void PlayerShoot()
    {
        if (blaster.fireRate > 300f || blaster.damage > 50)
        {
            AudioManager.instance.PlaySFX(AudioManager.SoundEffects.upgradedBlaster);
        }
        else
        {
            AudioManager.instance.PlaySFX(AudioManager.SoundEffects.defaultBlaster);
        }

        Shoot(projectileObject, centralProjectileOrigin, blaster);

        if (leftBlasterObject.activeSelf)
        {
            Shoot(sideProjectileObject, leftProjectileOrigin, blasterLeft);
            Shoot(sideProjectileObject, rightProjectileOrigin, blasterRight);
        }
    }
}