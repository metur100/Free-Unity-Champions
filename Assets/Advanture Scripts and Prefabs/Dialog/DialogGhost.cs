﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogGhost : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    [TextArea(3, 10)]
    public string[] sentences;
    private int index;
    public float typingSpeed;
    public GameObject activateDialog;
    public GameObject activateWallAndEnemies;
    public PlayerMovementAdvanturerKnight moveSpeed;

    public GameObject continueButton;

    public GameObject triggerWall;
    [SerializeField]
    Transform positionWall;
    //public Animator animator;

    private void Start()
    {
        StartCoroutine(Type());
    }
    private void Update()
    {
        if (textDisplay.text == sentences[index])
        {
            continueButton.SetActive(true);
        }
    }
    IEnumerator Type()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
    public void NextSentence()
    {
        //animator.SetTrigger("Change");
        continueButton.SetActive(false);
        activateWallAndEnemies.SetActive(true);
        Instantiate(triggerWall, positionWall.position, Quaternion.identity);
        if (index < sentences.Length - 1)
        {
            index++;
            textDisplay.text = "";
            //Instantiate(triggerWall, transform.position, Quaternion.identity);
            StartCoroutine(Type());
        }
        else
        {
            textDisplay.text = "";
            continueButton.SetActive(false);
        }
        if (index == sentences.Length - 1)
        {
            activateDialog.SetActive(false);
            moveSpeed.normalMovementSpeed = 400f;
        }
    }
}