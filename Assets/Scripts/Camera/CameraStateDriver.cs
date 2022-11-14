using Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

public enum CameraType
{
    PersonCamera,
    GlobalCamera
}
public class CameraStateDriver :ImprovedMonoBehaviour
{

    [Space(5)]
    [SerializeField] private CinemachineVirtualCamera personCamera;
    [SerializeField] private CinemachineVirtualCamera globalCamera;
    [SerializeField] private Animator cameraState;

    private const string TableCameraState = "Person_CM";
    private const string GlobalCameraState = "Global_CM";

    public void PersonLook()
    {
        SwitchCamera(CameraType.PersonCamera);
    }

    public void GlobalLook()
    {
        SwitchCamera(CameraType.GlobalCamera);
    }

    private void SwitchCamera(CameraType cameraType)
    {
        switch (cameraType)
        {
            case CameraType.PersonCamera:
                SwitchToPersonCamera();
                break;
            case CameraType.GlobalCamera:
                SwitchToGlobalCamera();
                break;
            default:
                break;
        }
    }

    private void SwitchToPersonCamera()
    {
        SwitchAnimator(TableCameraState);
        UpCameraPriority(personCamera);
        DownCameraPriority(globalCamera);
    }
    private void SwitchToGlobalCamera()
    {
        SwitchAnimator(GlobalCameraState);
        UpCameraPriority(globalCamera);
        DownCameraPriority(personCamera);
    }

    private void SwitchAnimator(string cameraAtAnimatorName)
    {
        cameraState.Play(cameraAtAnimatorName);
    }
    private void UpCameraPriority(CinemachineVirtualCamera camera)
    {
        camera.Priority = 1;
    }
    private void DownCameraPriority(CinemachineVirtualCamera camera)
    {
        camera.Priority = 0;
    }

    public void MovePersonCamera(Transform cameraPosition)
    {
        personCamera.transform.position = cameraPosition.position;
        personCamera.transform.rotation = cameraPosition.rotation;
    }
}
