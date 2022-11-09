using UnityEngine;
using UnityEditor;
using System.IO;

namespace TFPlay.BuildTools.Builder
{
    public class iOSBuilder : BaseBuilder<iOSBuildConfig>
    {
        public iOSBuilder(iOSBuildConfig iOSBuildConfig) : base(iOSBuildConfig)
        {

        }

        protected override BuildPlayerOptions GetBuildPlayerOptions()
        {
            var buildPlayerOptions = new BuildPlayerOptions();
            buildPlayerOptions.scenes = GetActiveScenes();
            buildPlayerOptions.locationPathName = GetBuildOutputPath();
            buildPlayerOptions.target = BuildTarget.iOS;
            buildPlayerOptions.options = GetBuildOptions();
            return buildPlayerOptions;
        }

        protected override string GetBuildPlatformTitle()
        {
            return "iOS Build";
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
            var buildFolderName = GetBuildFileName();
            var buildTitle = GetBuildPlatformTitle();
            var buildDirectory = CreateOrGetBuildDirectory();
            var buildFolderPath = Path.Combine(buildDirectory, buildFolderName);
            if (!Directory.Exists(buildFolderPath))
            {
                Directory.CreateDirectory(buildFolderPath);
            }
            var outputPath = EditorUtility.SaveFolderPanel(buildTitle, buildDirectory, buildFolderName);
            return outputPath;
        }
    }
}

