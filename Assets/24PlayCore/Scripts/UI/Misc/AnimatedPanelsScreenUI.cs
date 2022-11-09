using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace TFPlay.UI
{
    public class AnimatedPanelsScreenUI : BaseCanvasGroupUI
    {
        [SerializeField] private List<UIElementAppearData> panels = new List<UIElementAppearData>();

        public override void Show()
        {
            base.Show();
            StartCoroutine(AppearCoroutine());
        }

        public override void Hide()
        {
            KillElementsTweens();
            base.Hide();
        }

        public override void HideInstant()
        {
            KillElementsTweens();
            base.HideInstant();

            foreach (var element in panels)
                element.elementTransform.localScale = Vector3.zero;
        }

        private IEnumerator AppearCoroutine()
        {
            KillElementsTweens();

            foreach (var element in panels)
                element.elementTransform.localScale = Vector3.zero;

            foreach (var element in panels)
            {
                yield return new WaitForSecondsRealtime(element.appearElementDelay);

                element.elementTransform.DOScale(element.endValue, element.appearElementTime)
                     .SetEase(element.appearElementEase)
                     .From(element.startValue);
            }
        }

        private void KillElementsTweens()
        {
            foreach (var element in panels)
                element.elementTransform.DOKill();
        }
    }
}