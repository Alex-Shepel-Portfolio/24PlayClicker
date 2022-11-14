using System;
using System.Collections;
using System.Collections.Generic;
using TFPlay.UI;
using UnityEngine;

public class InteractivePanel : BaseUIBehaviour
{
    public override void Show()
    {
        base.Show();
        ClosePanelController.Instance?.AddInteractivePanel(this);
    }

    protected virtual void OnDisable()
    {
        GameC.Instance.OnInitCompleted -= Init;
    }
}
