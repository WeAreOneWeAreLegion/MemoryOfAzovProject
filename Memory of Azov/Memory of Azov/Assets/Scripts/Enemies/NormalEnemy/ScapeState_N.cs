using UnityEngine;
using UnityEditor;

public class ScapeState_N : IEnemyState
{
    private Enemy enemy;

    private float refreshTime = 0.4f; //Para optimizar el calculo del punto de huida
    private float refreshTimer;

    public void Enter(Enemy e)
    {
        enemy = e;

        enemy.SetTarget(enemy.surrogateTarget);

        refreshTimer = refreshTime;
    }

    public void Execute()
    {
        if (!enemy.IsInSight() && enemy.IsCloseToScapePoint() || !enemy.IsReceivingDamage())
            enemy.ChangeState(new ChaseState_N());

        refreshTimer += Time.deltaTime;

        if (/*refreshTimer >= refreshTime && */enemy.IsInSight())
        {
            enemy.CalculateEscapePoint();
            refreshTimer = 0;
        }

        enemy.EscapeMove();
        enemy.RotateToTarget();
    }

    public void Exit()
    {

    }
}
