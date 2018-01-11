using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {
    public float health;
    private Text text;
    private Text healthbar;

    private void Start()
    {
        healthbar = GameObject.FindGameObjectWithTag("PlayerHealth").GetComponent<Text>();
        text = GameObject.FindGameObjectWithTag("Defeat").GetComponent<Text>();
        healthbar.text = health.ToString();
        text.enabled = false;
    }
    public void RecievedDamage(float _amt)
    {
        health -= _amt;
        healthbar.text = health.ToString();
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        text.enabled = true;
        Time.timeScale = 0;
    }
}
