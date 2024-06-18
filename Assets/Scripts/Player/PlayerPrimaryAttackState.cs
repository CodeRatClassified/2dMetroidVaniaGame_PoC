using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimaryAttackState : PlayerState
{

    private int comboCounter;

    private float lastTimeAttacked;
    private float comboWindow = 2;

    public CharacterAttributes characterAttributes;
    
    public PlayerPrimaryAttackState(Player _player, PlayerStateMachine _stateMachine, string _animatorBoolName) : base(_player, _stateMachine, _animatorBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        if (player.characterAttributes.currentStamina > player.characterAttributes.attackStaminaCost)
        {
            player.characterAttributes.UseStamina(player.characterAttributes.attackStaminaCost);
            player.characterAttributes.StopStaminaRegeneration();
            player.StartCoroutine(player.ResumeStaminaRegen(0f)); 
        }
        else
        {
            comboCounter = 0;
            player.animator.SetInteger("ComboCounter", comboCounter);
            stateMachine.ChangeState(player.idlesState);
            return;  
        }

        xInput = 0;

        if (comboCounter > 2 || Time.time >= lastTimeAttacked + comboWindow)
            comboCounter = 0;

        player.animator.SetInteger("ComboCounter", comboCounter);


        #region Attack Direction
        float attackDirection = player.facingDirection;

        if(xInput != 0)
            attackDirection = xInput;

        #endregion


        player.SetVelocity(player.attackMovement[comboCounter].x * attackDirection, player.attackMovement[comboCounter].y);


        stateTimer = 0.1f;
    }

    public override void Exit()
    {
        base.Exit();

        player.StartCoroutine("BusyFor", 0.15f);
        

        comboCounter++;

        lastTimeAttacked = Time.time;
        
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
            player.SetZeroVelocity();

        if (triggerCalled)
            stateMachine.ChangeState(player.idlesState);
    }
}
