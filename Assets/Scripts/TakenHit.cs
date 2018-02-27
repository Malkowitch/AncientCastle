using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TakenHit : MonoBehaviour {


    public GameObject bloodView;
    public AudioSource projHit;
    private byte imageAlpha = 48;
    private float imageAlphaDegenerate = 0;
    
    private void Update()
    {
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
