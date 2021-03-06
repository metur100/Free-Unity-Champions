﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyJumpOverObstacle : MonoBehaviour
{
	float dirX;

	[SerializeField]
	float moveSpeed = 3f;

	Rigidbody2D rb;

	bool facingRight = false;

	Vector3 localScale;

	public Animator animator;

	// Use this for initialization
	void Start()
	{
		localScale = transform.localScale;
		rb = GetComponent<Rigidbody2D>();
		dirX = -1f;
	}

	// Update is called once per frame
	void Update()
	{
		if (transform.position.x < 5925.8f)
			dirX = 1f;
		else if (transform.position.x > 6268.5f)
			dirX = -1f;
	}

	void FixedUpdate()
	{
		rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
	}

	void LateUpdate()
	{
		CheckWhereToFace();
	}

	void CheckWhereToFace()
	{
		if (dirX < 0)
			facingRight = true;
		else if (dirX > 0)
			facingRight = false;

		if (((facingRight) && (localScale.x < 0)) || ((!facingRight) && (localScale.x > 0)))
			localScale.x *= -1;

		transform.localScale = localScale;
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		switch (col.tag)
		{
			case "BigStump":
				rb.AddForce(Vector2.up * 2000f);
				animator.SetTrigger("IsJumping");
				break;
		}
	}
}
