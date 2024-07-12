using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        AudioManager.instance.PlaySFX(0, player.transform);
    }

    public override void Exit()
    {
        base.Exit();

        AudioManager.instance.StopSFX(0);
    }

    public override void Update()
    {
        base.Update();
        
        player.transform.position += playerInput.normalized * player.moveSpeed * Time.deltaTime;
        player.FlipController(playerInput.x);
        player.fx.dustTrailFX.Play();

        if (playerInput.x == 0 && playerInput.y == 0)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }    
}
