using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LaserScript : MonoBehaviour
{
    public float _cd = 6f;
    private float _cdr = 0;

    private LineRenderer _line;
    private Light _light;
    public Material _lineMaterial;
    public float _gunRange;
    public float _gunDam;
    public AudioSource _sfx;

    public bool isFiring;
    public bool doubleWielding;

    public int MouseShooter = 0;
    public GameObject debrisPrefab;

    private bool shooting;
    private bool firstHit;

    private float beamTime = 3f;
    private float beamTimeR = 0f;

    // Use this for initialization
    void Start()
    {
        _line = GetComponent<LineRenderer>();
        _light = GetComponent<Light>();
        _sfx = GetComponent<AudioSource>();

        _line.enabled = false;
        _light.enabled = false;
        _line.material = _lineMaterial;
        _line.startWidth = 0.05f;

        shooting = false;
        firstHit = true;
        beamTimeR = beamTime;


    }

    // Update is called once per frame
    void Update()
    {
        _cdr -= Time.deltaTime;
        if (Input.GetMouseButtonDown(MouseShooter))
        {
            if (isFiring)
            {
                _cd = _cdr;
                shooting = true;
                StopCoroutine("FireLaser");
                StartCoroutine("FireLaser");
                if (doubleWielding)
                {
                    isFiring = false;
                }
            }
            else
            {
                isFiring = true;
            }
        }
        if (_cdr <= 0 && !Input.GetMouseButtonDown(MouseShooter))
        {
            firstHit = true;
            shooting = false;
            beamTimeR = beamTime;
        }
    }

    IEnumerator FireLaser()
    {
        //while (Input.GetMouseButton(0))
        while (Input.GetMouseButton(0) && shooting)
        {
            _sfx.Play();
            _line.material.mainTextureOffset = new Vector2(0, Time.time);
            beamTimeR -= Time.deltaTime;
            _line.enabled = true;
            _light.enabled = true;
            Ray ray = new Ray(transform.position, Camera.main.transform.forward + transform.forward);
            RaycastHit hit;

            _line.SetPosition(0, ray.origin);
            Vector3 endpoint = Vector3.zero;
            if (Physics.Raycast(ray, out hit, _gunRange))
                endpoint = hit.point;
            else
                endpoint = ray.GetPoint(_gunRange);

            _line.SetPosition(1, endpoint);

            if (hit.collider != null)
            {
                GameObject go = hit.collider.gameObject;
                HasHealth h = go.GetComponent<HasHealth>();
                if (h != null && firstHit)
                {
                    h.RecieveDamage(_gunDam);
                    firstHit = false;
                }
                if (debrisPrefab != null && !go.tag.Equals("Enemy"))
                {

                    Instantiate(debrisPrefab, endpoint, Quaternion.identity);
                }
            }
            if (beamTimeR <= 0)
            {
                shooting = false;
            }
            yield return null;
        }

        _line.enabled = false;
        _light.enabled = false;
    }
}
