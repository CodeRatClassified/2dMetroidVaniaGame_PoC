using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : Entity
{
    [Header("Attack info")]
    public Vector2[] attackMovement;
    public float parryDuration;

    [Header("Fall info")]
    [SerializeField] public float minimumFallDistance = 3f;
    [SerializeField] public float damageMultiplier = 5f;
    public bool IsWallSlidingOn { get; set; } = false;
    public float lastGroundedPositionY;
    private bool wasGroundedLastFrame;
   
    public bool isBusy { get; private set; }
    [Header("Move info")]
    public float moveSpeed = 7f;
    public float jumpForce = 15f;


    [Header("Dash info")]
    [SerializeField] private float dashCooldown;
    private float dashTimer;
    public float dashSpeed;
    public float dashDuration;
    public float dashDirection { get; private set; }

    #region States
    public PlayerStateMachine stateMachine { get; private set; } 
    

    public PlayerIdleState idlesState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerWallSlideState wallSlideState { get; private set; }
    public PlayerWallJumpState wallJumpState { get; private set; }
    public PlayerPrimaryAttackState primaryAttackState { get; private set; } 
    public PlayerParryState parryState { get; private set; }
    public PlayerDeathState deathState { get; private set; }





    #endregion

    protected override void Awake()
    {
        base.Awake();

        stateMachine = new PlayerStateMachine();
        

        idlesState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        airState  = new PlayerAirState(this, stateMachine, "Jump");
        dashState = new PlayerDashState(this, stateMachine, "Dash");
        wallSlideState = new PlayerWallSlideState(this, stateMachine, "WallSlide");
        wallJumpState = new PlayerWallJumpState(this, stateMachine, "Jump");
        primaryAttackState = new PlayerPrimaryAttackState(this, stateMachine, "Attack");
        parryState = new PlayerParryState(this, stateMachine, "Parry");
        deathState = new PlayerDeathState(this, stateMachine, "Die");

    }

    protected override void Start()
    {
        base.Start();

        stateMachine.Initialize(idlesState);

    }


    protected override void Update()
    {
        base.Update();

        stateMachine.currentState.Update();

        CheckForDashInput();

        if(!IsWallSlidingOn)
            CheckFallDamage();
       
    }

    public IEnumerator BusyFor(float _seconds)
    {
        isBusy = true;
        
        yield return new WaitForSeconds(_seconds);

        isBusy = false;
        
    }

    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();

    private void CheckForDashInput()
    {
        if (IsWallDetected())
            return;


        dashTimer -= Time.deltaTime;


        if (Input.GetKeyDown(KeyCode.LeftShift) && dashTimer < 0 && characterAttributes.currentStamina >= characterAttributes.dashStaminaCost)
        {

            characterAttributes.UseStamina(characterAttributes.dashStaminaCost);
            dashTimer = dashCooldown;

            dashDirection = Input.GetAxisRaw("Horizontal");

            if (dashDirection == 0)
                dashDirection = facingDirection;

            characterAttributes.StopStaminaRegeneration();
            StartCoroutine(ResumeStaminaRegen(dashDuration));
            stateMachine.ChangeState(dashState);
        }
    }

    private void CheckFallDamage()
    {
        bool isGrounded = IsGroundDetected();
        if (isGrounded && !IsWallSlidingOn)
        {
            float fallDistance = lastGroundedPositionY - transform.position.y;
            if (fallDistance > minimumFallDistance)
                {
                    int fallDamage = Mathf.FloorToInt(fallDistance * damageMultiplier);
                    characterAttributes.TakeDamage(fallDamage);
                }
                lastGroundedPositionY = transform.position.y;
        }
    }
    public IEnumerator ResumeStaminaRegen(float delay)
    {
        yield return new WaitForSeconds(delay);
        characterAttributes.StartStaminaRegeneration();
    }

    public override void Die()
    {
        base.Die();

        stateMachine.ChangeState(deathState);
    }
}
