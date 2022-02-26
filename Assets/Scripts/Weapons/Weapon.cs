using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class Weapon : MonoBehaviour
{
    private Camera fpCamera;
    [SerializeField] private float range = 100f;
    [SerializeField] private bool drawRangeGizmo = false;
    [SerializeField] private float damageAmount = 25f;
    [SerializeField] private float timeBetweenShots = 1f;
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private GameObject hitEffect;
    [SerializeField] private Ammo ammoSlot;
    [SerializeField] private AmmoType ammoType;

    private bool _canShoot = true;

    private void Start()
    {
        fpCamera = FindObjectOfType<Camera>();
        muzzleFlash = GetComponentInChildren<ParticleSystem>();
    }

    private void OnEnable()
    {
        _canShoot = true;
    }

    public void OnFire(InputValue value)
    {
        StartCoroutine(Shoot());
    }

    private IEnumerator Shoot()
    {
        if (ammoSlot.GetCurrentAmmo(ammoType) > 0 && _canShoot)
        {
            PlayMuzzleFlash();
            ProcessRaycast();
            ammoSlot.ReduceCurrentAmmo(ammoType);
        }

        _canShoot = false;
        yield return new WaitForSeconds(timeBetweenShots);
        _canShoot = true;
    }

    private void PlayMuzzleFlash()
    {
        if (!muzzleFlash)
        {
            Debug.LogWarning("No particles found!");
        }

        muzzleFlash.Play();
    }

    private void ProcessRaycast()
    {
        RaycastHit hit;
        var cameraTransform = fpCamera.transform;
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, range))
        {
            CreateHitImpact(hit);
            var target = hit.transform.GetComponent<EnemyHealth>();
            if (!target) return;
            target.TakeDamage(damageAmount);
        }
    }

    private void CreateHitImpact(RaycastHit hit)
    {
        var effect = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(effect, 1f);
    }
    
    
    private void OnDrawGizmos()
    {
        if (!drawRangeGizmo) return;
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
