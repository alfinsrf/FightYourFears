using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeastDeadState : EnemyState
{
    private Enemy_Beast enemy;

    public BeastDeadState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Beast _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        AudioManager.instance.PlaySFX(9, null);
        enemy.fx.CreatePopUpText("Congratulations!");
    }

    public override void Update()
    {
        base.Update();
        enemy.SetZeroVelocity();
    }
}
