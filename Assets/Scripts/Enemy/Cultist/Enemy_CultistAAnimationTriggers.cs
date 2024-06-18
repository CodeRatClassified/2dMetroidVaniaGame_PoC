using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_CultistAAnimationTriggers : MonoBehaviour
{
    private Enemy_CultistA enemy => GetComponentInParent<Enemy_CultistA>();

    private void AnimationTrigger()
    {
        enemy.AnimationFinishTrigger();
    }

    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(enemy.attackCheck.position, enemy.attackCheckRadius);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Player>() != null)
            {
                PlayerStats target = hit.GetComponent<PlayerStats>();
                enemy.characterAttributes.DoDamage(target);
                
            }  
        }
    }

    private void CounterWindow() => enemy.CounterAttackWindow();
    private void CloseCounterWindow() => enemy.CloseCounterAttackWindow();
}
