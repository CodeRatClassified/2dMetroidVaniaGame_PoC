using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CultistAMoveState : CultistAGroundedState
{
    public CultistAMoveState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animatorBoolName, Enemy_CultistA _enemy) : base(_enemyBase, _stateMachine, _animatorBoolName, _enemy)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        enemy.SetVelocity(enemy.moveSpeed * enemy.facingDirection, rb.velocity.y);

        if (enemy.IsWallDetected() || !enemy.IsGroundDetected())
        {
            enemy.Flip();
            stateMachine.ChangeState(enemy.idleState);
        }
    }
}
