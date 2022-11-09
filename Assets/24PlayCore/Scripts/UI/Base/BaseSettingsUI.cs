using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TFPlay.UI
{
    public abstract class BaseSettingsUI : MonoBehaviour
    {
        [Header("Toggles")]
        [SerializeField] protected bool showSoundButton;
        [SerializeField] protected bool showVibrationButton;

        [Header("Buttons")]
        [SerializeField] protected Button settingsButton;
        [SerializeField] protected Button soundButton;
        [SerializeField] protected Button vibrationButton;

        [Header("Images")]
        [SerializeField] protected Image sound;
        [SerializeField] protected Image vibration;
        [SerializeField] protected Image background;

        [Header("Sprites")]
        [SerializeField] protected Sprite soundOn;
        [SerializeField] protected Sprite soundOff;
        [SerializeField] protected Sprite vibrationOn;
        [SerializeField] protected Sprite vibrationOff;

        public bool IsOpened { get; protected set; }

        protected abstract void TogglePanel();

        protected virtual void OnValidate()
        {
            
        }

        private void Start()
        {
            Init();
        }

        protected virtual void Init()
        {
            settingsButton.onClick.AddListener(OnSettingsButonClicked);
            soundButton.onClick.AddListener(OnSoundButtonClicked);
            vibrationButton.onClick.AddListener(OnVibrationButtonClicked);

            SetSound(SLS.Data.Settings.SoundEnabled.Value);
            SetVibration(SLS.Data.Settings.VibrationEnabled.Value);
        }

        protected virtual void OnSettingsButonClicked()
        {
            TogglePanel();
            Taptic.Selection();
        }

        protected virtual void OnSoundButtonClicked()
        {
            SetSound(!SLS.Data.Settings.SoundEnabled.Value);
            Taptic.Selection();
        }

        protected virtual void OnVibrationButtonClicked()
        {
            SetVibration(!SLS.Data.Settings.VibrationEnabled.Value);
            Taptic.Selection();
        }

        protected virtual void SetVibration(bool enable)
        {
            SLS.Data.Settings.VibrationEnabled.Value = enable;
            vibration.sprite = enable ? vibrationOn : vibrationOff;
            Taptic.tapticOn = enable;
        }

        protected virtual void SetSound(bool enable)
        {
            SLS.Data.Settings.SoundEnabled.Value = enable;
            sound.sprite = enable ? soundOn : soundOff;
            AudioListener.volume = enable ? 1f : 0f;
        }
    }
}
