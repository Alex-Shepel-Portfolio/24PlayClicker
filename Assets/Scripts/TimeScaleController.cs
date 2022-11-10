using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TimeScaleController : ImprovedMonoBehaviour
{
    public event System.Action<float> OnTimeScaleChanged;
    
    [Header("Click per second")]
    // max i could recieve in internet test is 7 cps
    [SerializeField] private float minCPS = 3;
    [SerializeField] private float maxCPS = 7; 
    [SerializeField] private float cpsLerp = 0.5f;
    [SerializeField, Tooltip("After hom many seconds CPS become 0")] private float cooldownDuration = 5;
    [SerializeField] private float minInterval = 0.001f;
    [Header("Time scale")]
    [SerializeField] private float maxTimeScaleMultiplier = 2;
    [SerializeField] private AnimationCurve difficultyCurve;
    [Header("VFX")]
    [SerializeField] private ParticleSystem speedLines;
    [SerializeField] private float minEmission = 5;
    [SerializeField] private float maxEmission = 50;
    
    private float currentCPS;

    private float lastInterval;
    private float lastClickTime;

    private Coroutine cooldownCoroutine;
    private float cooldownCounter;

    private float baseTimeScale;
    private const float defaultTimeScale = 1;
    private ParticleSystem.EmissionModule emission;
    
    private float TimeScaleMultiplier =>
        1 + difficultyCurve.Evaluate(currentCPS / maxCPS) * (maxTimeScaleMultiplier - 1);
    public float CurrentTimeScale =>
        baseTimeScale * TimeScaleMultiplier;

    public void Init()
    {
        currentCPS = 0;
        cooldownCounter = 0;
        emission = speedLines.emission;
        SetParticles();
        EventManager.InputEvent.OnClick.AddListener(OnTap);
    }

    private void OnTap()
    {
        float newClickTime = Time.time;
        lastInterval = Mathf.Clamp(newClickTime - lastClickTime, minInterval, float.MaxValue);
        lastClickTime = newClickTime;

        cooldownCounter = cooldownDuration;
        UpdateCPS();
        if (cooldownCoroutine == null)
            cooldownCoroutine = StartCoroutine(CooldownCPS());
    }

    private IEnumerator CooldownCPS()
    {
        while (cooldownCounter > 0)
        {
            UpdateCPS();

            cooldownCounter -= Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        currentCPS = 0;
        cooldownCoroutine = null;
    }

    private void UpdateCPS()
    {
        float currentInterval = Time.time - lastClickTime;
        float newCPS = 1 / Mathf.Max(lastInterval, currentInterval);
        currentCPS = Mathf.Lerp(currentCPS, newCPS, cpsLerp);

        UpdateTimeScale();
        SetParticles();
    }

    private void UpdateTimeScale()
    {
        OnTimeScaleChanged?.Invoke(CurrentTimeScale);
    }
    
    public void SetParameters(float speedWork)
    {
        SetBaseTimeScale(speedWork);
        UpdateTimeScale();
    }

    private void SetBaseTimeScale(float speedWork) => baseTimeScale = defaultTimeScale * speedWork;

    private void SetParticles()
    {
        if (currentCPS < minCPS)
        {
            emission.rateOverTime = 0;
        }
        else
        {
            emission.rateOverTime =
                Mathf.Lerp(minEmission, maxEmission,
                Mathf.InverseLerp(minCPS, maxCPS, currentCPS));
        }  
    }
}
