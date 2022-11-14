using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraZoom : ImprovedMonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera camera;
    [SerializeField] private float minZoom = 60;
    [SerializeField] private float maxZoom = 20;
    [SerializeField] private float zoomOffset = 0.1f;

    public void ChangeZoom(float zoom)
    {
        #if UNITY_EDITOR
        zoomOffset = 10;
        #endif
        var zoomValue = camera.m_Lens.FieldOfView + zoom * zoomOffset;
        camera.m_Lens.FieldOfView = Mathf.Clamp(zoomValue, maxZoom, minZoom);
    }

    public void BackMainZoom()
    {
        camera.m_Lens.FieldOfView = minZoom;
    }
}
