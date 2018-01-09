using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerformAttack : MonoBehaviour {

    public float _cd = 0.2f;
    private float _cdr = 0;
    private Transform _cam;
    public float _gunRange = 100.0f;
    public float _gunDam = 50.0f;

    public GameObject debrisPrefab;

	// Use this for initialization
	void Start () {
        _cam = Camera.main.transform;
        
	}
	
	// Update is called once per frame
	void Update () {
        _cdr -= Time.deltaTime;
        if (Input.GetMouseButton(0) && _cdr <= 0)
        {
            _cdr = _cd;
            Ray ray = new Ray(_cam.position, _cam.forward);
            RaycastHit hitinfo;

            if(Physics.Raycast(ray, out hitinfo, _gunRange))
            {
                Vector3 hitPoint = hitinfo.point;
                GameObject go = hitinfo.collider.gameObject;

                HasHealth h = go.GetComponent<HasHealth>();
                if (h != null)
                {
                    h.RecieveDamage(_gunDam);
                }

                if (debrisPrefab != null)
                {
                    Instantiate(debrisPrefab, hitPoint, Quaternion.identity);
                }
            }
        }
	}
}
