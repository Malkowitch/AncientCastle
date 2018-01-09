using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPShooting : MonoBehaviour {


    public GameObject bullet;
    private float bulletSpeed = 20f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire1"))
        {
            Camera cam = Camera.main;
            GameObject shot =  (GameObject)Instantiate(bullet, cam.transform.position + cam.transform.forward, cam.transform.rotation);
            shot.GetComponent<Rigidbody>().AddForce(cam.transform.forward * bulletSpeed, ForceMode.Impulse);
        }
	}
}
