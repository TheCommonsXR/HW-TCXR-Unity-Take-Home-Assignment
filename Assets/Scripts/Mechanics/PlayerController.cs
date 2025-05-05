using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Gameplay;
using static Platformer.Core.Simulation;
using Platformer.Model;
using Platformer.Core;

namespace Platformer.Mechanics
{
    /// <summary>
    /// This is the main class used to implement control of the player.
    /// It is a superset of the AnimationController class, but is inlined to allow for any kind of customisation.
    /// </summary>
    public class PlayerController : KinematicObject
    {
        public AudioClip jumpAudio;
        public AudioClip respawnAudio;
        public AudioClip ouchAudio;
        public GameObject bulletPrefab;
        /// <summary>
        /// Max horizontal speed of the player.
        /// </summary>
        public float maxSpeed = 7;
        /// <summary>
        /// Initial jump velocity at the start of a jump.
        /// </summary>
        public float jumpTakeOffSpeed = 7;

        public JumpState jumpState = JumpState.Grounded;
        private bool stopJump;
        /*internal new*/ public Collider2D collider2d;
        /*internal new*/ public AudioSource audioSource;
        public Health health;
        public bool controlEnabled = true;
        public bool isImmune = false;
        [SerializeField]
        float immunityDuration = 1.0f;
        [SerializeField]
        float timeBetweenShots = 0.5f; // Time between shots in seconds
        [SerializeField]
        int bulletDamage = 1;
        bool facingRight = true;
        bool jump;
        bool canFire = true; // Flag to check if the player can fire
        Vector2 move;
        SpriteRenderer spriteRenderer;
        internal Animator animator;
        readonly PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        public Bounds Bounds => collider2d.bounds;

        void Awake()
        {
            health = GetComponent<Health>();
            audioSource = GetComponent<AudioSource>();
            collider2d = GetComponent<Collider2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
        }

        protected override void Update()
        {
            if (controlEnabled)
            {
                move.x = Input.GetAxis("Horizontal");
                if (jumpState == JumpState.Grounded && Input.GetButtonDown("Jump"))
                    jumpState = JumpState.PrepareToJump;
                else if (Input.GetButtonUp("Jump"))
                {
                    stopJump = true;
                    Schedule<PlayerStopJump>().player = this;
                }
                if (Input.GetButton("Fire1") && canFire) // Was bound to left ctrl, I changed it to left shift. But if that doesn't get committed left ctrl should work too.
                {
                    // If player's velocity is positive, fire to the right. If it's negative, fire to the left.
                    // If the player is not moving, fire to the last direction the player was moving.
                    GameObject bulletObject = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                    Bullet bulletComponent = bulletObject.GetComponent<Bullet>();
                    bulletComponent.SetDirection(facingRight ? Vector2.right : Vector2.left);
                    bulletComponent.SetDamage(bulletDamage);
                    StartCoroutine(StartFireCooldown());
                }
            }
            else
            {
                move.x = 0;
            }
            UpdateJumpState();
            base.Update();
        }

        void UpdateJumpState()
        {
            jump = false;
            switch (jumpState)
            {
                case JumpState.PrepareToJump:
                    jumpState = JumpState.Jumping;
                    jump = true;
                    stopJump = false;
                    break;
                case JumpState.Jumping:
                    if (!IsGrounded)
                    {
                        Schedule<PlayerJumped>().player = this;
                        jumpState = JumpState.InFlight;
                    }
                    break;
                case JumpState.InFlight:
                    if (IsGrounded)
                    {
                        Schedule<PlayerLanded>().player = this;
                        jumpState = JumpState.Landed;
                    }
                    break;
                case JumpState.Landed:
                    jumpState = JumpState.Grounded;
                    break;
            }
        }

        protected override void ComputeVelocity()
        {
            if (jump && IsGrounded)
            {
                velocity.y = jumpTakeOffSpeed * model.jumpModifier;
                jump = false;
            }
            else if (stopJump)
            {
                stopJump = false;
                if (velocity.y > 0)
                {
                    velocity.y = velocity.y * model.jumpDeceleration;
                }
            }

            if (move.x > 0.01f)
                spriteRenderer.flipX = false;
            else if (move.x < -0.01f)
                spriteRenderer.flipX = true;

            animator.SetBool("grounded", IsGrounded);
            animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

            targetVelocity = move * maxSpeed;
            // Use the player's velocity to determine the direction the player is facing. If it's 0, just don't change the direction.
            if (velocity.x > 0)
            {
                facingRight = true;
            }
            else if (velocity.x < 0)
            {
                facingRight = false;
            }
        }

        public enum JumpState
        {
            Grounded,
            PrepareToJump,
            Jumping,
            InFlight,
            Landed
        }

        public void StartImmunityTimeCoroutine()
        {
            StartCoroutine(ImmunityTime());
        }

        IEnumerator ImmunityTime()
        {
            isImmune = true;
            yield return new WaitForSeconds(immunityDuration);
            isImmune = false;
        }

        IEnumerator StartFireCooldown()
        {
            canFire = false;
            yield return new WaitForSeconds(timeBetweenShots);
            canFire = true;
        }

    }
}