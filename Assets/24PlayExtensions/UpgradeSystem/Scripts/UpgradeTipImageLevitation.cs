using DG.Tweening;
using UnityEngine;

namespace TFPlay.UpgradeSystem
{
    public class UpgradeTipImageLevitation : MonoBehaviour
    {
        [SerializeField]
        private float duration = 1f;
        [SerializeField]
        private Ease ease = Ease.InOutSine;

        private UpgradeButtonUI upgradeButton;
        private Tween tween;

        private void Awake()
        {
            upgradeButton = GetComponentInParent<UpgradeButtonUI>();
        }

        private void Start()
        {
            SLS.Data.Game.Coins.OnValueChanged += Coins_OnValueChanged;
            Coins_OnValueChanged(SLS.Data.Game.Coins.Value);
            tween = transform.GetRectTransform().DOAnchorPos(Vector3.zero, duration).SetEase(ease).SetLoops(-1, LoopType.Yoyo);
        }

        private void OnDestroy()
        {
            KillTween();
        }

        private void Coins_OnValueChanged(int coins)
        {
            if (upgradeButton.HasEnoughMoney())
            {
                Play();
            }
            else
            {
                Stop();
            }
        }

        private void Play()
        {
            gameObject.SetActive();
        }

        private void Stop()
        {
            gameObject.SetInactive();
        }

        private void KillTween()
        {
            if (tween != null)
            {
                tween.Kill();
                tween = null;
            }
        }
    }
}
