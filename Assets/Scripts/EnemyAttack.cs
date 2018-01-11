using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

    public float SightRange = 30.0f;
	// Use this for initialization
	void Start () {
        Animator ani = GetComponent<Animator>();
        
	}
	
	// Update is called once per frame
	void Update () {
        
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction, Color.blue);

        Physics.Raycast(ray, out hit, SightRange);
        Debug.DrawRay(transform.position, hit.point, Color.red);

	}
}
