using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(CharacterController))]
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

    #region Private
    private float verticalVelocity = 0;
    private Transform player;
    private CharacterController cc;
    private Rigidbody rb;
    #endregion

    // Use this for initialization
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        player = Camera.main.transform;
        cc = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
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
        var vertical = Input.GetAxis("Vertical") * movementSpeed;
        var horizont = Input.GetAxis("Horizontal") * movementSpeed;

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
}
