using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    public float SightRange = 10.0f;
    public float ViewRangeAngle = 110f;
    public bool playerInSight;
    public float misfire = 3f;

    private SphereCollider col;
    public GameObject attackObject;

    private GameObject player;

    private Camera cam;
    private Animator ani;
    // Use this for initialization
    void Awake()
    {
        ani = GetComponent<Animator>();
        cam = GetComponent<Camera>();
        player = GameObject.FindGameObjectWithTag("Player");
        col = attackObject.GetComponent<SphereCollider>();
        playerInSight = false;

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerStay(Collider other)
    {
        if (!GetComponent<HasHealth>().isDead)
        {

            if (other.gameObject == player)
            {
                playerInSight = false;
                Vector3 direction = other.transform.position - transform.position;
                float angle = Vector3.Angle(direction, transform.forward);
                if (angle < ViewRangeAngle * 0.5f)
                {
                    RaycastHit hit;
                    if (Physics.Raycast(transform.position + transform.up, direction.normalized, out hit, col.radius))
                    {
                        if (hit.collider.gameObject.Equals(player))
                        {
                            Debug.DrawLine(transform.position, RandomHit(hit.collider.gameObject.transform), RandomColor());
                            ani.Play("Attack");
                            playerInSight = true;
                        }
                    }
                }
            }
        }
    }
    private Vector3 RandomHit(Transform _toHit)
    {
        switch (new System.Random().Next(1, 6))
        {
            case 1: return _toHit.position + _toHit.up * misfire;
            case 2: return _toHit.position - _toHit.up * misfire;
            case 3: return _toHit.position + _toHit.right * misfire;
            case 4: return _toHit.position - _toHit.right * misfire;
            default: return _toHit.position;
        }
    }
    private Color RandomColor()
    {
        switch (new System.Random().Next(1, 6))
        {
            case 1: return Color.black;
            case 2: return Color.blue;
            case 3: return Color.red;
            case 4: return Color.yellow;
            default: return Color.white;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            ani.Play("Idle");
            playerInSight = false;
        }
    }
}
