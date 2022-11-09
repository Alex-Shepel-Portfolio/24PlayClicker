using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using TFPlay.UI;

public class DeveloperPanelEnabler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
	[SerializeField]
	private BaseSettingsUI settingsUI;

	private float timeToOpen = 5;
	private Coroutine coroutine;

	public void OnPointerDown(PointerEventData eventData)
	{
		if (DeveloperPanel.Instance != null && settingsUI != null && settingsUI.IsOpened)
        {
			coroutine = StartCoroutine(WaitAndShowDeveloperPanel());
		}
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		if (coroutine != null)
        {
			StopCoroutine(coroutine);
		}
	}

	private IEnumerator WaitAndShowDeveloperPanel()
	{
		yield return new WaitForSeconds(timeToOpen);
		DeveloperPanel.Instance.TogglePanel();
	}
}
