using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPView : MonoBehaviour {


    private float vRotation = 0;
    public float minLimit = -25;
    public float maxLimit = 45;
    public float mouseSensitivity = 10.0f;


	void FixedUpdate () {

        var rotX = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(0, rotX, 0);

        vRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        vRotation = Mathf.Clamp(vRotation, minLimit, maxLimit);

        Camera.main.transform.localRotation = Quaternion.Euler(vRotation, 0, 0);
    }
}
