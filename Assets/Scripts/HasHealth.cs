using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasHealth : MonoBehaviour
{

    public float health = 250.0f;
    private Animator _animator;
    public float decay = 10.0f;
    public bool isDead = false;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void RecieveDamage(float _amt)
    {
        health -= _amt;
        _animator.Play("Get_hit", 0, 0);
        if (health <= 0)
            Die();
    }

    private void Die()
    {
        _animator.StopPlayback();
        isDead = true;
        _animator.Play("Dead", 0, 0);
        gameObject.tag = "Dead";
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        Destroy(gameObject, decay);
        FPController.Kills++;
    }
}
