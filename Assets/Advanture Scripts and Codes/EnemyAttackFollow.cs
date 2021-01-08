﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackFollow : MonoBehaviour
{
    [SerializeField]
    Transform player;
    [SerializeField]
    float agroRange;
    [SerializeField]
    float moveSpeed;
    [SerializeField]
    float shootingRange;
    [SerializeField]
    GameObject bullet;
    [SerializeField]
    GameObject bulletParent;
    float fireRate = 1f;
    private float nextFireTime;
    [SerializeField]
    Rigidbody2D rb2d;
    //private Animator animator;
    //private float horizontalMove = 0f;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>(); 
    }

    // Update is called once per frame
    void Update()
    {
        float distToPlayer = Vector2.Distance(transform.position, player.position);

        if (distToPlayer < agroRange && distToPlayer > shootingRange)
        {
            ChasePlayer();
            //animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        }
        else if (distToPlayer <= shootingRange && nextFireTime < Time.time)
        {
            StartShooting();
            //animator.SetTrigger("IsAttacking");
            nextFireTime = Time.time + fireRate;
        }
        else
        {
            StopChasingPlayer();
        }
    }
    private void ChasePlayer()
    {
        if (transform.position.x < player.position.x)
        {
            rb2d.velocity = new Vector2(moveSpeed, 0);
            transform.localScale = new Vector2(10, 10);
        }
        else //if (transform.position.x > player.position.x)
        {
            rb2d.velocity = new Vector2(-moveSpeed, 0);    
            transform.localScale = new Vector2(-10, 10);
        }
    }
    private void StopChasingPlayer()
    {
        rb2d.velocity = new Vector2(0, 0);
    }

    private void StartShooting()
    {
        Instantiate(bullet, bulletParent.transform.position, Quaternion.identity);  
    }
}