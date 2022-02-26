using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float health = 100f;

    private void Die()
    {
        GetComponent<DeathHandler>().HandleDeath();
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    public void TakeDamage(float damageAmount)
    {
        if (damageAmount <= Mathf.Epsilon) return;
        health -= damageAmount;

        if (health <= Mathf.Epsilon) Die();
    }
}
