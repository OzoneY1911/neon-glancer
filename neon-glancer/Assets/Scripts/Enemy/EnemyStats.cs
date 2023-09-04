using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] int health;
    [SerializeField] int maxHealth;

    [Header("Material")]
    [SerializeField] Material materialOrigin;

    void Awake()
    {
        GetComponent<MeshRenderer>().material = new Material(materialOrigin);
    }

    void Start()
    {
        maxHealth = 100;
        health = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Destroy(gameObject);
            Destroy(GetComponent<MeshRenderer>().material);
            WaveController.enemyList.Remove(gameObject);

            PlayerStats.instance.ChangeNeon(200);
            HUDController.instance.UpdateNeonText();

            PlayerStats.instance.health++;
            HUDController.instance.UpdateHealthBar();
        }

        if (health <= maxHealth / 2)
        {
            MaterialColorChanger.SetMaterialColor(GetComponent<MeshRenderer>().material, new Color (1f, 0.15f, 0.15f), Color.red);
        }
    }
}
