using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClosePanelController : MonoSingleton<ClosePanelController>
{
   [SerializeField]private List<InteractivePanel> activedPanels = new List<InteractivePanel>();
   [SerializeField] private UIMouseClickDetection mouseClickDetection;

   private void Start()
   {
      mouseClickDetection.OnClicked += CloseLastActivePanel;
   }

   public void AddInteractivePanel(InteractivePanel panel)
   {
      activedPanels.Add(panel);
   }
   
   public void CloseInteractivePanel(InteractivePanel panel)
   {
      panel.Hide();
   }

   private void CloseLastActivePanel(Vector2 clickPoint)
   {
      if(activedPanels.Count - 1 < 0){return;}
      var panelToClose = activedPanels[activedPanels.Count - 1];
      activedPanels.Remove(panelToClose);
      CloseInteractivePanel(panelToClose);
   }
   
   
}
