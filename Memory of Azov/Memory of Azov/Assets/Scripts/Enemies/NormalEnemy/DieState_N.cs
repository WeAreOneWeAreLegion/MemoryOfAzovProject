using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieState_N : IEnemyState
{
    private Enemy enemy;

    private float timer;
    private float timeToDesapear = 2f;

    public void Enter(Enemy e)
    {
        enemy = e;

        timer = Time.time;
        e.StopMovement();

        e.ChangeAnimation(Enemy.AnimationState.Die);
    }

    public void Execute()
    {
        if (Time.time >= timer + timeToDesapear)
        {
            enemy.Die();
            timer = Time.time;
        }
    }

    public void Exit()
    {
    }
}