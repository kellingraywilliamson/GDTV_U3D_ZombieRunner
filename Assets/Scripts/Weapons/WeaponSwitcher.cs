using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    private Weapon[] _weapons;
    public int currentWeaponIndex = 0;

    private void Awake()
    {
        _weapons = GetComponentsInChildren<Weapon>();
    }

    private void Start()
    {
        if (!_weapons.Any()) return;

        foreach (var weapon in _weapons)
        {
            weapon.gameObject.SetActive(false);
        }

        _weapons[currentWeaponIndex].gameObject.SetActive(true);
    }

    private void ChangeWeapon()
    {
        var currentWeapon = _weapons.First(x => x.gameObject.activeInHierarchy);
        if (currentWeapon)
        {
            currentWeapon.gameObject.SetActive(false);
        }

        _weapons[currentWeaponIndex].gameObject.SetActive(true);
    }

    public void OnNextWeapon()
    {
        currentWeaponIndex++;
        if (currentWeaponIndex >= _weapons.Length)
        {
            currentWeaponIndex = 0;
        }

        ChangeWeapon();
    }

    public void OnPreviousWeapon()
    {
        currentWeaponIndex--;
        if (currentWeaponIndex < 0)
        {
            currentWeaponIndex = _weapons.Length - 1;
        }

        ChangeWeapon();
    }
}
