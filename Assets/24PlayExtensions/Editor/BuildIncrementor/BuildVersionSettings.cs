using UnityEngine;
using UnityEditor;

namespace TFPlay.BuildIncrementor
{
    [InitializeOnLoad]
    public class BuildVersionSettings : ResourceSingleton<BuildVersionSettings>
    {
        [SerializeField]
        private bool autoIncrement = true;
        [SerializeField]
        private AndroidBuildPlatformSettings Android;
        [SerializeField]
        private iOSBuildPlatformSettings IOS;

        public BuildPlatformSettings CurrentPlatform
        {
            get
            {
#if UNITY_ANDROID
                return Android;
#elif UNITY_IOS
                return IOS;
#else
                return new BuildPlatformSettings();
#endif
            }
        }

        public event System.Action OnValuesChanged;

        public bool AutoIncrement
        {
            get { return autoIncrement; }
            set { autoIncrement = value; }
        }

        private void OnValidate()
        {
            UpdatePlatformVersionNumber();
            OnValuesChanged?.Invoke();
        }

        protected override void OnInstanceLoaded()
        {
            UpdatePlatformVersionNumber();
        }

        public void EqualizeBuildVersion()
        {
            var androidVersionCode = Android.GetVersionCode();
            var iosVersionCode = IOS.GetVersionCode();
            if (androidVersionCode > iosVersionCode)
            {
                IOS.MajorVersion = Android.MajorVersion;
                IOS.MinorVersion = Android.MinorVersion;
                IOS.BuildVersion = Android.BuildVersion - 1;
            }
            else if (iosVersionCode > androidVersionCode)
            {
                Android.MajorVersion = IOS.MajorVersion;
                Android.MinorVersion = IOS.MinorVersion;
                Android.BuildVersion = IOS.BuildVersion - 1;
            }
        }

        private void UpdatePlatformVersionNumber()
        {
            CurrentPlatform.UpdateVersionNumber();
            Save();
        }
    }
}
