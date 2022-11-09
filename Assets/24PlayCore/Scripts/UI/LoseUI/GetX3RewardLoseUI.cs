using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TFPlay.UI
{
    public class GetX3RewardLoseUI : BaseRewardLoseUI
    {
        [SerializeField] private UILevelScore score;
        [SerializeField] private float scoreAnimationDelay = 1f;

        public override void Show()
        {
            base.Show();
            score.AnimateScore(scoreAnimationDelay);
            // Set Score here
            score.SetScore(100);
        }

        protected override void OnNoThanksButtonClicked()
        {
            base.OnNoThanksButtonClicked();
            Taptic.Selection();
        }

        protected override void OnRewardedAdShown()
        {
            base.OnRewardedAdShown();
            score.AnimateScore(scoreAnimationDelay);
            // Put get reward logic here
            score.SetScore(2 * 100);
        }

        protected override void OnRewardButtonClicked()
        {
#if UNITY_EDITOR
            OnRewardedAdShown();
            return;
#endif
            // Put show reward logic here

            Taptic.Selection();
        }
    }
}
