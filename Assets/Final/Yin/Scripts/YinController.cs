using UnityEngine;
using UnityEngine.InputSystem;
using srs.AvatarController;

[RequireComponent(typeof(AvatarController2D))]
public class YinController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    [SerializeField]
    private ParticleSystem LandParticles;

    private SpriteRenderer spriteRenderer;

    private AvatarController2D controller;
    private Controls controls;

    private Animator animator;

    private void Awake()
    {
        // Setup Controls
        controller = GetComponent<AvatarController2D>();
        controls = new Controls();
        controls.Enable();
        controls.Avatar.Jump.performed += Jump;
        controls.Avatar.Jump.canceled += Jump;

        animator = GetComponentInChildren<Animator>();

        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        Vector2 moveVector = controls.Avatar.Move.ReadValue<float>()*Vector2.right;
        controller.Move(moveVector*moveSpeed);

        if(moveVector.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if(moveVector.x > 0)
        {
            spriteRenderer.flipX = false;
        }

        // If Yin is moving on the ground
        if(Mathf.Abs(controller.Velocity.x) > 0 && controller.IsGrounded)
        {
            animator.Play("Run");
        }
        else if(controller.IsGrounded)
        {
            animator.Play("Idle");
        }
        else
        {
            animator.Play("Jump");
        }
    }

    private void OnDisable()
    {
        controls.Avatar.Jump.performed -= Jump;
        controls.Avatar.Jump.canceled -= Jump;
    }

    private void Jump(InputAction.CallbackContext context)
    {
        if(context.performed == true)
        {
            if(controller.IsGrounded)
            {
                animator.Play("Jump");
                controller.Jump();
            }
            
        }
        else if(context.canceled)
        {
            controller.ShortenJump();
        }
        
    }

    public void EnableControls()
    {
        controls.Enable();
    }

    public void DisableControls()
    {
        controls.Disable();
    }
}