using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTakenHit : MonoBehaviour {


    public GameObject bloodView;
    public AudioSource projHit;
    private byte imageAlpha = 48;
    private float imageAlphaDegenerate = 0;
    private Text healthbar;
    private Text defeatText;

    private void Start()
    {
        defeatText = GameObject.FindGameObjectWithTag("Defeat").GetComponent<Text>();
        defeatText.enabled = false;
        healthbar = GameObject.FindGameObjectWithTag("PlayerHealth").GetComponent<Text>();
    }

    private void Update()
    {
        healthbar.text = GetComponent<HealthScript>().health.ToString();
        if (imageAlphaDegenerate > 0)
        {
            imageAlphaDegenerate = imageAlphaDegenerate - (Time.deltaTime * 10);
            if (imageAlphaDegenerate <= 0)
            {
                imageAlphaDegenerate = 0;
            }
            bloodView.GetComponent<Image>().color = new Color32(221, 111, 111, Convert.ToByte(imageAlphaDegenerate));
        }
    }

    public void GotHit()
    {
        bloodView.GetComponent<Image>().color = new Color32(221, 111, 111, imageAlpha);
        imageAlphaDegenerate = imageAlpha;
        if (projHit != null)
            projHit.Play();
    }
}
