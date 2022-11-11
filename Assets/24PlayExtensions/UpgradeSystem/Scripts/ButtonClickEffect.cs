using UnityEngine;
using DG.Tweening;

namespace TFPlay.UpgradeSystem
{
    public class ButtonClickEffect : MonoBehaviour
    {
        [SerializeField]
        private Transform content;
        [Header("Success")]
        [SerializeField]
        private float scaleDuration = 0.5f;
        [SerializeField]
        private Ease scaleEase = Ease.OutElastic;
        [SerializeField]
        private float popUpScale = 0.9f;
        [Header("Fail")]
        [SerializeField]
        private float shakeDuration = 0.3f;
        [SerializeField]
        private float shakeStrenght = 10f;
        [SerializeField]
        private int shakeVibratio = 30;

        private Tween effectTween;
        private IButtonClickEffectReceiver receiver;

        private void Awake()
        {
            receiver = GetComponent<IButtonClickEffectReceiver>();
        }

        private void OnEnable()
        {
            receiver.OnClicked += Receiver_OnClick;
        }

        private void OnDisable()
        {
            receiver.OnClicked -= Receiver_OnClick;
        }

        private void Receiver_OnClick(bool successfull)
        {
            KillEffectTween();
            if (successfull)
            {
                PlaySuccessful();
            }
            else
            {
                PlayFailed();
            }
            Taptic.Selection();
        }

        private void PlayFailed()
        {
            content.localRotation = Quaternion.identity;
            effectTween = content.DOShakeRotation(shakeDuration, Vector3.forward * shakeStrenght, shakeVibratio);
        }

        private void PlaySuccessful()
        {
            effectTween = content.DOScale(Vector3.one, scaleDuration).From(Vector3.one * popUpScale).SetEase(scaleEase);
        }

        private void KillEffectTween()
        {
            if (effectTween != null)
            {
                effectTween.Kill();
            }
        }
    }
}
