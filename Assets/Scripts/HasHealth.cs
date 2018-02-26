using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HasHealth : MonoBehaviour
{

    public float health;
    public float decay;
    public bool isDead;
    public bool isEnemy;
    
    public void RecieveDamage(float _amt)
    {
        health -= _amt;
        if (isEnemy)
        {
            GetComponent<Animator>().Play("Get_hit");
        }
        if (health <= 0)
            Die();
    }

    private void Die()
    {
        if (isEnemy)
        {
            Animator _animator = GetComponent<Animator>();
            _animator.StopPlayback();
            isDead = true;
            _animator.Play("Dead", 0, 0);
            gameObject.tag = "Dead";
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
            Destroy(gameObject, decay);
            FPController.Kills++;
        }
        else
        {
            GameObject.FindGameObjectWithTag("Defeat").GetComponent<Text>().enabled = true;
            Time.timeScale = 0;
        }
    }
}
