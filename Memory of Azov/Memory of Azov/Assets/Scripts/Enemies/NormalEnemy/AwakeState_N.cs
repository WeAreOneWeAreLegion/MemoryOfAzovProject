using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwakeState_N : IEnemyState
{
    private Enemy enemy;

    private float timer;
    private float timeWaiting = 2.5f;

    public void Enter(Enemy e)
    {   
        enemy = e;

        enemy.PlayAwakenSound();
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