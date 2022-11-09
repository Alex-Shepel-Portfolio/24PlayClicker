using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace TFPlay.UI
{
    public class SettingsUI : BaseSettingsUI
    {
        protected override void TogglePanel()
        {
            IsOpened = !IsOpened;
            StopAllCoroutines();
            StartCoroutine(OpenClose(IsOpened));
        }

        private IEnumerator OpenClose(bool open)
        {
            var time = 0.2f;
            var t = 0f;
            var alpha = 0f;

            if (open)
            {
                if (showSoundButton)
                {
                    soundButton.gameObject.SetActive(true);
                }
                if (showVibrationButton)
                {
                    vibrationButton.gameObject.SetActive(true);
                }
            }

            while (t < time)
            {
                t += Time.deltaTime;
                alpha = open ? t / time : 1 - t / time;
                background.color = background.color.WithAlpha(alpha);
                sound.color = sound.color.WithAlpha(alpha);
                vibration.color = vibration.color.WithAlpha(alpha);
                soundButton.image.color = soundButton.image.color.WithAlpha(alpha);
                vibrationButton.image.color = vibrationButton.image.color.WithAlpha(alpha);
                yield return null;
            }

            alpha = open ? 1 : 0;
            background.color = background.color.WithAlpha(alpha);
            sound.color = sound.color.WithAlpha(alpha);
            vibration.color = vibration.color.WithAlpha(alpha);
            soundButton.image.color = soundButton.image.color.WithAlpha(alpha);
            vibrationButton.image.color = vibrationButton.image.color.WithAlpha(alpha);

            if (!open)
            {
                if (showSoundButton)
                {
                    soundButton.gameObject.SetActive(false);
                }
                if (showVibrationButton)
                {
                    vibrationButton.gameObject.SetActive(false);
                }
            }
        }
    }
}