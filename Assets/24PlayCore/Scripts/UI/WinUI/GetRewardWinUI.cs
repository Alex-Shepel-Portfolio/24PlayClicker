using UnityEngine;

namespace TFPlay.UI
{
    public class GetRewardWinUI : WinUI
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
    }
}