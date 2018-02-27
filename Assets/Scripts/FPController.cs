using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(CharacterController))]
public class FPController : MonoBehaviour
{
    #region Public Movement
    public float movementSpeed = 10.0f;
    public float mouseSensitivity = 10.0f;
    public float jumpSpeed = 7.0f;
    #endregion
    #region Public View
    private float vRotation = 0;
    public float minLimit = -25;
    public float maxLimit = 45;
    #endregion

    public Text endText;
    private int maxEnemy;
    public static int Kills { get; set; }
    #region Private
    private float verticalVelocity = 0;
    private Transform player;
    private CharacterController cc;
    public static GameObject[] mainWeapons;
    private DrainStamina ds;
    private Text text;
    private Text healthbar;
    #endregion

    // Use this for initialization
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        player = Camera.main.transform;
        cc = GetComponent<CharacterController>();

        mainWeapons = GameObject.FindGameObjectsWithTag("MainWeapon");
        endText.enabled = false;
        ds = GetComponent<DrainStamina>();


        healthbar = GameObject.FindGameObjectWithTag("PlayerHealth").GetComponent<Text>();
        text = GameObject.FindGameObjectWithTag("Defeat").GetComponent<Text>();
        text.enabled = false;
        maxEnemy = GameObject.FindGameObjectsWithTag("Enemy").Length;

    }

    public static void RefreshMainList()
    {
        mainWeapons = GameObject.FindGameObjectsWithTag("MainWeapon");
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        #region rotation
        var rotX = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(0, rotX, 0);
        #endregion

        #region View
        vRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        vRotation = Mathf.Clamp(vRotation, minLimit, maxLimit);

        player.localRotation = Quaternion.Euler(vRotation, 0, 0);
        #endregion

        #region Movement
        float vertical = Input.GetAxis("Vertical") * movementSpeed;
        float horizont = Input.GetAxis("Horizontal") * movementSpeed;
        if (Input.GetKey(KeyCode.LeftShift) && vertical > 0)
        {
            if (ds.Stamina >= 0)
            {
                vertical = vertical * 2.5f;
                horizont = horizont * 2.5f;
            }
        }
        ds.Drain(vertical, horizont);

        verticalVelocity += Physics.gravity.y * Time.deltaTime;

        if (Input.GetButtonDown("Jump") && cc.isGrounded)
        {
            verticalVelocity = jumpSpeed;
        }

        Vector3 speed = new Vector3(horizont, verticalVelocity, vertical);
        speed = transform.rotation * speed;


        cc.Move(speed * Time.deltaTime);

        #endregion
    }

    private void Update()
    {
        healthbar.text = GetComponent<HasHealth>().health.ToString();

        if (Input.GetKeyDown(KeyCode.Alpha1) && mainWeapons.Length > 0)
            WeaponActive();
        if (Kills == maxEnemy)
        {
            Time.timeScale = 0;
            endText.enabled = true;
        }
    }

    private void WeaponActive()
    {
        foreach (GameObject mainWeapon in mainWeapons)
        {
            mainWeapon.GetComponentInChildren<LaserScript>().ShowHideAmmoText();

            mainWeapon.SetActive(UniversalMethods.ReBoolean(mainWeapon.activeSelf));
        }
    }
    public static void PUActivateWeapon()
    {
        foreach (GameObject mainWeapon in mainWeapons)
        {
            if (!mainWeapon.activeSelf)
            {
                mainWeapon.GetComponentInChildren<LaserScript>().ShowHideAmmoText();
                
                mainWeapon.SetActive(UniversalMethods.ReBoolean(mainWeapon.activeSelf));
            }
        }
    }

}
