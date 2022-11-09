using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace TFPlay.UI
{
    public class UILevelScoreBonus : UILevelScore
    {
        [SerializeField] private TMP_Text totalText;
        [SerializeField] private TMP_Text bonusText;

        [SerializeField] private string bonusTextFormat = "F1";

        [SerializeField] private float bonusAnimationTime = .5f;
        [SerializeField] private float totalAnimationTime = 1f;
        
        [SerializeField] private float delayBetweenAnimations = .3f;

        private float bonus;
        private float total;
        
        private float textBonus;
        private float textTotal;

        private float TextBonus
        {
            get => textBonus;
            set
            {
                textBonus = value;
                bonusText.SetText(textBonus.ToString(bonusTextFormat));
            }
        }

        private float TextTotal
        {
            get => textTotal;
            set
            {
                textTotal = value;
                totalText.SetText(textTotal.ToString(scoreTextFormat));
            }
        }

        public void AddBonus(float value)
        {
            SetBonus(bonus + value);
        }
        
        public void SetBonus(float value)
        {
            bonus = value;
            CalculateTotalScore();
        }

        public override void SetScore(float value)
        {
            base.SetScore(value);
            CalculateTotalScore();
        }

        private void CalculateTotalScore()
        {
            total = score * bonus;
        }

        public override void ResetScore()
        {
            base.ResetScore();
            bonus = 0f;
            total = 0f;
        }

        protected override void ResetScoreText()
        {
            base.ResetScoreText();
            TextBonus = 0;
            TextTotal = 0;
        }

        protected override IEnumerator AnimationCoroutine(float delay)
        {
            base.AnimationCoroutine(delay);
            
            yield return new WaitForSeconds(delayBetweenAnimations);

            DOTween.To(() => TextBonus, b => TextBonus = b, bonus, bonusAnimationTime);

            yield return new WaitForSeconds(delayBetweenAnimations);

            DOTween.To(() => TextTotal, t => TextTotal = t, total, totalAnimationTime);
        }
    }
}