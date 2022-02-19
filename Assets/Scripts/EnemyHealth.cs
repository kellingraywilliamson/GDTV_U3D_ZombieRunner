using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float hitPoints = 100f;

    public void TakeDamage(float damageAmount)
    {
        if (damageAmount <= 0 || hitPoints <= 0) return;

        hitPoints -= damageAmount;

        if (hitPoints<=0) Killed();
    }

    private void Killed()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
