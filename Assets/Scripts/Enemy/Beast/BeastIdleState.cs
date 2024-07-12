using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeastIdleState : EnemyState
{
    private Enemy_Beast enemy;
    private Transform player;

    public BeastIdleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Beast _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;        
    }

    public override void Enter()
    {
        base.Enter();
        enemy.SetZeroVelocity();
        stateTimer = enemy.idleTime;
        player = PlayerManager.instance.player.transform;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (Vector2.Distance(player.transform.position, enemy.transform.position) < 10)
        {
            enemy.bossFightBegun = true;
            AudioManager.instance.PlaySFX(6, null);
        }        

        if (stateTimer < 0 && enemy.bossFightBegun)
        {
            stateMachine.ChangeState(enemy.battleState);
        }
    }    
}
