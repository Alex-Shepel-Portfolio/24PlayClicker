using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeveloperPanel : MonoSingleton<DeveloperPanel>
{
    [SerializeField]
    private AccessPanel accessPanel;
    [SerializeField]
    private QAConsole qaConsole;
    [SerializeField]
    private Reporter reporter;

    private void Start()
    {
        accessPanel.OnAccessGranted += AccessPanel_OnAccessGranted;
        accessPanel.OnExit += AccessPanel_OnExit;
    }

    private void AccessPanel_OnAccessGranted()
    {
        qaConsole.Show();
        reporter.Show();
    }

    private void AccessPanel_OnExit()
    {
        qaConsole.Hide();
        reporter.Hide();
    }

    public void TogglePanel()
    {
        accessPanel.Show();
    }
}
