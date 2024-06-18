using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CultistADeathState : EnemyState
{
    private Enemy_CultistA enemy;
    public CultistADeathState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animatorBoolName, Enemy_CultistA enemy) : base(_enemyBase, _stateMachine, _animatorBoolName)
    {
        this.enemy = enemy;
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
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

        enemy.SetZeroVelocity();
    }
}
