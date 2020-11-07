﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovementHolyKnightNum : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;
    public GameObject gameOverUI;
    public Rigidbody2D rb;
    public Image speeding;
    public float normalMovementSpeed = 70f;
    private float slowedMovementSpeed = 20f;
    private float maxMovementSpeed = 70f;
    private float incraseMovementSpeed = 140f;
    private float horizontalMove = 0f;
    private float slowOverTimeDuration = 1f;
    private float speedOverTimeDuration = 3f;
    private float speedingCd = 9f;
    private float trapOverTimeDuration = 3f;
    private float trapMovementSpeed = 0f;
    private bool jump = false;
    //private bool crouch = false;
    private bool grounded;
    private bool isSpeedingCd = false;

    private void Start()
    {
        speeding.fillAmount = 0;
    }
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal2") * normalMovementSpeed;
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetKeyDown(KeyCode.E) && isSpeedingCd == false)
        {
            isSpeedingCd = true;
            speeding.fillAmount = 1;
            StartCoroutine(IncreasedMovementSpeed());
        }
        if (isSpeedingCd)
        {
            speeding.fillAmount -= 1 / speedingCd * Time.deltaTime;
            if (speeding.fillAmount <= 0)
            {
                speeding.fillAmount = 0;
                isSpeedingCd = false;
            }
        }
        if (Input.GetButtonDown("Jump2"))
        {
            jump = true;
            animator.SetBool("IsJumping", true);
            FindObjectOfType<AudioManager>().Play("Jump");
        }
        //if (Input.GetButtonDown("Crouch"))
        //{
        //    crouch = true;
        //}
        //else if (Input.GetButtonUp("Crouch"))
        //{
        //    crouch = false;
        //}
        if (grounded && GetComponent<FireBall>().knockBackOnHit == false)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
    }

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
    }

    //public void OnCrouching(bool isCrouching)
    //{
    //    animator.SetBool("IsCrouching", isCrouching);
    //}
    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;

        if (rb.position.y < -6f)
        {
            gameOverUI.SetActive(true);
        }
    }
    public void CoroutineHunterSlowOverTimeFrost()
    {
        StartCoroutine(SlowOverTimeOnHitWithFrostBullet());
    }
    public void CoroutineMoveIfTrapHolyKnight()
    {
        StartCoroutine(stopMoveIfTrapHolyKnight());
    }
    IEnumerator SlowOverTimeOnHitWithFrostBullet()
    {
        normalMovementSpeed = slowedMovementSpeed;
        yield return new WaitForSeconds(slowOverTimeDuration);
        normalMovementSpeed = maxMovementSpeed;
    }
    IEnumerator IncreasedMovementSpeed()
    {
        normalMovementSpeed = incraseMovementSpeed;
        yield return new WaitForSeconds(speedOverTimeDuration);
        normalMovementSpeed = maxMovementSpeed;
    }
    IEnumerator stopMoveIfTrapHolyKnight()
    {
        normalMovementSpeed = trapMovementSpeed;
        yield return new WaitForSeconds(trapOverTimeDuration);
        normalMovementSpeed = maxMovementSpeed;
    }
}