using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[System.Serializable]
public class QualitySettingsData
{
    public float renderScale;
    public bool showShadows;
    public Material waterMaterial;
    public bool enablePostProcessing;
}

public class PerfomanceOptimizator : MonoBehaviour
{
    [SerializeField]
    private UniversalRenderPipelineAsset universalRenderPipelineAsset;
    [SerializeField]
    private Volume postProcessingVolume;
    [SerializeField]
    private Renderer waterRenderer;
    [SerializeField]
    private float calculaionDelay = 0.5f;
    [SerializeField]
    private float calculaionTime = 0.25f;
    [SerializeField]
    private float requiredFPSHighQuality = 50;

    [SerializeField]
    private QualitySettingsData lowQualitySettings;
    [SerializeField]
    private QualitySettingsData highQualitySettings;


    private void Start()
    {
#if UNITY_ANDROID
        GameC.Instance.OnLevelLoaded += OnLevelLoaded;
#endif
    }

    private void OnLevelLoaded(int _)
    {
        GameC.Instance.OnLevelLoaded -= OnLevelLoaded;
        StartCoroutine(CheckPerfomanceRoutine());
    }

    private void SetHighQuality()
    {
        SetQuality(highQualitySettings);
        Debug.Log("PerfomanceOptimizator - SetHighQuality");
    }

    private void SetLowQuality()
    {
        SetQuality(lowQualitySettings);
        Debug.Log("PerfomanceOptimizator - SetLowQuality");
    }

    private void SetQuality(QualitySettingsData qualitySettings)
    {
        SetShadows(qualitySettings.showShadows);
        SetWater(qualitySettings.waterMaterial);
        SetRenderSacle(qualitySettings.renderScale);
        SetPostProcessing(qualitySettings.enablePostProcessing);
    }

    private void SetQuality(float averageFramerate)
    {
        if (averageFramerate > requiredFPSHighQuality)
        {
            SetHighQuality();
        }
        else
        {
            SetLowQuality();
        }
    }

    private void SetShadows(bool showShadows)
    {
        if (universalRenderPipelineAsset != null)
        {
            QualitySettings.shadows = showShadows ? UnityEngine.ShadowQuality.All : UnityEngine.ShadowQuality.Disable;
        }
    }

    private void SetWater(Material waterMaterial)
    {
        if (waterRenderer != null)
        {
            waterRenderer.material = waterMaterial;
        }
    }

    private void SetRenderSacle(float value)
    {
        if (universalRenderPipelineAsset != null)
        {
            StartCoroutine(SetRenderScaleRoutine(universalRenderPipelineAsset.renderScale, value));
        }
    }

    private void SetPostProcessing(bool enable)
    {
        if (postProcessingVolume != null)
        {
            postProcessingVolume.enabled = enable;
        }
    }

    private IEnumerator SetRenderScaleRoutine(float fromScale, float toScale, float scaleTime = 1f)
    {
        float percent = 0f;
        float speed = 1f / scaleTime;
        while (percent < 1f)
        {
            percent += speed * Time.unscaledDeltaTime;
            universalRenderPipelineAsset.renderScale = Mathf.Lerp(fromScale, toScale, percent);
            yield return null;
        }
    }

    private IEnumerator CheckPerfomanceRoutine()
    {
        yield return new WaitForSecondsRealtime(calculaionDelay);
        yield return CalculateAverageFramerateRoutine(SetQuality);
    }


    private IEnumerator CalculateAverageFramerateRoutine(Action<float> onCalculationCompleted)
    {
        List<float> fpsList = new List<float>();
        float t = calculaionTime;
        float timer = 0f;
        float refresh = 0f;
        float averageFramerate = 60f;
        while (t > 0f)
        {
            t -= Time.unscaledDeltaTime;

            float timelapse = Time.smoothDeltaTime;
            timer = timer <= 0 ? refresh : timer -= timelapse;
            if (timer <= 0)
            {
                averageFramerate = (int)(1f / timelapse);
            }

            fpsList.Add(averageFramerate);
            yield return null;
        }
        if (onCalculationCompleted != null)
        {
            float averageFps = fpsList.Average();
            Debug.Log("PerfomanceOptimizator - AverageFPS: " + averageFps);
            onCalculationCompleted(averageFps);
        }
    }
}