using UnityEngine;
using UnityEditor;

namespace TFPlay.BuildTools.Builder
{
    public class AndroidBuilder : BaseBuilder<AndroidBuildConfig>
    {
        public AndroidBuilder(AndroidBuildConfig buildConfig) : base(buildConfig)
        {
            EditorUserBuildSettings.buildAppBundle = buildConfig.buildAppBundle;
            PlayerSettings.Android.useAPKExpansionFiles = buildConfig.buildAppBundle;
            PlayerSettings.SetScriptingBackend(EditorUserBuildSettings.selectedBuildTargetGroup, buildConfig.scriptingBeckend);
            if (buildConfig.scriptingBeckend == ScriptingImplementation.IL2CPP)
            {
                PlayerSettings.Android.targetArchitectures = AndroidArchitecture.ARM64 | AndroidArchitecture.ARMv7;
            }
            else
            {
                PlayerSettings.Android.targetArchitectures = AndroidArchitecture.ARMv7;
            }
            PlayerSettings.Android.useCustomKeystore = buildConfig.buildAppBundle;
            if (buildConfig.buildAppBundle)
            {
                PlayerSettings.Android.keystorePass = "242424";
                PlayerSettings.Android.keyaliasPass = "242424";
            }
        }

        protected override BuildOptions GetBuildOptions()
        {
            var buildOptions = base.GetBuildOptions();
            if (buildConfig.buildAndRun)
            {
                buildOptions |= BuildOptions.AutoRunPlayer;
            }
            buildOptions |= BuildOptions.CompressWithLz4;
            return buildOptions;
        }

        protected override BuildPlayerOptions GetBuildPlayerOptions()
        {
            var buildPlayerOptions = new BuildPlayerOptions();
            buildPlayerOptions.scenes = GetActiveScenes();
            buildPlayerOptions.locationPathName = GetBuildOutputPath();
            buildPlayerOptions.target = BuildTarget.Android;
            buildPlayerOptions.options = GetBuildOptions();
            return buildPlayerOptions;
        }

        protected override string GetBuildPlatformTitle()
        {
            return "Android Build";
        }

        protected string GetBuildExtension()
        {
            if (buildConfig.buildAppBundle)
            {
                return "aab";
            }
            else
            {
                return "apk";
            }
        }

        protected override string GetBuildFileName()
        {
            var productName = GetCleanProductName();
            var bundleVersion = PlayerSettings.bundleVersion;
            var buildFileName = productName + "-" + bundleVersion + base.GetBuildFileName();
            return buildFileName;
        }

        protected override string GetBuildOutputPath()
        {
            var buildFileName = GetBuildFileName();
            var buildTitle = GetBuildPlatformTitle();
            var buildExtension = GetBuildExtension();
            var buildDirectory = CreateOrGetBuildDirectory();
            var outputPath = EditorUtility.SaveFilePanel(buildTitle, buildDirectory, buildFileName, buildExtension);
            return outputPath;
        }
    }
}

