using DG.Tweening;
using UnityEngine;

namespace TFPlay.UI
{
    public class CoinsPulseUI : MonoBehaviour
    {
        [SerializeField]
        private Transform[] pulseObjects;
        [SerializeField]
        private Vector2 pulseMinMax = new Vector2(1f, 1.25f);
        [SerializeField]
        private float pulseDuration = 0.5f;

        private int previousCoinsCount;
        private Sequence sequence;

        private void Start()
        {
            SLS.Data.Game.Coins.OnValueChanged += Coins_OnValueChanged;
            previousCoinsCount = SLS.Data.Game.Coins.Value;
        }

        private void Coins_OnValueChanged(int coinsCount)
        {
            if (coinsCount > previousCoinsCount)
            {
                sequence?.Kill();
                sequence = DOTween.Sequence();
                for (int i = 0; i < pulseObjects.Length; i++)
                {
                    sequence.Join(pulseObjects[i].DOScale(pulseMinMax.y, pulseDuration).From(pulseMinMax.x).SetEase(Ease.OutElastic));
                }
            }
            previousCoinsCount = coinsCount;
        }
    }
}
