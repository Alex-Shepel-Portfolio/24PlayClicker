using UnityEngine;

public class WorkerAnimator : ImprovedMonoBehaviour
{
    [SerializeField] private Animator workerAnimator;
    [SerializeField] private float workerOffsetAnimator = 1;

    private float minAnimSpeed = 0.2f;
    private float maxAnimSpeed =20f;
    
    
    public void ChangeAnimationSpeed(float timeWork)
    {
        timeWork *= workerOffsetAnimator;
        var workSpeedToSet = Mathf.Clamp(timeWork, minAnimSpeed, maxAnimSpeed);
        workerAnimator.speed = workSpeedToSet;
    }
}