using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace TFPlay.UpgradeSystem
{
	//[DefaultExecutionOrder(1)]
	public class UpgradeMenuTutorial : MonoBehaviour
	{
		private const string UpgradeTutorialPassedKey = "UpgradeTutorialPassed";

		[SerializeField]
		private int showFromLevel = 2;
		[SerializeField]
		private UpgradeButtonUI tutorialUpgradeButton;
		[SerializeField]
		private UpgradeButtonUI[] otherUpgradeButtons;
		[SerializeField]
		private Image blinkImage;
		[SerializeField]
		private Image background;
		[SerializeField]
		private float blinkTime = 1f;
		[SerializeField]
		private float hideContentTime = 1f;

		private Tween fadeButtonTween;
		private float defaultBackgroundAlpha;

		private bool IsTutorialPassed
		{
			get => PlayerPrefs.GetInt(UpgradeTutorialPassedKey, 0) == 1;
			set
			{
				PlayerPrefs.SetInt(UpgradeTutorialPassedKey, value ? 1 : 0);
				PlayerPrefs.Save();
			}
		}

		private void Start()
		{
			defaultBackgroundAlpha = background.color.a;
			blinkImage.SetInactive();
			background.SetInactive();
			GameC.Instance.OnLevelStartLoading += OnLevelStartLoading;
		}

		private void OnLevelStartLoading(int level)
		{
			if (level < showFromLevel)
				return;

			if (IsTutorialPassed)
				return;

			Show();
		}

		private void Show()
		{
			tutorialUpgradeButton.OnPurchased += UpgradeButton_OnPurchased;
			foreach (var upgradeButton in otherUpgradeButtons)
			{
				upgradeButton.SetInteractable(false);
			}
			background.SetActive();
			background.DOFade(defaultBackgroundAlpha, blinkTime).From(0f);
			blinkImage.SetActive();
			blinkImage.transform.SetParent(tutorialUpgradeButton.transform);
			var blinkImageRectTransform = blinkImage.GetRectTransform();
			blinkImageRectTransform.anchoredPosition = blinkImageRectTransform.sizeDelta = Vector2.zero;
			fadeButtonTween = blinkImage.DOFade(0f, blinkTime).From(0.5f).SetLoops(-1, LoopType.Yoyo);
			this.DoAfterNextFrameCoroutine(OverrideSortingUpgradeButton);
		}

		private void UpgradeButton_OnPurchased(UpgradeType upgradeType)
		{
			tutorialUpgradeButton.OnPurchased -= UpgradeButton_OnPurchased;
			fadeButtonTween.Kill();
			Destroy(tutorialUpgradeButton.GetComponent<GraphicRaycaster>());
			Destroy(tutorialUpgradeButton.GetComponent<Canvas>());
			blinkImage.DOFade(0f, hideContentTime).OnComplete(blinkImage.SetInactive);
			background.DOFade(0f, hideContentTime).OnComplete(background.SetInactive);
			IsTutorialPassed = true;
		}

		private void OverrideSortingUpgradeButton()
		{
			var graphicRaycaster = tutorialUpgradeButton.gameObject.AddComponent<GraphicRaycaster>();
			var canvas = graphicRaycaster.GetComponent<Canvas>();
			canvas.overrideSorting = true;
			canvas.sortingOrder = 99;
			canvas.additionalShaderChannels = AdditionalCanvasShaderChannels.Normal
				| AdditionalCanvasShaderChannels.Tangent
				| AdditionalCanvasShaderChannels.TexCoord1
				| AdditionalCanvasShaderChannels.TexCoord2
				| AdditionalCanvasShaderChannels.TexCoord3;
		}
	}
}