using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillFullScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
        RectTransform rt = GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(Screen.width, Screen.height);
	}
}
