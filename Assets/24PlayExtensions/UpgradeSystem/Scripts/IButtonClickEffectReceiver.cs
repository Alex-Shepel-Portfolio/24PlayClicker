using System;

namespace TFPlay.UpgradeSystem
{
    public interface IButtonClickEffectReceiver
    {
        event Action<bool> OnClicked;
    }
}
