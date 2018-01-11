using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrainStamina : MonoBehaviour
{

    public float Stamina = 100f;
    private Text text;

    // Use this for initialization
    void Start()
    {
        text = GameObject.FindGameObjectWithTag("PlayerStamina").GetComponent<Text>();
    }
    public void Drain(float _v, float _h)
    {
        bool drain = false;

        if (_v > 10)
            drain = true;

        if (_h > 10)
            drain = true;

        if (drain)
            Stamina -= Time.deltaTime * 10;
        if (!drain && Stamina < 100)
        {
            Stamina += Time.deltaTime / 4;
        }
        text.text = Stamina.ToString();
    }
}
