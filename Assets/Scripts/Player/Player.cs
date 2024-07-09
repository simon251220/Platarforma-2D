using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public HealthBase healthBase;
    public Rigidbody2D playerRigidbody2D;
    private float _currentSpeed;
    //public Animator animator;
    private Animator _currentPlayer;

    [Header("Player setup")]
    public SOPlayerSetup soPlayerSetup;

    [Header("Jump setup")]
    public Collider2D collider2DPlayer;
    public float disToGround;
    public float spaceToGround;
    public ParticleSystem jumpVFX;
    public AudioSource audioSourcePlayerJump;

    void Awake()
    {
        if(healthBase != null)
        {
            healthBase.onKill += OnPlayerKill;
        }

        //_currentPlayer = Instantiate(soPlayerSetup.player, transform);
        _currentPlayer = transform.GetComponentInChildren<Animator>();

        if(collider2DPlayer != null)
        {
            disToGround = collider2DPlayer.bounds.extents.y;
        }
    }

    private bool IsGrounded()
    {
        Debug.DrawRay(transform.position, -Vector2.up, Color.green, disToGround + spaceToGround);
        return Physics2D.Raycast(transform.position, -Vector2.up, disToGround + spaceToGround);
    }

    private void OnPlayerKill()
    {
        healthBase.onKill -= OnPlayerKill;
        _currentPlayer.SetTrigger(soPlayerSetup.triggerKill);
    }

    private void PlayJumpVFX()
    {
        //if(jumpVFX != null)jumpVFX.Play();
        VFXManager.Instance.PlayVFXByType(VFXManager.VFXType.JUMP, transform.position);

    }

    private void PlayJumpSFX()
    {
        //if(jumpVFX != null)jumpVFX.Play();
        audioSourcePlayerJump.Play();

    }

    void Update()
    {
        IsGrounded();
        print(IsGrounded());
        HandleJump();
        HandleMovement();
    }

    private void HandleMovement()
    {
        if(Input.GetKey(KeyCode.LeftControl))
        {
            _currentSpeed = soPlayerSetup.runSpeed;
            _currentPlayer.speed = 2;
        }
        else
        {
            _currentSpeed = soPlayerSetup.speed;
            _currentPlayer.speed = 1;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            playerRigidbody2D.velocity = new Vector2(-_currentSpeed, playerRigidbody2D.velocity.y);
            if(playerRigidbody2D.transform.localScale.x != -1)
            {
                playerRigidbody2D.transform.DOScaleX(-1, soPlayerSetup.durationScale);
            }

            _currentPlayer.SetBool(soPlayerSetup.boolRun, true);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            playerRigidbody2D.velocity = new Vector2(_currentSpeed, playerRigidbody2D.velocity.y);
            if (playerRigidbody2D.transform.localScale.x != 1)
            {
                playerRigidbody2D.transform.DOScaleX(1, soPlayerSetup.durationScale);
            }
            _currentPlayer.SetBool(soPlayerSetup.boolRun, true);
        }
        else
        {
            _currentPlayer.SetBool(soPlayerSetup.boolRun, false);
        }

        if(playerRigidbody2D.velocity.x > 0)
        {
            playerRigidbody2D.velocity += soPlayerSetup.friction; 
        }

        else if (playerRigidbody2D.velocity.x < 0)
        {
            playerRigidbody2D.velocity -= soPlayerSetup.friction;
        }

    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            PlayJumpVFX();
            PlayJumpSFX();
            playerRigidbody2D.velocity = Vector2.up * soPlayerSetup.forceJump;
            
            if (transform.localScale.x > 0)
            {
                playerRigidbody2D.transform.localScale = Vector2.one;
            }
            else if (transform.localScale.x < 0)
            {
                playerRigidbody2D.transform.localScale = new Vector2(-1, 1);
            }

            DOTween.Kill(playerRigidbody2D.transform);

            HandleScaleJump();
        }
    }

    private void HandleScaleJump()
    {
        
        playerRigidbody2D.transform.DOScaleY(soPlayerSetup.jumpScaleY, soPlayerSetup.animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(soPlayerSetup.ease);

        if(transform.localScale.x > 0)
        {
            playerRigidbody2D.transform.DOScaleX(soPlayerSetup.jumpScaleX, soPlayerSetup.animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(soPlayerSetup.ease);
        }
        else if(transform.localScale.x < 0)
        {
            playerRigidbody2D.transform.DOScaleX(-soPlayerSetup.jumpScaleX, soPlayerSetup.animationDuration).SetLoops(2, LoopType.Yoyo).SetEase(soPlayerSetup.ease);
        }
        
    }

    public void DestroyMe()
    {
        Destroy(this.gameObject);
    }
}
