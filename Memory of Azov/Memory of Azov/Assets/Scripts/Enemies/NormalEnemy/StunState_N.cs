using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunState_N : IEnemyState
{
    private Enemy enemy;

    private float stunTimer;

    public void Enter(Enemy e)
    {
        enemy = e;

        stunTimer = 0;

        enemy.StopMovement();
        
        enemy.ChangeAnimation(Enemy.AnimationState.Stun);

        Debug.Log("Enter stun");
        SoundManager.Instance.GhostSoundEnum(SoundManager.SoundRequestGhost.G_Stunned);
    }

    public void Execute()
    {
        stunTimer += Time.deltaTime;

        if (stunTimer >= enemy.GetStunTimer())
            enemy.ChangeState(new ChaseState_N());
    }

    public void Exit()
    {
    }
}
