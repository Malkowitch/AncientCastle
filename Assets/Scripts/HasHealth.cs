using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasHealth : MonoBehaviour {

    public float health = 250.0f;

    public void RecieveDamage(float _amt)
    {
        health -= _amt;
        if (health <= 0)
            Die();
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
