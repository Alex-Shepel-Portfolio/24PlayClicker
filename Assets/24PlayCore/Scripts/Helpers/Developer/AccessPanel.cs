using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AccessPanel : MonoBehaviour
{
    private const string PASSWORD = "2424";

    [SerializeField]
    private GameObject content;
    [SerializeField]
    private Button loginButton;
    [SerializeField]
    private Button closeButton;
    [SerializeField]
    private TMP_InputField passwordInputField;

    public event System.Action OnAccessGranted;
    public event System.Action OnExit;

    private void Start()
    {
        loginButton.onClick.AddListener(OnLoginButtonClicked);
        closeButton.onClick.AddListener(OnCloseButtonlicked);
    }

    private void OnLoginButtonClicked()
    {
        if (passwordInputField.text.Equals(PASSWORD))
        {
            Hide();
            OnAccessGranted?.Invoke();
        }
    }

    private void OnCloseButtonlicked()
    {
        Hide();
        OnExit?.Invoke();
    }

    public void Show()
    {
        content.SetActive();
    }

    public void Hide()
    {
        content.SetInactive();
    }
}
