using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CultistABattleState : EnemyState
{
    private Transform player;
    private Enemy_CultistA enemy;
    private int moveDirection;
    public CultistABattleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animatorBoolName, Enemy_CultistA _enemy) : base(_enemyBase, _stateMachine, _animatorBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        player = GameObject.Find("Player").transform;

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (enemy.IsPlayerDetected())
        {
            stateTimer = enemy.battleTime;

            if (enemy.IsPlayerDetected().distance < enemy.attackDistance)
            {
                if (CanAttack())
                    stateMachine.ChangeState(enemy.attackState);
            }
        }
        else 
        {
            if (stateTimer < 0 || Vector2.Distance(player.transform.position, enemy.transform.position) > 7)
                stateMachine.ChangeState(enemy.idleState);
        }

        if (player.position.x > enemy.transform.position.x)
            moveDirection = 1;
        else if (player.position.x < enemy.transform.position.x)
            moveDirection = -1;

        enemy.SetVelocity(enemy.moveSpeed * moveDirection, rb.velocity.y);
    }

    private bool CanAttack()
    {
        if (Time.time >= enemy.lastTimeAttacked + enemy.attackCooldown)
        {
            enemy.lastTimeAttacked = Time.time;
            return true;
        }
       
        return false; 
    }
}
