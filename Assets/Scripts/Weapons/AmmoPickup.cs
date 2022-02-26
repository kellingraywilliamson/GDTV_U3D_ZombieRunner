using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [SerializeField] private AmmoType ammoType;
    [SerializeField] private int ammoAmount = 5;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PickupTriggered(other.gameObject);
        }
    }

    private void PickupTriggered(GameObject player)
    {
        var ammo = player.GetComponent<Ammo>();
        if (!ammo) return;
        
        ammo.IncreaseCurrentAmount(ammoType, ammoAmount);
        Destroy(gameObject);
    }
}
