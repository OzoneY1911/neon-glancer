using UnityEngine;
using System;
using UnityEngine.UI;
using System.Collections.Generic;

public class UpgradeController : MonoBehaviour
{
    internal static UpgradeController instance;

    [SerializeField] ShopHUD upgradeShopHUD;

    [SerializeField] List<Text> upgradePriceText;
    [SerializeField] List<int> upgradePrice;
    [SerializeField] List<Button> shopButton;

    void Awake()
    {
        instance = this;

        for (int i = 0; i < upgradePrice.Count; i++)
        {
            upgradePriceText[i].text = upgradePrice[i].ToString();
        }
    }

    public void BuyUpgrade(UpgradeChoice upgrade)
    {
        switch (upgrade.upgradeName)
        {
            case Upgrade.AutomaticMode:
                if (upgradeShopHUD.CheckPrice(upgradePrice[0]))
                {
                    AudioManager.instance.PlaySFX(AudioManager.SoundEffects.upgradeAuto);
                    PlayerShooting.instance.blaster.isAutomatic = true;
                    shopButton[0].interactable = false;
                }
                break;
            case Upgrade.AgilityImplant:
                if (upgradeShopHUD.CheckPrice(upgradePrice[1]))
                {
                    AudioManager.instance.PlaySFX(AudioManager.SoundEffects.upgradeMovement);
                    PlayerMovement.instance.movementSpeed *= 1.25f;
                    shopButton[1].interactable = false;
                }
                break;
            case Upgrade.TripleBlaster:
                if (upgradeShopHUD.CheckPrice(upgradePrice[2]))
                {
                    AudioManager.instance.PlaySFX(AudioManager.SoundEffects.upgradeSideBlasters);
                    PlayerShooting.instance.leftBlasterObject.SetActive(true);
                    PlayerShooting.instance.rightBlasterObject.SetActive(true);
                    shopButton[2].interactable = false;
                }
                break;
            case Upgrade.BlasterDamage:
                if (upgradeShopHUD.CheckPrice(upgradePrice[3]))
                {
                    AudioManager.instance.PlaySFX(AudioManager.SoundEffects.upgradeDamage);
                    PlayerShooting.instance.blaster.damage *= 2;
                    MaterialColorChanger.SetMaterialColor(PlayerShooting.instance.projectileMaterial, Color.cyan);
                    MaterialColorChanger.SetMaterialColor(PlayerShooting.instance.sideProjectileMaterial, new Color(1f, 0f, 0.7f));
                    shopButton[3].interactable = false;
                }
                break;
            case Upgrade.FireRate:
                if (upgradeShopHUD.CheckPrice(upgradePrice[4]))
                {
                    AudioManager.instance.PlaySFX(AudioManager.SoundEffects.upgradeRate);
                    PlayerShooting.instance.blaster.fireRate *= 2.5f;
                    PlayerShooting.instance.blasterLeft.fireRate *= 2.5f;
                    PlayerShooting.instance.blasterRight.fireRate *= 2.5f;
                    MaterialColorChanger.SetMaterialColor(PlayerShooting.instance.blasterMaterial, Color.white);
                    MaterialColorChanger.SetMaterialColor(PlayerShooting.instance.sideBlasterMaterial, new Color(1f, 0f, 0.8f));
                    shopButton[4].interactable = false;
                };
                break;
            case Upgrade.ProjectileSpeed:
                if (upgradeShopHUD.CheckPrice(upgradePrice[5]))
                {
                    AudioManager.instance.PlaySFX(AudioManager.SoundEffects.upgradeProjectile);
                    PlayerShooting.instance.blaster.projectileSpeed *= 2;
                    PlayerShooting.instance.blasterLeft.projectileSpeed *= 2;
                    PlayerShooting.instance.blasterRight.projectileSpeed *= 2;
                    shopButton[5].interactable = false;
                };
                break;
        }
    }
}
