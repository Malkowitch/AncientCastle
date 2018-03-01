using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsInventory : MonoBehaviour {

    public  GameObject[] mainWeapons;

    // Use this for initialization
    void Start () {
        mainWeapons = GameObject.FindGameObjectsWithTag("MainWeapon");
    }
	
	// Update is called once per frame
	void Update () {

    }
    public void RefreshMainList()
    {
        mainWeapons = GameObject.FindGameObjectsWithTag("MainWeapon");
    }
    public void WeaponActive()
    {
        foreach (GameObject mainWeapon in mainWeapons)
        {
            mainWeapon.GetComponentInChildren<LaserScript>().ShowHideAmmoText();

            mainWeapon.SetActive(UniversalMethods.ReBoolean(mainWeapon.activeSelf));
        }
    }
    public void PUActivateWeapon()
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
