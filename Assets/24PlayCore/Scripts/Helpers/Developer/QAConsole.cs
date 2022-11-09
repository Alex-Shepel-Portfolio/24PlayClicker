using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class QAConsole : MonoBehaviour
{
    [SerializeField] private GameObject content;
    [SerializeField] private Canvas canvas;
    [SerializeField] private Button showQAConsoleButton;
    [Header("Level")]
    [SerializeField] private TMP_InputField levelInputField;
    [SerializeField] private Button loadLevelButton;
    [SerializeField] private TextMeshProUGUI levelNameText;
    [SerializeField] private TextMeshProUGUI levelNumberText;
    [SerializeField] private TextMeshProUGUI buildVersionText;
    [Header("Coins")]
    [SerializeField] private Button addCoinsButton;
    [SerializeField] private Button removeCoinsButton;
    [SerializeField] private int coinAmount;
    [Header("FPS")]
    [SerializeField] private Button showFPSButton;
    [SerializeField] private GameObject FPSCounter;
    [Header("Core")]
    [SerializeField] private Button winLevelButton;
    [SerializeField] private Button loseLevelButton;
    [Header("UI")]
    [SerializeField] private Button toggleMenuUIButton;
    [SerializeField] private TextMeshProUGUI toggleMenuUIText;

    private bool isOpen = false;

    private void Start()
    {
        loadLevelButton.onClick.AddListener(LoadLevel);
        showQAConsoleButton.onClick.AddListener(ToggleConsole);

        addCoinsButton.onClick.AddListener(AddCoins);
        removeCoinsButton.onClick.AddListener(ClearCoins);
        showFPSButton.onClick.AddListener(ToggleFPSCounter);
        winLevelButton.onClick.AddListener(WinLevel);
        loseLevelButton.onClick.AddListener(LoseLevel);
        toggleMenuUIButton.onClick.AddListener(ToggleMenuUI);
    }

    public void Show()
    {
        canvas.enabled = true;
    }

    public void Hide()
    {
        canvas.enabled = false;
    }

    private void ToggleConsole()
    {
        isOpen = !isOpen;
        content.SetActive(isOpen);
        if (isOpen)
        {
            ShowInformation();
        }
    }

    private void ToggleFPSCounter()
    {
        FPSCounter.SetActive(!FPSCounter.activeSelf);
    }

    private void LoadLevel()
    {
        if (int.TryParse(levelInputField.text, out int levelNumber))
        {
            isOpen = false;
            content.SetActive(false);
            SLS.Data.Game.Level.Value = levelNumber;
            GameC.Instance.LoadLevel();
        }
    }

    private void ShowInformation()
    {
        if (SceneManager.sceneCount > 1)
        {
            levelNameText.text = string.Format("SCENE NAME: {0}", SceneManager.GetSceneAt(1).name);
            levelNumberText.text = string.Format("SCENE NUMBER IN BUILD: {0}", SceneManager.GetSceneAt(1).buildIndex);
        }

        buildVersionText.text = string.Format("BUILD VERSION: {0}", Application.version);
    }

    private void AddCoins()
    {
        SLS.Data.Game.Coins.Value += coinAmount;
    }

    private void ClearCoins()
    {
        SLS.Data.Game.Coins.Value = 0;
    }

    private void WinLevel()
    {
        GameC.Instance.LevelEnd(true);
    }

    private void LoseLevel()
    {
        GameC.Instance.LevelEnd(false);
    }

    private void ToggleMenuUI()
    {
        var uiHolderActive = !MenuUI.Instance.UIHolder.activeSelf;
        toggleMenuUIText.text = uiHolderActive ? "Hide UI" : "Show UI";
        MenuUI.Instance.UIHolder.SetActive(uiHolderActive);
    }
}