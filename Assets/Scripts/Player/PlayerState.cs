using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected Rigidbody2D rb;
    
    public Vector3 playerInput;

    protected PlayerStateMachine stateMachine;
    protected Player player;

    private string animBoolName;

    protected bool triggerCalled;
    
    protected float stateTimer;

    public PlayerState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName)
    {
        this.player = _player;
        this.stateMachine = _stateMachine;
        this.animBoolName = _animBoolName;
    }

    public virtual void Enter()
    {
        player.anim.SetBool(animBoolName, true);
        rb = player.rb;
        triggerCalled = true;
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
        
        playerInput = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
    }

    public virtual void Exit()
    {
        player.anim.SetBool(animBoolName, false);
    }

    public virtual void AnimationFinishTrigger()
    {
        triggerCalled = true;
    }
}
