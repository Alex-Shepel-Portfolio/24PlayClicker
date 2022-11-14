using TFPlay.UI;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoSingleton<MenuUI>
{
    [SerializeField] private GameObject uiHolder;
    [SerializeField] private WinUI winUI;
    [SerializeField] private LoseUI loseUI;
    [SerializeField] private LevelUI levelUI;
    [SerializeField] private Button exitPersonModeButton;
    [SerializeField] private CoinsUI coinsUI;
    [SerializeField] private LevelProgressBar levelProgressBar;
    public LevelProgressBar LevelProgressBar => levelProgressBar;
    public GameObject UIHolder => uiHolder;

    private void Start()
    {
        GameC.Instance.OnInitCompleted += Init;
        exitPersonModeButton.onClick.AddListener(ExitPersonMode);
        exitPersonModeButton.SetInactive();
    }

    private void ExitPersonMode()
    {
        exitPersonModeButton.SetInactive();
        SceneController.Instance.ExitPersonMode();
    }

    private void Init()
    {
        GameC.Instance.OnLevelEnd += OnLevelEnd;
    }

    private void OnLevelEnd(bool playerWon)
    {
        if (playerWon)
        {
            winUI.Show();
        }
        else
        {
            loseUI.Show();
        }
    }

    public void ActiveExitPersonModeButton()
    {
        exitPersonModeButton.SetActive();
    }
}