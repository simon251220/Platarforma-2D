using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    [Header("Speed setup")]
    public Rigidbody2D playerRigidbody2D;
    public Vector2 friction = new Vector2(.1f, 0);
    public float speed;
    public float runSpeed;
    public float forceJump = 2;
    private float _currentSpeed;

    [Header("Animation")]
    public float jumpScaleY = 1.5f;
    public float jumpScaleX = .7f;
    public float animationDuration = .3f;
    public Ease ease = Ease.OutBack;

    void Update()
    {
        HandleJump();
        HandleMovement();
    }

    private void HandleMovement()
    {
        if(Input.GetKey(KeyCode.LeftControl))
        {
            _currentSpeed = runSpeed;
        }
        else
        {
            _currentSpeed = speed;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            playerRigidbody2D.velocity = new Vector2(-_currentSpeed, playerRigidbody2D.velocity.y);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            playerRigidbody2D.velocity = new Vector2(_currentSpeed, playerRigidbody2D.velocity.y);
        }

        if(playerRigidbody2D.velocity.x > 0)
        {
            playerRigidbody2D.velocity += friction; 
        }

        else if (playerRigidbody2D.velocity.x < 0)
        {
            playerRigidbody2D.velocity -= friction;
        }

    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerRigidbody2D.velocity = Vector2.up * forceJump;
            playerRigidbody2D.transform.localScale = Vector2.one;

            DOTween.Kill(playerRigidbody2D.transform);

            HandleScaleJump();
        }
    }

    private void HandleScaleJump()
    {
        playerRigidbody2D.transform.DOScaleY(jumpScaleY, animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(ease);
        playerRigidbody2D.transform.DOScaleX(jumpScaleX, animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(ease);
    }
}
