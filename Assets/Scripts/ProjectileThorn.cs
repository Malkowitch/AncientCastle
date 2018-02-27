using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileThorn : MonoBehaviour
{
    public float speed;
    public float damage;
    private bool stop;
    
    private void Update()
    {
        if (!stop)
        {
            transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);
        }
    }
    private void OnTriggerEnter(Collider _trig)
    {
        if (!_trig.gameObject.tag.Equals("Enemy") && !_trig.gameObject.tag.Equals("AttackObject"))
        {
            Debug.Log(_trig.gameObject.tag);
            if (_trig.gameObject.tag.Equals("Player"))
            {
                _trig.gameObject.GetComponent<HasHealth>().RecieveDamage(damage);
                _trig.gameObject.GetComponent<TakenHit>().GotHit();
                Destroy(gameObject);
            }
            stop = true;
            
        }
    }
}
