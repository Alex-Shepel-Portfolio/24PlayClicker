using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace TFPlay.UI
{
    public class UILevelScore : MonoBehaviour
    {
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private Transform scoreImage;

        [SerializeField] protected string scoreTextFormat = "F1";
        [SerializeField] private float scoreAnimationTime = 1f;

        protected float score;

        private float textScore;

        private float TextScore
        {
            get => textScore;
            set
            {
                textScore = value;
                scoreText.SetText(textScore.ToString(scoreTextFormat));
            }
        }

        public void AddScore(float value)
        {
            SetScore(score + value);
        }

        public virtual void SetScore(float value)
        {
            score = value;
        }

        public virtual void ResetScore()
        {
            score = 0f;
        }

        protected virtual void ResetScoreText()
        {
            TextScore = 0;
        }

        public void AnimateScore(float delay = 0)
        {
            StartCoroutine(AnimationCoroutine(delay));
        }

        protected virtual IEnumerator AnimationCoroutine(float delay)
        {
            ResetScoreText();

            yield return new WaitForSeconds(delay);

            DOTween.To(() => TextScore, s => TextScore = s, score, scoreAnimationTime)
                .OnComplete(() =>
                {
                    CoinsAnimator.Instance.Animate(scoreImage.position, () => SLS.Data.Game.Coins.Value += (int)score);
                });
        }
    }
}