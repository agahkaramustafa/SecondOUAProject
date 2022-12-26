using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D _playerRB;
    TextMeshProUGUI countdownText;
    Animator _animator;

    bool isGameStarted = false;
    bool isGrounded = true;
    bool isJumping = false;

    [SerializeField] float speed = 3.5f;
    [SerializeField] float jumpForce = 20000f;
    [SerializeField] Canvas startingCanvas;
    [SerializeField] Button startButton;

    void Awake()
    {
        _playerRB = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (isGameStarted && isGrounded)
            {
                _animator.SetTrigger("Jump");
                isGrounded = false;
                isJumping = true;
            }
        }

        _animator.SetBool("Grounded", isGrounded);
    }

    void FixedUpdate()
    {
        if (isGameStarted)
        {
            _playerRB.velocity = new Vector2(speed, _playerRB.velocity.y);
        }

        if (isJumping)
        {
            _playerRB.AddForce(new Vector2(0f, jumpForce));
            isJumping = false;
        }
    }

    public void IngameStartButton()
    {
        StartCoroutine(StartingCountdown());
    }

    IEnumerator StartingCountdown()
    {
        countdownText = startingCanvas.GetComponentInChildren<TextMeshProUGUI>();
        startButton.enabled = false;

        for (int i = 3; i >= 0; i--)
        {
            countdownText.text = i.ToString();
            yield return new WaitForSeconds(1);
        }
        startingCanvas.enabled = false;
        isGameStarted = true;
        _animator.SetBool("GameStarted", isGameStarted);
        _animator.SetBool("Grounded", isGrounded);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Zemin"))
        {
            isGrounded = true;
        }
    }

}
