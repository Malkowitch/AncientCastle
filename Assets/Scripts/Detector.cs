using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour {

    private EnemyAttack ea;

	// Use this for initialization
	void Awake () {
        ea = transform.GetComponentInParent<EnemyAttack>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerStay(Collider other)
    {
        ea.objectInsight = other;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == ea.player)
        {
            if (!ea.CheckIfDead())
            {
                ea.ani.Play("Idle");
            }
            ea.playerInSight = false;
            ea.objectInsight = null;
        }
    }
}
