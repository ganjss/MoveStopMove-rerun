using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
    [SerializeField] Rigidbody rbEnemy;
    [SerializeField] NavMeshAgent navMeshAgent;

    private IState currentState;
    private Vector3 destination;

    public bool IsDestionation => Vector3.Distance(currentTransform.position, destination + (currentTransform.position.y - destination.y) * Vector3.up) < 0.1f;

    private void Start()
    {
        OnInit();
    }

    private void OnInit()
    {
        ChangeWeapon(WeaponType.Hammer);
        destination = currentTransform.position;
    }

    private void Update()
    {
        //Moving();

        Attack();

        if (currentState != null)
        {
            currentState.OnExecute(this);
        }
    }

    public void ChangeState(IState newState)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }

        currentState = newState;

        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }

    public void StopMoving()
    {
        rbEnemy.velocity = Vector3.zero;
    }

    public void Moving()
    {
        if (IsDestionation)
        {
            destination = RandomNavmeshLocation(20f);
            SetDestination(destination);
            ChangeAnimRun();
        }
    }

    public void SetDestination(Vector3 destination)
    {
        this.destination = destination;
        navMeshAgent.SetDestination(destination);
    }

    public Vector3 RandomNavmeshLocation(float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }
}
