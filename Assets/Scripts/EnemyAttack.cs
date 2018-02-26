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

    public Collider objectInsight;

    private SphereCollider col;
    public GameObject attackObject;

    public GameObject player;

    public GameObject projectilePrefab;
    
    public Animator ani;

    public float cd;
    private float cdr;
    // Use this for initialization
    void Awake()
    {
        ani = GetComponent<Animator>();
        //cam = GetComponent<Camera>();
        player = GameObject.FindGameObjectWithTag("Player");
        col = attackObject.GetComponent<SphereCollider>();
        playerInSight = false;
        cd = 0.5f;
        cdr = cd;
    }
    // Update is called once per frame
    void Update()
    {
        if (objectInsight != null)
        {
            if (!GetComponent<HasHealth>().isDead)
            {
                if (objectInsight.gameObject == player)
                {
                    playerInSight = false;
                    Vector3 direction = objectInsight.transform.position - transform.position;
                    float angle = Vector3.Angle(direction, transform.forward);
                    cdr -= Time.deltaTime;
                    if (angle < ViewRangeAngle * 0.5f)
                    {
                        RaycastHit hit;
                        if (Physics.Raycast(transform.position + transform.up, direction.normalized, out hit, col.radius))
                        {
                            if (hit.collider.gameObject.Equals(player))
                            {
                                if (cdr <= 0)
                                {
                                    Instantiate(projectilePrefab, transform.GetChild(3).position, transform.rotation);
                                    cdr = cd;
                                }
                                ani.Play("Attack");
                                playerInSight = true;
                            }
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
}
