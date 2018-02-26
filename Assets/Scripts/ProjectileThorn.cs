using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileThorn : MonoBehaviour
{

    public float speed;
    public float damage;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);
    }
    private void OnTriggerEnter(Collider _trig)
    {
        if (!_trig.gameObject.tag.Equals("Enemy") && !_trig.gameObject.tag.Equals("AttackObject"))
        {
            if (_trig.gameObject.tag.Equals("Player"))
            {
                _trig.gameObject.GetComponent<HasHealth>().RecieveDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}
