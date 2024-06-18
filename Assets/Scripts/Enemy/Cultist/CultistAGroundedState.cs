using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CultistAGroundedState : EnemyState
{
    protected Enemy_CultistA enemy;

    protected Transform player;

    public CultistAGroundedState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animatorBoolName, Enemy_CultistA _enemy) : base(_enemyBase, _stateMachine, _animatorBoolName)
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

        if (enemy.IsPlayerDetected() || Vector2.Distance(enemy.transform.position, player.position) < 2)
            stateMachine.ChangeState(enemy.battleState);
    }
}
