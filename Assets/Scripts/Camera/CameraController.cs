using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEditor.Rendering;
using UnityEngine;

public class CameraController : MonoSingleton<CameraController>
{
    [SerializeField] private CameraStateDriver cameraStateDriver;
    [SerializeField] private CameraZoom zoomCamera;
    [SerializeField] private Transform cameraHolder;
    [SerializeField] private float  inputStrength;
    [SerializeField] private Vector3 minPositionLimit;
    [SerializeField] private Vector3 maxPositionLimit;
    private Vector2  movementBounds;
    private Vector3 cameraPosition;
    private Vector3 mainPositon;

    private void OnDestroy()
    {
        InputSystem.Instance.OnDragAction -= OnDragAction;
    }

    protected override void Awake()
    {
        base.Awake();
        mainPositon = cameraHolder.position;
        cameraPosition = cameraHolder.position;
    }

    public void Init()
    {
        EventManager.InputEvent.OnDrag.AddListener(OnDragAction);
        EventManager.InputEvent.OnDoubleTouch.AddListener(OnZooming);

    }

    public void PersonLookCamera()
    {
        cameraStateDriver.PersonLook();
        zoomCamera.BackMainZoom();
    }
    public void GlobalLookCamera()
    {
        cameraStateDriver.GlobalLook();
    }
    

    private void OnDragAction(Vector2 drag)
    {
        var positionToSet = cameraHolder.position - new Vector3(drag.x, 0, drag.y) * inputStrength;
        positionToSet = new Vector3(Mathf.Clamp(positionToSet.x, minPositionLimit.x, maxPositionLimit.x), positionToSet.y,
            Mathf.Clamp(positionToSet.z, minPositionLimit.z, maxPositionLimit.z));
        cameraPosition = positionToSet;
    }

    private void OnZooming(float distance)
    {
        zoomCamera.ChangeZoom(distance);
    }
    
    private void FixedUpdate()
    {
        cameraHolder.position = cameraPosition;
    }

    public void MovePersonCamera(Transform cameraPosition)
    {
        cameraStateDriver.MovePersonCamera(cameraPosition);
    }
}
