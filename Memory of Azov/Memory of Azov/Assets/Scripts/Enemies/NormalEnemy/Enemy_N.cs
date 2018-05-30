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
    }

    public override void SetUpEnemyVariables(EnemySO enemyData, ObjectsManager.ItemRequest dropItem)
    {
        base.SetUpEnemyVariables(enemyData, dropItem);


        if (currentState != null)
            ChangeState(new AwakeState_N());
    }

    public override void RecieveDamage()
    {
        if (currentState.GetType() != typeof(ScapeState_N))
            ChangeState(new ScapeState_N());

        base.RecieveDamage();
    }
    #endregion
}
