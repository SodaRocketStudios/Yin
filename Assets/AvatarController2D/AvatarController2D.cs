using UnityEngine;
using UnityEngine.Events;

// To Do:
// Determine how much of the move function will be left up to user, and how much is handled in this script.
//      Should at least handle moving on slopes and eventually steps.
// Add a step offset option

namespace srs.AvatarController
{
    /// <summary>
    ///    2D avatar controller
    /// </summary>
    [RequireComponent(typeof(CapsuleCollider2D), typeof(Rigidbody2D))]
    public class AvatarController2D : MonoBehaviour
    {
        /// <summary>
        ///     The current velocity of the avatar.
        /// </summary>
        public Vector2 Velocity {get => avatarRigidbody.velocity;}

        /// <summary>
        ///     Returns true when the avatar is on the ground.
        /// </summary>
        public bool IsGrounded {get => collisions.isGrounded;}

        /// <summary>
        ///     Returns true if the avatar was on the ground at the last check.
        /// </summary>
        public bool WasGrounded {get => collisions.wasGrounded;}

        public UnityEvent OnLanding;

        /// <summary>
        ///     Returns true when the avatar is on a slope.
        /// </summary>
        public bool IsOnSlope {get => collisions.isOnSlope;}

        /// <summary>
        ///     The maximum slope angle that the avatar can climb.
        /// </summary>
        public float SlopeLimit
        {
            get => slopeLimit;
            set => slopeLimit = Mathf.Max(90, value);
        }
        [SerializeField, Range(0, 90)]
        private float slopeLimit = 45;

        /// <summary>
        ///     The Layer that will be treated as ground.
        ///
        ///     Make sure to exclude the layer assigned to the player collider.
        /// </summary>
        public LayerMask GroundLayer
        {
            get => groundLayer; 
            set => groundLayer = value;
        }
        [SerializeField]
        private LayerMask groundLayer;

        // The size of the ground check boxcast.
        [SerializeField, Tooltip("The size of the ground check boxcast.")]
        private Vector2 groundCheckSize = new Vector2(0.5f, 0.1f);

        [SerializeField] private float jumpHeight;
        [SerializeField] private float timeToJumpApex;
        [SerializeField, Min(0)] private float fallGravityMultiplier = 1;
        private float jumpVelocity;
        private bool isJumping;

        private CapsuleCollider2D avatarCollider;
        private Rigidbody2D avatarRigidbody;

        private CollisionInfo collisions;

        private Vector2 velocity;
        private float defaultGravityScale;


        private void Awake()
        {
            // Get Character Components
            avatarCollider = GetComponent<CapsuleCollider2D>();
            avatarRigidbody = GetComponent<Rigidbody2D>();

            // Get the default gravity scale
            defaultGravityScale = avatarRigidbody.gravityScale;

            // determine the jump parameters.
            Physics2D.gravity = jumpHeight/(Mathf.Pow(timeToJumpApex, 2))*Vector2.down;
            jumpVelocity = Mathf.Sqrt(-2*jumpHeight*Physics2D.gravity.y*defaultGravityScale);

            // Stop the character from rotating around the z-axis.
            avatarRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;

            // Stop the landing event from firing on start.
            collisions.isGrounded = true;

            if(OnLanding == null)
            {
                OnLanding = new UnityEvent();
            }
        }

        private void FixedUpdate()
        {
            collisions.Reset();
            GroundCheck();

            avatarRigidbody.gravityScale = collisions.isGrounded?0:defaultGravityScale;

            bool isFalling = avatarRigidbody.velocity.y < 0 || isJumping == false && collisions.isGrounded == false;

            if(isFalling == true)
            {
                avatarRigidbody.gravityScale = defaultGravityScale*fallGravityMultiplier;
            }
        }

