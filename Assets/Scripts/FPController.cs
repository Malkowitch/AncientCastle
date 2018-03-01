using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(CharacterController))]
public class FPController : MonoBehaviour
{
    public float movementSpeed = 10.0f;
    public float mouseSensitivity = 10.0f;
    public float jumpSpeed = 7.0f;

    public Text endText;
    private int maxEnemy;
    public static int Kills { get; set; }

    private float verticalVelocity = 0;

    private CharacterController cc;
    private DrainStamina ds;

    // Use this for initialization
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        cc = GetComponent<CharacterController>();

        endText.enabled = false;
        ds = GetComponent<DrainStamina>();


        maxEnemy = GameObject.FindGameObjectsWithTag("Enemy").Length;

    }

    private void FixedUpdate()
    {
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
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1) && GetComponent<WeaponsInventory>().mainWeapons.Length > 0)
            GetComponent<WeaponsInventory>().WeaponActive();
        if (Kills == maxEnemy)
        {
            Time.timeScale = 0;
            endText.enabled = true;
        }
    }


}
