using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState 
{
    protected EnemyStateMachine stateMachine;
    protected Enemy enemyBase;
    protected Rigidbody2D rb;

    public string animatorBoolName;

    protected float stateTimer;
    protected bool triggerCalled;

    public EnemyState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animatorBoolName)
    {
        this.enemyBase = _enemyBase;
        this.stateMachine = _stateMachine;
        this.animatorBoolName = _animatorBoolName;
    }

    public virtual void Enter()
    {
        triggerCalled = false;
        rb = enemyBase.rb;
        enemyBase.animator.SetBool(animatorBoolName, true);
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
        
    }

    public virtual void Exit()
    {
        enemyBase.animator.SetBool(animatorBoolName, false);

    }
    public virtual void AnimationFinishTrigger()
    {
        triggerCalled = true;
    }
}
