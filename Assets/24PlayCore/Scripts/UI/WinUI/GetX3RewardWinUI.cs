using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace TFPlay.UI
{
    public class GetX3RewardWinUI : BaseRewardWinUI
    {
        [SerializeField] protected UILevelScore score;
        [SerializeField] protected float scoreAnimationDelay = 1f;

        public override void Show()
        {
            base.Show();
            score.AnimateScore(scoreAnimationDelay);
            // Set Score here
            score.SetScore(100);
        }

        protected override void NoThanksButtonClicked()
        {
            base.NoThanksButtonClicked();
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