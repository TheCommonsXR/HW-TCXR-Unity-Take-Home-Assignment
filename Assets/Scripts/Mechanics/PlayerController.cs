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

        bool jump;
        Vector2 move;
        SpriteRenderer spriteRenderer;
        internal Animator animator;
        readonly PlatformerModel model = Simulation.GetModel<PlatformerModel>();
        
        // Fields for damage effect
        private SpriteRenderer sprite;
        private Coroutine hurtEffectCoroutine;
        private Color originalSpriteColor;
        
        // Fields for firing gun
        [SerializeField] private int gunDamage = 1;
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private float projectileSpeed = 5f;

        public Bounds Bounds => collider2d.bounds;

        void Awake()
        {
            health = GetComponent<Health>();
            audioSource = GetComponent<AudioSource>();
            collider2d = GetComponent<Collider2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
            sprite = GetComponent<SpriteRenderer>();
        }

        override protected void OnEnable() {
            base.OnEnable();
            GameDelegates.OnGameModeChanged += OnGameModeChanged;
        }

        override protected void OnDisable() {
            base.OnDisable();
            GameDelegates.OnGameModeChanged -= OnGameModeChanged;
        }

        override protected void Start() {
            originalSpriteColor = sprite.color;
        }

        protected override void Update()
        {
            UpdateJumpState();
            base.Update();

            if (!controlEnabled) {
                move.x = 0;
                return;
            }
            
            // Using old Unity input system for firing gun
            if (Input.GetKeyDown(KeyCode.F)) {
                GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                PlayerProjectile projectileScript = projectile.GetComponent<PlayerProjectile>();
                projectileScript.Initialize(sprite.flipX ? Vector3.left : Vector3.right, gunDamage, projectileSpeed);
            }
            
            move.x = Input.GetAxis("Horizontal");
            if (jumpState == JumpState.Grounded && Input.GetButtonDown("Jump"))
                jumpState = JumpState.PrepareToJump;
            else if (Input.GetButtonUp("Jump"))
            {
                stopJump = true;
                Schedule<PlayerStopJump>().player = this;
            }
            
            
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
        }

        public enum JumpState
        {
            Grounded,
            PrepareToJump,
            Jumping,
            InFlight,
            Landed
        }

        public void DoHurtEffect(float duration = 0.5f)
        {
            audioSource.PlayOneShot(ouchAudio);
            if (hurtEffectCoroutine != null) {
                StopCoroutine(hurtEffectCoroutine);
                hurtEffectCoroutine = null;
            }
            StartCoroutine(HurtEffectCoroutine(duration));
        }

        private IEnumerator HurtEffectCoroutine(float duration)
        {
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(duration);
            spriteRenderer.color = originalSpriteColor;
        }

        private void OnGameModeChanged(GameMode newGameMode) {
            maxSpeed = newGameMode.MaxPlayerSpeed;
            jumpTakeOffSpeed = newGameMode.PlayerJumpTakeoffSpeed;
            gunDamage = newGameMode.PlayerBulletDamage;
            projectileSpeed = newGameMode.PlayerBulletSpeed;
            Time.timeScale = newGameMode.SimulationSpeed;
            model.spawnPoint.transform.position = newGameMode.SpawnPosition;
            health.maxHP = newGameMode.PlayerHealth;
            health.SetHP(health.maxHP);
            health.SetDamageCooldown(newGameMode.PlayerImmunityDurationWhenDamaged);
            model.jumpModifier = newGameMode.GlobalJumpModifier;
            model.jumpDeceleration = newGameMode.GlobalJumpDeceleration;
        }
        
    }
}