        /// <summary>
        ///     Move the avatar at the specified velocity.
        ///
        ///     If acceleration time is set, it will take that long to reach the target velocity.
        /// </summary>
        public void Move(Vector2 moveVelocity, float accelerationTime = 0)
        {
            // Reset all collision info.
            collisions.Reset();
            GroundCheck();

            float xVelocity = moveVelocity.x;

            // Keep the velocity due to gravity
            moveVelocity.y = avatarRigidbody.velocity.y;

            // If the player is on a slope.
            if(collisions.isOnSlope == true)
            {
                // If the slope isn't too steep
                if(collisions.beyondSlopeLimit == false)
                {
                    // Move up the slope at a constant speed.
                    moveVelocity = Vector2.SmoothDamp(avatarRigidbody.velocity, collisions.tangent*moveVelocity.x, ref velocity, accelerationTime);

                    if(isJumping == true)
                    {
                        moveVelocity.y = avatarRigidbody.velocity.y;
                    }

                    avatarRigidbody.isKinematic = xVelocity == 0;
                }
            }
            else
            {
                moveVelocity.x = Mathf.SmoothDamp(avatarRigidbody.velocity.x, moveVelocity.x, ref velocity.x, accelerationTime);
            }

            avatarRigidbody.velocity = moveVelocity;
        }

        public void Jump()
        {
            Vector2 moveVelocity = jumpVelocity*Vector2.up + avatarRigidbody.velocity*Vector2.right;
            avatarRigidbody.velocity = moveVelocity;
            isJumping = true;
        }

        public void ShortenJump()
        {
            isJumping = false;
        }

        // Check if the avatar is on the ground.
        private void GroundCheck()
        {
            Vector3 pos = avatarCollider.bounds.center + Vector3.down*(avatarCollider.size.y/2 + groundCheckSize.y/2.0f);
            RaycastHit2D hit = Physics2D.BoxCast(pos, groundCheckSize, 0, Vector2.down, 0, groundLayer);

            collisions.normal = hit.normal;
            collisions.tangent = -Vector2.Perpendicular(collisions.normal);

            collisions.slopeAngle = Mathf.Abs(Vector2.Angle(Vector2.up, collisions.normal));

            // If the avatar is grounded
            if(hit)
            {
                collisions.isGrounded = true;

                // The avatar landed
                if(collisions.wasGrounded == false)
                {
                    if(OnLanding != null)
                    {
                        OnLanding.Invoke();
                    }
                }

                if(Mathf.Abs(collisions.slopeAngle) > 0)
                {
                    collisions.isOnSlope = true;

                    if(Mathf.Abs(collisions.slopeAngle) > slopeLimit)
                    {
                        collisions.beyondSlopeLimit = true;
                    }
                }
            }
        }

        private void OnDrawGizmos()
        {
            CapsuleCollider2D collider = GetComponent<CapsuleCollider2D>();
            Vector3 pos = collider.bounds.center;
            pos.y -= (collider.size.y/2 + groundCheckSize.y/2.0f);

            Gizmos.color = Color.red;

            // Draw the ground check boxcast
            Gizmos.DrawWireCube(pos, new Vector3(groundCheckSize.x, groundCheckSize.y, 0));

            Gizmos.color = Color.green;

            // Draw the ground normal.
            Gizmos.DrawLine(pos, (Vector2)pos + collisions.normal);

            Gizmos.color = Color.blue;

            // Draw the ground tangent
            Gizmos.DrawLine(pos, (Vector2)pos + collisions.tangent);
        }

        private struct CollisionInfo
        {
            public bool isGrounded;
            public bool wasGrounded;
            public bool isOnSlope;
            public bool beyondSlopeLimit;
            public float slopeAngle;

            public Vector2 normal;
            public Vector2 tangent;

            public void Reset()
            {
                wasGrounded = isGrounded;
                isGrounded = false;
                isOnSlope = false;
                beyondSlopeLimit = false;
                slopeAngle = 0;
                normal = Vector2.zero;
                tangent = Vector2.zero;
            }
        }
    }
}