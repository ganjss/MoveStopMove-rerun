using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState
{
    float randomTime;
    float timer;

    public void OnEnter(Enemy enemy)
    {

    }

    public void OnExecute(Enemy enemy)
    {
        if (timer < randomTime)
        {
            enemy.Moving();
        }
        else
        {
            enemy.ChangeState(new IdleState());
        }

        timer += Time.deltaTime;
    }

    public void OnExit(Enemy enemy)
    {

    }
}
