using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_N : Enemy
{
    #region Override Methods
    protected override void Start()
    {
        base.Start();

        ChangeState(new AwakeState_N());
    }

    protected override void Update()
    {
        base.Update();

        if (IsReceivingDamage() && currentState.GetType() != typeof(AwakeState_N))
            RecieveDamage();

        if (!GameManager.Instance.player.IsPlayerAlive())
        {
            ChangeState(new IdleState_N());
            enabled = false;
        }
    }

    public override void SetUpEnemyVariables(EnemySO enemyData, ObjectsManager.ItemRequest dropItem)
    {
        base.SetUpEnemyVariables(enemyData, dropItem);


        if (currentState != null)
            ChangeState(new AwakeState_N());
    }

    public override void RecieveDamage()
    {
        if (currentState.GetType() != typeof(ScapeState_N) && currentState.GetType() != typeof(StunState_N) && 
			currentState.GetType() != typeof(DieState_N) && currentState.GetType() != typeof(AwakeState_N))
            ChangeState(new ScapeState_N());

        base.RecieveDamage();
    }
    #endregion
}
