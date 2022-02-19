using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour
{
    [SerializeField] private bool drawChaseRange = true;
    [SerializeField] private Transform target;
    [SerializeField] private float chaseRange = 5f;


    private NavMeshAgent _navMeshAgent;
    private float _distanceToTarget = Mathf.Infinity;
    private bool _isProvoked = false;

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }


    private void Update()
    {
        _distanceToTarget = Vector3.Distance(target.position, transform.position);

        if (_isProvoked)
        {
            EngageTarget();
        }
        else if (_distanceToTarget <= chaseRange)
        {
            _isProvoked = true;
        }
    }

    private void EngageTarget()
    {
        if (_distanceToTarget >=_navMeshAgent.stoppingDistance)
        {
            ChasseTarget();
        }
        else if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
        {
            AttackTarget();
        }
    }

    private void AttackTarget()
    {
        Debug.Log("Attacking!");
    }

    private void ChasseTarget()
    {
        _navMeshAgent.SetDestination(target.position);
    }

    private void OnDrawGizmos()
    {
        if (!drawChaseRange) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}