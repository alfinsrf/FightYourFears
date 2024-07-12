using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    public PlayerDashState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        if(player.GetComponent<PlayerStats>().isDead)
        {
            stateMachine.ChangeState(player.deadState);
        }

        base.Enter();

        stateTimer = player.dashDuration;

        player.stats.MakeInvincible(true);
        AudioManager.instance.PlaySFX(10, null);
    }

    public override void Exit()
    {
        base.Exit();

        player.SetZeroVelocity();
        player.stats.MakeInvincible(false);
    }

    public override void Update()
    {
        base.Update();
        
        if(playerInput.y == 0)
        {
            player.SetVelocity(player.dashSpeed * player.dashDir, 0);
        }
        else if(playerInput.x == 0)
        {
            player.SetVelocity(0, player.dashSpeed * playerInput.y);
        }
        else if(playerInput.x != 0 && playerInput.y != 0)
        {
            player.SetVelocity(player.dashSpeed * player.dashDir , player.dashSpeed * playerInput.y);
        }
        
        if(stateTimer < 0)
        {
            stateMachine.ChangeState(player.idleState);
        }

        player.fx.CreateAfterImage();
    }
}
