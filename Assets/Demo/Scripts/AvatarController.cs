using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class AvatarController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private float movementSmoothing;

    [SerializeField]
    private PhysicsMaterial2D staticFriction;

    [SerializeField]
    private PhysicsMaterial2D dynamicFriction;

    [SerializeField]
    private float maxSlope = 45;

    [SerializeField]
    private float jumpVelocity;

    [SerializeField]
    private float gravityScaleMultiplier;

    [SerializeField]
    private float jumpMemory = 0.1f;

    [SerializeField]
    private float groundCheckheight = 0.1f;

    [SerializeField]
    private float groundCheckWidth = 0.1f;

    [SerializeField]
    private LayerMask orderColliders;

    [SerializeField]
    private LayerMask chaosColliders;

    [SerializeField]
    private ParticleSystem runParticles;

    [SerializeField]
    private ParticleSystem landParticles;

    [SerializeField]
    private AudioClip landingClip;

    private Rigidbody2D rigidbody2d;
    
    private Controls controls;
    private InputAction move;
    private InputAction jump;
    private InputAction quit;

    private Vector2 moveVelocity;
    private Vector3 velocity = Vector3.zero;
    private bool isGrounded = true;
    // private bool jumping = false;
    private float defaultGravityScale;
    private float fallGravityScale;
    private float jumpTimer;
    private float leaveGroundTimer;
    private bool isJumping = false;
    private LayerMask groundMask;

    private Vector2 slopeTangent;
    private float slopeAngle;
    private bool isOnSlope;

    private Animator animator;

    private SpriteRenderer spriteRenderer;

    private AudioSource source;

    private void Awake()
    {
        controls = new Controls();
        rigidbody2d = GetComponent<Rigidbody2D>();

        defaultGravityScale = rigidbody2d.gravityScale;
        fallGravityScale = defaultGravityScale*gravityScaleMultiplier;

        animator = GetComponentInChildren<Animator>();

        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        source = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        move = controls.Avatar.Move;
        jump = controls.Avatar.Jump;
        quit = controls.Avatar.Quit;

        move.Enable();
        jump.Enable();
        quit.Enable();

        jump.started += (context) => OnJump(context);
        jump.canceled += (context) => OnJump(context);
    }

    private void FixedUpdate()
    {
        if(gameObject.layer == LayerMask.NameToLayer("Chaos Player"))
        {
            groundMask = chaosColliders;
        }
        else
        {
            groundMask = orderColliders;
        }

        if(moveVelocity.x == 0 && isGrounded)
        {
            rigidbody2d.sharedMaterial = staticFriction;
        }
        else
        {
            rigidbody2d.sharedMaterial = dynamicFriction;
        }

        Move();

        bool wasGrounded = isGrounded;
        isGrounded = false;
        isOnSlope = false;

        if(rigidbody2d.velocity.y < 0)
        {
            rigidbody2d.gravityScale = defaultGravityScale * gravityScaleMultiplier;
        }

        Vector2 boxSize = new Vector2(groundCheckWidth, groundCheckheight);
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, boxSize, 0, Vector2.down, 0, groundMask);

        // If the ground was detected.
        if(hit.collider != null)
        {
            isGrounded = true;

            rigidbody2d.gravityScale = defaultGravityScale;
            slopeTangent = Vector2.Perpendicular(hit.normal).normalized;
            slopeAngle = Vector2.Angle(hit.normal, Vector2.up);

            Debug.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y) + hit.normal, Color.green);
            Debug.DrawLine(transform.position, slopeTangent + hit.point, Color.red);

            if(slopeAngle != 0)
            {
                isOnSlope = true;
            }
        }

        // If the avatar landed
        if(isGrounded == true && wasGrounded == false)
        {
            isJumping = false;
            if(jumpTimer > 0)
            {
                Jump();
            }
            landParticles.Play();
            source.PlayOneShot(landingClip);
        }

        // If the avatar dropped between frames
        if(isGrounded == false && wasGrounded == true && isJumping == false)
        {
            leaveGroundTimer = jumpMemory;
        }

        if(slopeAngle > maxSlope)
        {
            rigidbody2d.sharedMaterial = dynamicFriction;
        }
    }

    private void Update()
    {
        // Move the avatar.
        moveVelocity.x = move.ReadValue<float>()*moveSpeed;
        moveVelocity.y = rigidbody2d.velocity.y;

        // Flip sprite
        // spriteRenderer.flipX = moveVelocity.x < 0? true:false;s
        if(moveVelocity.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        if(moveVelocity.x > 0)
        {
            spriteRenderer.flipX = false;
        }

        // Animations
        // Running
        if(Mathf.Abs(moveVelocity.x) > 0 && isJumping == false)
        {
            animator.Play("Yin_Run");
            if(runParticles.isEmitting == false)
            {
                runParticles.Play();
            }
            
        }
        // Idle
        else if(isJumping == false)
        {
            animator.Play("Yin_Idle");
            runParticles.Stop();
        }
        // Jumping
        else if(isJumping == true && rigidbody2d.velocity.y > 0)
        {
            animator.Play("Yin_Jump");
            runParticles.Stop();
        }
        // Falling
        else if(isJumping == true && rigidbody2d.velocity.y < 0)
        {
            runParticles.Stop();
        }


        // Update jumpTimers
        jumpTimer -= Time.deltaTime;
        leaveGroundTimer -= Time.deltaTime;
    }

    private void OnDisable()
    {
        move.Disable();

        jump.Disable();
    }

    private void Move()
    {
        if(isGrounded == true && isOnSlope == true)
        {
            moveVelocity = -moveVelocity.x*slopeTangent;
            moveVelocity.y = rigidbody2d.velocity.y;
        }

        rigidbody2d.velocity = Vector3.SmoothDamp(rigidbody2d.velocity, moveVelocity, ref velocity, movementSmoothing);
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            if(isGrounded || leaveGroundTimer > 0)
            {
                Jump();
            }
            else
            {
                jumpTimer = jumpMemory;
            }
        }

        // If the player releases the jump button
        if(context.canceled)
        {
            rigidbody2d.gravityScale = defaultGravityScale * gravityScaleMultiplier;
        }
    }

    private void Jump()
    {
        isJumping = true;
        rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, jumpVelocity);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position + groundCheckWidth*Vector3.right, transform.position + groundCheckWidth*Vector3.right + Vector3.down*groundCheckheight);
        Gizmos.DrawLine(transform.position + groundCheckWidth*Vector3.left, transform.position + groundCheckWidth*Vector3.left + Vector3.down*groundCheckheight);
    }
}
