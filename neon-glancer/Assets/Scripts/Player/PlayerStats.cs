using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    internal static PlayerStats instance;

    public Material playerMaterial;
    public Material armorMaterial;
    public Material medKitMaterial;

    [Header("Health")]
    public int health;
    public int armor;
    int maxHealth = 100;
    public int maxArmor = 100;
    bool healing;

    [Header("Items")]
    public int medKitAmount;
    public int maxMedKitAmount = 5;

    [Header("Neon")]
    public int neon;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        health = maxHealth;

        GetComponent<MeshRenderer>().material = playerMaterial;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H) && PlayerMovement.instance.canRotate && medKitAmount > 0 && health < 100 && Time.timeScale == 1)
        {
            if (medKitAmount > 0 && !healing)
            {
                StartCoroutine(UseMedKit());
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if (armor > 0)
        {
            armor -= damage;

            if (armor < 0)
            {
                armor = 0;
            }

            HUDController.instance.UpdateArmorBar();
        }
        else
        {
            health -= damage;
            HUDController.instance.UpdateHealthBar();
        }

        if (health <= 0)
        {
            GetComponent<PlayerMovement>().enabled = false;
            GetComponent<PlayerShooting>().enabled = false;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            GetComponent<Rigidbody>().freezeRotation = false;
            GetComponent<Rigidbody>().AddRelativeForce(transform.forward * (-10), ForceMode.Impulse);
            WaveController.instance.game_over = true;
        }
    }

    public void ChangeNeon(int amount)
    {
        neon += amount;
    }

    IEnumerator UseMedKit()
    {
        healing = true;

        AudioManager.instance.PlaySFX(AudioManager.SoundEffects.useMedKit);

        Material initialMaterial = GetComponent<MeshRenderer>().material;
        GetComponent<MeshRenderer>().material = medKitMaterial;

        medKitAmount--;
        HUDController.instance.UpdateMedKitText();

        for (int i = 0; i < 25; i++)
        {
            health++;
            HUDController.instance.UpdateHealthBar();

            yield return null;
        }

        if (health > 100)
        {
            health = 100;
        }

        GetComponent<MeshRenderer>().material = initialMaterial;

        healing = false;
    }
}
