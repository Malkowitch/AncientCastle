using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpScript : MonoBehaviour
{
    public GameObject LaserPistol;

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.CompareTag("PickUp"))
        {
            foreach (Transform child in _other.transform)
                if (child.name.Equals("TagName"))
                    FindWeaponByTag(child.tag);

            Destroy(_other.gameObject);
        }
    }

    private void FindWeaponByTag(string tag)
    {
        switch (tag)
        {
            case "LaserPistol": LaserPistolInstantiate(); break;
            default: break;
        }
        gameObject.GetComponent<WeaponsInventory>().RefreshMainList();
    }

    private void LaserPistolInstantiate()
    {
        bool firstPU = true;
        foreach (Transform child in gameObject.transform.GetChild(0))
            if (child != null)
                if (child.name.Equals("LaserPistol"))
                    firstPU = false;
        if (firstPU)
        { 
            Instantiate(LaserPistol, transform.GetChild(0));
            gameObject.transform.GetChild(0).GetChild(0).name = "LaserPistol";
        }
        else
            DualWeapns();
    }

    private void DualWeapns()
    {
        gameObject.GetComponent<WeaponsInventory>().PUActivateWeapon();
        foreach (Transform child in gameObject.transform.GetChild(0).GetChild(0))
        {
            if (child.name.Equals("GunTip"))
            {
                child.GetComponent<LaserScript>().doubleWielding = true;
                break;
            }
        }
        Instantiate(LaserPistol, transform.GetChild(0));
        GameObject go = gameObject.transform.GetChild(0).GetChild(1).gameObject;
        go.name = "LaserPistol2";
        go.transform.localPosition = new Vector3(-0.4434f, LaserPistol.transform.position.y, LaserPistol.transform.position.z);

        foreach (Transform child in go.transform)
        {
            if (child.name.Equals("GunTip"))
            {
                child.GetComponent<LaserScript>().doubleWielding = true;
                child.GetComponent<LaserScript>().isFiring = false;
                child.GetComponent<LaserScript>().offHand = true;
                break;
            }
        }

    }
}
