using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsumablesShopController : MonoBehaviour
{
    internal static ConsumablesShopController instance;

    [SerializeField] ShopHUD consumablesShopHUD;

    [SerializeField] List<Text> consumablePriceText;
    [SerializeField] List<int> consumablePrice;
    public List<Button> consumableButton;

    void Awake()
    {
        instance = this;

        for (int i = 0; i < consumablePriceText.Count; i++)
        {
            consumablePriceText[i].text = consumablePrice[i].ToString();
        }
    }

    public void BuyMedKit()
    {
        if (consumablesShopHUD.CheckPrice(consumablePrice[0]))
        {
            AudioManager.instance.PlaySFX(AudioManager.SoundEffects.buyMedKit);
            PlayerStats.instance.medKitAmount++;
            HUDController.instance.UpdateMedKitText();
        }
    }

    public void BuyArmor()
    {
        if (consumablesShopHUD.CheckPrice(consumablePrice[1]))
        {
            AudioManager.instance.PlaySFX(AudioManager.SoundEffects.upgradeAuto);
            PlayerStats.instance.armor = PlayerStats.instance.maxArmor;
            HUDController.instance.UpdateArmorBar();
            PlayerStats.instance.GetComponent<MeshRenderer>().material = PlayerStats.instance.armorMaterial;
        }
    }
}
