using System;
using UnityEngine;

public class GameC : MonoSingleton<GameC>
{
    public event Action OnInitCompleted;
    public event Action OnShowFadeUI;
    public event Action<int> OnLevelStartLoading;
    public event Action<int> OnLevelLoaded;
    public event Action<bool> OnLevelEnd;
    public event Action OnLevelStarted;

    private void Start()
    {
        LevelsController.Instance.OnLevelLoaded += InvokeOnLevelLoaded;
        Application.targetFrameRate = 60;
        AnalyticsHelper.Init();
        LateStart();
    }

    private void LateStart()
    {
        this.DoAfterNextFrameCoroutine(() =>
        {
            OnInitCompleted?.Invoke();
            LoadLevel();
        });
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            LevelEnd(true);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            LevelEnd(false);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            SLS.Data.Game.Coins.Value += 10000;
        }
    }

    private void OnApplicationQuit()
    {
        MonoBehaviour[] scripts = FindObjectsOfType<MonoBehaviour>();
        foreach (MonoBehaviour script in scripts)
            script.enabled = false;
    }
#endif

    public void InvokeOnStartLevelLoading()
    {
        var levelNumber = SLS.Data.Game.Level.Value;
        AnalyticsHelper.StartLevel();
        OnLevelStartLoading?.Invoke(levelNumber);
    }

    private void InvokeOnLevelLoaded(int sceneId)
    {
        OnLevelLoaded?.Invoke(sceneId);
        Taptic.Light();
    }

    public void LevelStart()
    {
        OnLevelStarted?.Invoke();
    }

    public void LevelEnd(bool playerWin)
    {
        if (playerWin)
        {
            Taptic.Success();
            AnalyticsHelper.CompleteLevel();
        }
        else
        {
            Taptic.Failure();
            AnalyticsHelper.FailLevel();
        }

        OnLevelEnd?.Invoke(playerWin);
    }

    public void LoadLevel()
    {
        SLS.Save();
        OnShowFadeUI?.Invoke();
    }

    public void NextLevel()
    {
        UnloadLevel(true);
        LoadLevel();
    }

    public void RestartLevel()
    {
        UnloadLevel(false);
        LoadLevel();
    }

    private void UnloadLevel(bool nextLvl)
    {
        if (nextLvl)
        {
            SLS.Data.Game.Level.Value++;
        }
    }
}