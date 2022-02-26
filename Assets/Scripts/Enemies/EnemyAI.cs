using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour
{
    [SerializeField] private bool drawChaseRange = true;
    [SerializeField] private Transform target;
    [SerializeField] private float chaseRange = 5f;
    [SerializeField] private float turnSpeed = 5f;


    private NavMeshAgent _navMeshAgent;
    private float _distanceToTarget = Mathf.Infinity;
    private bool _isProvoked = false;
    private Animator _animator;
    private EnemyHealth _enemyHealth;
    private static readonly int MoveParamName = Animator.StringToHash("Move");
    private static readonly int AttackParamName = Animator.StringToHash("Attack");

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _enemyHealth = GetComponent<EnemyHealth>();
    }


    private void Update()
    {
        if (!target) return;
        if (_enemyHealth.IsDead)
        {
            _navMeshAgent.enabled = false;
            enabled = false;
        }
        
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
        _animator.SetBool(AttackParamName, true);
        if (_distanceToTarget >=_navMeshAgent.stoppingDistance)
        {
            ChaseTarget();
        }
        else if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
        {
            AttackTarget();
        }
    }

    private void AttackTarget()
    {
        FaceTarget();
    }

    private void ChaseTarget()
    {
        _animator.SetBool(AttackParamName, false);
        _animator.SetTrigger(MoveParamName);
        _navMeshAgent.SetDestination(target.position);
    }

    private void OnDrawGizmos()
    {
        if (!drawChaseRange) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }

    private void FaceTarget()
    {
        var direction = (target.position - transform.position).normalized;
        var lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }

    public void OnDamageTaken()
    {
        _isProvoked = true;
    }
}
