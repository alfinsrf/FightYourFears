using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BeastBattleState : EnemyState
{
    private Enemy_Beast enemy;
    private Transform player;
    private float distance;    

    public BeastBattleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Beast _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        player = PlayerManager.instance.player.transform;

        if (player.GetComponent<PlayerStats>().isDead)
        {
            stateMachine.ChangeState(enemy.idleState);
        }        
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (enemy.bossFightBegun)
        {
            stateTimer = enemy.battleTime;

            distance = Vector2.Distance(enemy.transform.position, player.transform.position);
            Vector2 direction = player.transform.position - enemy.transform.position;            

            direction.Normalize();
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, player.transform.position, enemy.moveSpeed * Time.deltaTime);

            if (enemy.IsPlayerDetected())
            {

                if (enemy.IsPlayerDetected().distance < enemy.attackDistance)
                {
                    if (CanAttack())
                    {
                        stateMachine.ChangeState(enemy.attackState);
                    }
                    else
                    {
                        stateMachine.ChangeState(enemy.idleState);
                    }
                }
            }
            else
            {               
                if (stateTimer < 0 || Vector2.Distance(player.transform.position, enemy.transform.position) > 20)
                {
                    enemy.bossFightBegun = false;
                    stateMachine.ChangeState(enemy.idleState);
                }
            }

            if (enemy.IsPlayerDetected() && enemy.IsPlayerDetected().distance < enemy.attackDistance - 0.1f)
            {
                return;
            }
        }        
    }

    private bool CanAttack()
    {
        if (Time.time >= enemy.lastTimeAttacked + enemy.attackCooldown)
        {
            enemy.attackCooldown = Random.Range(enemy.minAttackCooldown, enemy.maxAttackCooldown);
            enemy.lastTimeAttacked = Time.time;
            return true;
        }

        return false;
    }
}
