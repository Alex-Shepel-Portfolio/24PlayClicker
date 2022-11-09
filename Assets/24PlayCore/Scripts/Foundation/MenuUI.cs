using TFPlay.UI;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoSingleton<MenuUI>
{
    [SerializeField] private GameObject uiHolder;
    [SerializeField] private WinUI winUI;
    [SerializeField] private LoseUI loseUI;
    [SerializeField] private LevelUI levelUI;
    [SerializeField] private CoinsUI coinsUI;

    public GameObject UIHolder => uiHolder;

    private void Start()
    {
        GameC.Instance.OnInitCompleted += Init;
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
}