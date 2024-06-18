using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_CultistA : Enemy
{

    #region States

    public CultistAIdleState idleState { get; private set; }
    public CultistAMoveState moveState { get; private set; }
    public CultistABattleState battleState { get; private set; }
    public CultistAAttackState attackState { get; private set; }
    public CultistAStaggeredState staggeredState { get; private set; }
    public CultistADeathState deathState { get; private set; }
    #endregion


    protected override void Awake()
    {
        base.Awake();

        idleState = new CultistAIdleState(this, stateMachine, "Idle", this);
        moveState = new CultistAMoveState(this, stateMachine, "Move", this);
        battleState = new CultistABattleState(this, stateMachine, "Move", this);
        attackState = new CultistAAttackState(this, stateMachine, "Attack", this);
        staggeredState = new CultistAStaggeredState(this, stateMachine, "Stun", this);
        deathState = new CultistADeathState(this, stateMachine, "Die", this);
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.U))
            stateMachine.ChangeState(staggeredState);
    }

    public override bool CanBeStunned()
    {
        if (base.CanBeStunned())
        {
            stateMachine.ChangeState(staggeredState);
            return true;
        }
        return false;
    }

    public override void Die()
    {
        base.Die();

        stateMachine.ChangeState(deathState);
    }
}
