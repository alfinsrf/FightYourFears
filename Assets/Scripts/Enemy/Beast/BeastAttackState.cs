using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeastAttackState : EnemyState
{
    private Enemy_Beast enemy;
    public BeastAttackState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Beast _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.SetZeroVelocity();
    }

    public override void Exit()
    {
        base.Exit();

        enemy.lastTimeAttacked = Time.time;
    }

    public override void Update()
    {
        base.Update();

        enemy.SetZeroVelocity();

        if (triggerCalled)
        {            
            stateMachine.ChangeState(enemy.battleState);            
        }
    }
}
