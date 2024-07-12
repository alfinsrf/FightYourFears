using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Beast : Enemy
{
    #region States
    public BeastIdleState idleState { get; private set; }
    public BeastBattleState battleState { get; private set; }
    public BeastAttackState attackState { get; private set; }
    public BeastDeadState deadState { get; private set; }    
    #endregion

    public bool bossFightBegun;

    protected override void Awake()
    {
        base.Awake();

        idleState = new BeastIdleState(this, stateMachine, "Idle", this);
        battleState = new BeastBattleState(this, stateMachine, "Move", this);
        attackState = new BeastAttackState(this, stateMachine, "Attack", this);
        deadState = new BeastDeadState(this, stateMachine, "Die", this);

    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void Die()
    {
        base.Die();

        stateMachine.ChangeState(deadState);
    }
}
