using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState_N : IEnemyState
{
    private Enemy enemy;

    public void Enter(Enemy e)
    {
        enemy = e;

        e.ChangeAnimation(Enemy.AnimationState.Move);
    }

    public void Execute()
    {
    }

    public void Exit()
    {
    }
}