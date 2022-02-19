using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Camera FPCamera;
    [SerializeField] private float range = 100f;
    [SerializeField] private float damageAmount = 25f;
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private GameObject hitEffect;

    private void Start()
    {
        muzzleFlash = GetComponentInChildren<ParticleSystem>();
    }

    public void OnFire(InputValue value)
    {
        Shoot();
    }

    private void Shoot()
    {
        PlayMuzzleFlash();
        ProcessRaycast();
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
        var cameraTransform = FPCamera.transform;
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
}
