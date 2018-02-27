using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public AudioSource shootSound;
    public AudioSource reloadSound;

    public bool isFiring;
    public bool doubleWielding;

    public bool offHand;

    public string MouseShooter;
    public GameObject debrisPrefab;

    private bool shooting;
    private bool firstHit;

    private float beamTime = 3f;
    private float beamTimeR = 0f;
    private Vector3 CrosshairPlace;
    
    public float maxAmmo;
    private float ammoRem;
    public Vector2 ammoAnchorMin;
    public Vector2 ammoAnchorMax;
    public Vector2 ammoAnchorPivot;
    private Text ammoText;

    public GameObject AmmoTextObject;

    // Use this for initialization
    private void Start()
    {
        _line = GetComponent<LineRenderer>();
        _light = GetComponent<Light>();
        //shootSound = GetComponent<AudioSource>();

        CrosshairPlace = GameObject.FindGameObjectWithTag("Crosshair").transform.position;

        _line.enabled = false;
        _light.enabled = false;
        _line.material = _lineMaterial;
        _line.startWidth = 0.05f;

        shooting = false;
        firstHit = true;
        beamTimeR = beamTime;
        string searchPara = "";
        if (!offHand)
        {
            searchPara = "AmmoTextR";
        }
        else
        {
            searchPara = "AmmoTextL";
        }

        foreach (Transform item in GameObject.Find("UI").transform)
        {
            if (item.tag.Equals(searchPara))
            {
                AmmoTextObject = item.gameObject;
                break;
            }
        }
        AmmoTextObject.SetActive(true);
        ammoText = AmmoTextObject.GetComponent<Text>();
        ammoRem = maxAmmo;

        ammoText.text = ammoRem + "/" + maxAmmo;

    }

    public void ShowHideAmmoText()
    {
        AmmoTextObject.SetActive(ReBoolean(AmmoTextObject.activeSelf));
    }
    private bool ReBoolean(bool _toBool)
    {
        if (_toBool)
            return false;
        return true;
    }

    // Update is called once per frame
    private void Update()
    {
        _cdr -= Time.deltaTime;
        if (Input.GetButtonDown(MouseShooter) && ammoRem != 0)
        {
            if (isFiring)
            {
                _cd = _cdr;
                shooting = true;
                StartCoroutine("FireLaser");
                ammoRem--;
                ammoText.text = ammoRem + "/" + maxAmmo;
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
        if (_cdr <= 0)
        {
            firstHit = true;
            shooting = false;
            beamTimeR = beamTime;
        }
        if (Input.GetKeyDown(KeyCode.R) && ammoRem != maxAmmo)
        {
            if (!offHand)
                isFiring = true;
            else
                isFiring = false;

            reloadSound.Play();
            ammoRem = maxAmmo;
            ammoText.text = ammoRem + "/" + maxAmmo;
        }
        
    }

    private IEnumerator FireLaser()
    {
        //while (Input.GetMouseButton(0))
        while (Input.GetMouseButton(0) && shooting)
        {
            shootSound.Play();
            _line.material.mainTextureOffset = new Vector2(0, Time.time);
            beamTimeR -= Time.deltaTime;
            _line.enabled = true;
            _light.enabled = true;
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(CrosshairPlace.x, CrosshairPlace.y, CrosshairPlace.z));
            RaycastHit hit;


            _line.SetPosition(0, transform.position);
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
