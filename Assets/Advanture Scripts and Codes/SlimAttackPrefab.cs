﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimAttackPrefab : MonoBehaviour
{
    [SerializeField]
    GameObject target;
    [SerializeField]
    float speed;
    [SerializeField]
    Rigidbody2D bulletRB;
    int damageDone = -20;
    // Start is called before the first frame update
    void Start()
    {
        bulletRB = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player_Knight");
        Vector2 moveDir = (target.transform.position - transform.position).normalized * speed;
        bulletRB.velocity = new Vector2(moveDir.x, moveDir.y);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player_Knight")
        {
            HealthKnight eHealth = other.gameObject.GetComponent<HealthKnight>();
            eHealth.ModifyHealth(damageDone);
        }
    }
}