using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwakeState_N : IEnemyState
{
    private Enemy enemy;

    private float timer;
    private float timeWaiting = 4f;

    public void Enter(Enemy e)
    {   
        enemy = e;

        enemy.PlayAwakenSound();
        enemy.ChangeAnimation(Enemy.AnimationState.Appear);
        enemy.Invencible();

        enemy.StopMovement();

        timer = Time.time;
    }

    public void Execute()
    {

        if (Time.time >= timer + timeWaiting)
            enemy.ChangeState(new ChaseState_N());

    }

    public void Exit()
    {
        enemy.Vulnerable();
    }
}