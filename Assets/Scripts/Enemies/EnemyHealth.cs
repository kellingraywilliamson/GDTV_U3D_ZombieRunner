using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float hitPoints = 100f;
    private Animator _animator;
    private static readonly int Die = Animator.StringToHash("Die");
    public bool IsDead { get; private set; }

    private void Start()
    {
        
        _animator = GetComponent<Animator>();
    }

    public void TakeDamage(float damageAmount)
    {
        if (damageAmount <= 0 || hitPoints <= 0) return;
        
        BroadcastMessage("OnDamageTaken");

        hitPoints -= damageAmount;

        if (hitPoints<=0) Killed();
    }

    private void Killed()
    {
        if (IsDead) return;
        IsDead = true;
        _animator.SetTrigger(Die);
    }
}
