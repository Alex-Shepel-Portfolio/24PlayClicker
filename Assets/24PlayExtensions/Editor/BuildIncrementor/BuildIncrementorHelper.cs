using UnityEditor;

namespace TFPlay.BuildIncrementor
{
    public class BuildIncrementorHelper
    {
        private const string AutoIncrementPrefKey = "AutoIncrementSetting";
        private const string CustomMenuBasePath = "BuildHelper/Version/";
        private const string AutoIncrementMenuPath = CustomMenuBasePath + "Auto Increment";
        private const string CreateVersionFileMenuPath = CustomMenuBasePath + "Create Version File";
        private const string IncreaseMajorVersionMenuPath = CustomMenuBasePath + "Increase Major Version";
        private const string IncreaseMinorVersionMenuPath = CustomMenuBasePath + "Increase Minor Version";
        private const string OpenBuildVersionSettingsMenuPath = CustomMenuBasePath + "Open Build Version Settings";
        private const string EqualizeBuildVersionMenuPath = CustomMenuBasePath + "Equalize Build Version";

        static BuildIncrementorHelper()
        {
            SetAutoIncrementMenuCheker();
            BuildVersionSettings.Instance.OnValuesChanged += SetAutoIncrementMenuCheker;
        }

        private static bool AutoIncrementEnabled
        {
            get => EditorPrefs.GetBool(AutoIncrementPrefKey);
            set => EditorPrefs.SetBool(AutoIncrementPrefKey, value);
        }

        public static void IncreaseBuildVersion()
        {
            if (BuildVersionSettings.Instance.AutoIncrement)
            {
                BuildVersionSettings.Instance.CurrentPlatform.IncreaseBuild();
                BuildVersionSettings.Instance.Save();
            }
        }

        private static void SetAutoIncrementMenuCheker()
        {
            AutoIncrementEnabled = BuildVersionSettings.Instance.AutoIncrement;
            Menu.SetChecked(AutoIncrementMenuPath, AutoIncrementEnabled);
        }

        [MenuItem(AutoIncrementMenuPath, priority = 101)]
        private static void AutoIncrement()
        {
            AutoIncrementEnabled = !AutoIncrementEnabled;
            BuildVersionSettings.Instance.AutoIncrement = AutoIncrementEnabled;
        }

        [MenuItem(EqualizeBuildVersionMenuPath, priority = 101)]
        private static void EqualizeBuildVersion()
        {
            BuildVersionSettings.Instance.EqualizeBuildVersion();
        }

        [MenuItem(AutoIncrementMenuPath, true)]
        private static bool AutoIncrementValidate()
        {
            Menu.SetChecked(AutoIncrementMenuPath, AutoIncrementEnabled);
            return true;
        }

        [MenuItem(CreateVersionFileMenuPath, priority = 112)]
        private static void CreateVersionFile()
        {
            BuildVersionSettings.Instance.Make();
        }

        [MenuItem(IncreaseMajorVersionMenuPath, priority = 112)]
        private static void IncreaseMajorVersion()
        {
            BuildVersionSettings.Instance.CurrentPlatform.IncreaseMajor();
            BuildVersionSettings.Instance.Save();
        }

        [MenuItem(IncreaseMinorVersionMenuPath, priority = 112)]
        private static void IncreaseMinorVersion()
        {
            BuildVersionSettings.Instance.CurrentPlatform.IncreaseMinor();
            BuildVersionSettings.Instance.Save();
        }

        [MenuItem(OpenBuildVersionSettingsMenuPath, priority = 123)]
        private static void OpenBuildVersionSettings()
        {
            AssetDatabase.OpenAsset(BuildVersionSettings.Instance);
        }
    }
}
