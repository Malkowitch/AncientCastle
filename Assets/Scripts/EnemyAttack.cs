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
    public float rotateSpeed;

    public Collider objectInsight;

    private SphereCollider col;
    public GameObject attackObject;

    public GameObject player;

    public GameObject projectilePrefab;
    
    public Animator ani;

    public float cd;
    private float cdr;
    // Use this for initialization
    private void Awake()
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
    private void Update()
    {
        if (objectInsight != null)
        {
            if (!GetComponent<HealthScript>().isDead)
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
                                    Vector3 shootPosition = transform.GetChild(3).position;
                                    shootPosition = shootPosition + Vector3.up;
                                    Instantiate(projectilePrefab, shootPosition, transform.rotation);
                                    //Debug.Log(hit.collider.gameObject.tag);
                                    cdr = cd;
                                }
                                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotateSpeed);

                                ani.Play("Attack");
                                playerInSight = true;
                            }
                        }
                    }
                }
            }
        }

    }

    public bool CheckIfDead()
    {
        return GetComponent<HealthScript>().isDead;
    }
}
