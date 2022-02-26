using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float damageAmount = 40f;

    private PlayerHealth _target;

    private void Start()
    {
        _target = FindObjectOfType<PlayerHealth>();
    }

    public void AttackHitEvent()
    {
        if (!_target) return;
        _target.TakeDamage(damageAmount);
    }

    public void OnDamageTaken()
    {
    }
}
