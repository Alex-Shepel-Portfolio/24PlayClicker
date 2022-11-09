using UnityEngine;
using DG.Tweening;

namespace TFPlay.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class BaseCanvasGroupUI : BaseUIBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private float fadeTime = 0.5f;

        protected override void OnValidate()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        protected override void Init()
        {
            base.Init();
            canvasGroup.alpha = 0f;
        }

        public override void Show()
        {
            base.Show();
            canvasGroup.DOFade(1f, fadeTime).From(0f);
        }

        public override void Hide()
        {
            canvasGroup.DOFade(0f, fadeTime).From(1f).OnComplete(base.Hide);
        }
    }
}
