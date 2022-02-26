using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;

public class Ammo : MonoBehaviour
{
    [System.Serializable]
    private class AmmoSlot
    {
        public AmmoType AmmoType;
        public int AmmoAmount;
    }

    [SerializeField] private AmmoSlot[] ammoSlots;

    public int GetCurrentAmmo(AmmoType ammoType)
    {
        return GetAmmoSlot(ammoType).AmmoAmount;
    }

    public void ReduceCurrentAmmo(AmmoType ammoType)
    {
        var slot = GetAmmoSlot(ammoType);
        slot.AmmoAmount--;
    }

    public void IncreaseCurrentAmount(AmmoType ammoType, int ammoAmount)
    {
        if (ammoAmount <= 0) return;

        var slot = GetAmmoSlot(ammoType);
        if (slot == null) return;

        slot.AmmoAmount += ammoAmount;
    }

    private AmmoSlot GetAmmoSlot(AmmoType ammoType)
    {
        return ammoSlots.FirstOrDefault(x => x.AmmoType == ammoType);
    }
}
