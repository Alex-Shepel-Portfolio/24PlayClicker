using UnityEditor;
using System.IO;
using UnityEngine;

namespace TFPlay.BuildTools.Builder
{
    public class BuildHelper
    {
        private const string CustomMenuBasePath = "BuildHelper/";

        private const string BuildAndroidAppPackageMenuPath = CustomMenuBasePath + "Build Android App Package (apk)";
        private const string BuildAndRunAndroidAppPackageMenuPath = CustomMenuBasePath + "Build and Run Android App Package (apk)";
        private const string BuildAndroidAppBundleMenuPath = CustomMenuBasePath + "Build Android App Bundle (aab)";

        private const string BuildIOSXcodeProjectMenuPath = CustomMenuBasePath + "Build iOS Xcode Project";

        private const string OpenBuildsFolderMenuPath = CustomMenuBasePath + "Open Builds Folder";

        private const string DevelopmentBuildPrefKey = "DevelopmentBuildSettings";
        private const string DevelopmentBuildMenuPath = CustomMenuBasePath + "Development Build";

        private const string ScriptingImplementationMonoPrefKey = "ScriptingImplementationMonoSettings";
        private const string ScriptingImplementationMonoMenuPath = CustomMenuBasePath + "Mono";
        private const string ScriptingImplementationIL2CPPMenuPath = CustomMenuBasePath + "IL2CPP";

        private static bool DevelopmentBuildEnabled
        {
            get => EditorPrefs.GetBool(DevelopmentBuildPrefKey, false);
            set => EditorPrefs.SetBool(DevelopmentBuildPrefKey, value);
        }

        [MenuItem(DevelopmentBuildMenuPath, priority = 0)]
        private static void DevelopmentBuild()
        {
            DevelopmentBuildEnabled = !DevelopmentBuildEnabled;
        }

        [MenuItem(DevelopmentBuildMenuPath, true, priority = 0)]
        private static bool DevelopmentBuildValidate()
        {
            Menu.SetChecked(DevelopmentBuildMenuPath, DevelopmentBuildEnabled);
            return true;
        }

#if UNITY_ANDROID
        private static bool ScriptingImplementationMonoEnabled
        {
            get => EditorPrefs.GetBool(ScriptingImplementationMonoPrefKey, true);
            set => EditorPrefs.SetBool(ScriptingImplementationMonoPrefKey, value);
        }

        [MenuItem(ScriptingImplementationMonoMenuPath, priority = 0)]
        private static void ScriptingImplementationMono()
        {
            ScriptingImplementationMonoEnabled = true;
        }

        [MenuItem(ScriptingImplementationIL2CPPMenuPath, priority = 0)]
        private static void ScriptingImplementationIL2CPP()
        {
            ScriptingImplementationMonoEnabled = false;
        }

        [MenuItem(ScriptingImplementationMonoMenuPath, true, priority = 0)]
        private static bool ScriptingImplementationMonoValidate()
        {
            Menu.SetChecked(ScriptingImplementationMonoMenuPath, ScriptingImplementationMonoEnabled);
            return true;
        }

        [MenuItem(ScriptingImplementationIL2CPPMenuPath, true, priority = 0)]
        private static bool ScriptingImplementationIL2CPPValidate()
        {
            Menu.SetChecked(ScriptingImplementationIL2CPPMenuPath, !ScriptingImplementationMonoEnabled);
            return true;
        }

        private static ScriptingImplementation GetScriptingImplementation()
        {
            return ScriptingImplementationMonoEnabled ? ScriptingImplementation.Mono2x : ScriptingImplementation.IL2CPP;
        }

        [MenuItem(BuildAndroidAppPackageMenuPath, priority = 11)]
        private static void BuildAndroidAppPackage()
        {
            ProjectSettingsHelper.FixSettings();
            var androidBuildConfig = new AndroidBuildConfig(DevelopmentBuildEnabled, false, false, GetScriptingImplementation());
            var androidBuilder = new AndroidBuilder(androidBuildConfig);
            androidBuilder.Build();
        }

        [MenuItem(BuildAndRunAndroidAppPackageMenuPath, priority = 11)]
        private static void BuildAndRunAndroidAppPackage()
        {
            ProjectSettingsHelper.FixSettings();
            var androidBuildConfig = new AndroidBuildConfig(DevelopmentBuildEnabled, true, false, GetScriptingImplementation());
            var androidBuilder = new AndroidBuilder(androidBuildConfig);
            androidBuilder.Build();
        }

        [MenuItem(BuildAndroidAppBundleMenuPath, priority = 22)]
        private static void BuildAndroidAppBundle()
        {
            ProjectSettingsHelper.FixSettings();
            var androidBuildConfig = new AndroidBuildConfig(DevelopmentBuildEnabled, false, true, ScriptingImplementation.IL2CPP);
            var androidBuilder = new AndroidBuilder(androidBuildConfig);
            androidBuilder.Build();
        }
#endif

#if UNITY_IOS
        [MenuItem(BuildIOSXcodeProjectMenuPath, priority = 11)]
        private static void BuildIOSXcodeProject()
        {
            ProjectSettingsHelper.FixSettings();
            var iosBuildConfig = new iOSBuildConfig(DevelopmentBuildEnabled);
            var iosBuilder = new iOSBuilder(iosBuildConfig);
            iosBuilder.Build();
        }
#endif

        [MenuItem(OpenBuildsFolderMenuPath, priority = 33)]
        private static void OpenBuildsFolder()
        {
            var directoryPath = Path.GetDirectoryName(Application.dataPath);
            var buildsPath = Path.Combine(directoryPath, "Builds");
            Application.OpenURL($"file://{buildsPath}");
        }
    }
}
