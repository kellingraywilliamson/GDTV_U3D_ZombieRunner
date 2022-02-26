using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class WeaponZoom : MonoBehaviour
{
    [SerializeField] private float zoomFOV = 20f;
    [SerializeField] private float defaultZoom = 40f;

    private CinemachineVirtualCamera _virtualCamera;
    private bool _zoomed = false;

    // Start is called before the first frame update
    void Start()
    {
        _virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
    }

    private void OnDisable()
    {
        ZoomOut();
    }


    public void OnZoom()
    {
        if (!_virtualCamera) return;

        if (_zoomed)
        {
            ZoomOut();
        }
        else
        {
            ZoomIn();
        }
    }

    private void ZoomIn()
    {
        _virtualCamera.m_Lens.FieldOfView = zoomFOV;
        _zoomed = true;
    }

    private void ZoomOut()
    {
        _virtualCamera.m_Lens.FieldOfView = defaultZoom;
        _zoomed = false;
    }
}
