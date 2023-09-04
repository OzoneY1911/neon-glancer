using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    internal static PlayerStats instance;

    [Header("Health")]
    public int health;
    int maxHealth;

    [Header("Neon")]
    public int neon;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        maxHealth = 100;
        health = maxHealth;

        neon = 0;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

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
}
