using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeadState : PlayerState
{
    public PlayerDeadState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void Enter()
    {
        base.Enter();

        player.gunImage.SetActive(false);
        player.SetZeroVelocity();        
        AudioManager.instance.playBgm = false;
        AudioManager.instance.PlaySFX(11, null); 
        GameObject.Find("Canvas").GetComponent<UI>().SwitchOnEndScreen();
        AudioManager.instance.PlaySFX(6, null); 
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        player.SetZeroVelocity();
    }
}
