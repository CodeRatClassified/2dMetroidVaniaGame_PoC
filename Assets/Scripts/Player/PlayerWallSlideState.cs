using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerState
{
    public PlayerWallSlideState(Player _player, PlayerStateMachine _stateMachine, string _animatorBoolName) : base(_player, _stateMachine, _animatorBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.IsWallSlidingOn = true;

        player.lastGroundedPositionY = player.transform.position.y;
    }

    public override void Exit()
    {
        base.Exit();
        player.IsWallSlidingOn = false;

        player.lastGroundedPositionY = player.transform.position.y;
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.ChangeState(player.wallJumpState);
            return;

        }
        if (xInput != 0 && player.facingDirection != xInput)
                stateMachine.ChangeState(player.idlesState);
        if (yInput < 0)
            rb.velocity = new Vector2(0, rb.velocity.y);
        else
            rb.velocity = new Vector2(0, rb.velocity.y * 0.7f);

        if(player.IsGroundDetected())
            stateMachine.ChangeState(player.idlesState);
    }
}
