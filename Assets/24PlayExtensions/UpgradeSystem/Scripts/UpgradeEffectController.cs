using DG.Tweening;
using UnityEngine;

namespace TFPlay.UpgradeSystem
{
    [DefaultExecutionOrder(-1)]
    public class UpgradeEffectController : MonoBehaviour
    {
        [SerializeField]
        private ParticleSystem[] levelUpEffects;
        [SerializeField]
        private float upgradeAnimationTime = 1f;
        [SerializeField]
        private float upgradeAnimationSize = 1.1f;
        [SerializeField]
        private Ease upgradeEase = Ease.OutElastic;

        private Transform target;
        private Vector3 initialTargetSize;

        private void Start()
        {
            UpgradeController.Instance.OnTargetInitialized += OnTargetInitialized;
            UpgradeController.Instance.OnUpgraded += OnUpgraded;
        }

        private void OnTargetInitialized(Transform target)
        {
            this.target = target;
            initialTargetSize = target.localScale;
            for (int i = 0; i < levelUpEffects.Length; i++)
            {
                levelUpEffects[i].transform.position = target.position;
            }
        }

        private void OnUpgraded(UpgradeType upgradeType)
        {
            levelUpEffects[(int)upgradeType].Play();
            PlayUpgradeAnimation();
        }

        private void PlayUpgradeAnimation()
        {
            target.DOScale(initialTargetSize, upgradeAnimationTime).SetEase(upgradeEase).From(initialTargetSize * upgradeAnimationSize);
        }
    }
}
