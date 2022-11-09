using UnityEngine;
using UnityEditor.Build.Reporting;
using UnityEditor;
using System.IO;

namespace TFPlay.BuildTools.Builder
{
    public abstract class BaseBuilder<T> where T : BaseBuildConfig
    {
        protected T buildConfig;

        public BaseBuilder(T buildConfig)
        {
            this.buildConfig = buildConfig;
        }

        public void Build()
        {
            var buildPlayerOptions = GetBuildPlayerOptions();
            if (!string.IsNullOrEmpty(buildPlayerOptions.locationPathName))
            {
                var buildReport = BuildPipeline.BuildPlayer(buildPlayerOptions);
                HandleBuildReport(buildReport);
            }
        }

        protected abstract BuildPlayerOptions GetBuildPlayerOptions();
        protected abstract string GetBuildPlatformTitle();
        protected abstract string GetBuildOutputPath();

        protected virtual string GetBuildFileName()
        {
            return buildConfig.developmentBuild ? "-DevBuild" : "";
        }

        protected string GetCleanProductName()
        {
            var productName = Application.productName;
            var cleanProductName = string.Join("", productName.Split(Path.GetInvalidFileNameChars()));
            cleanProductName = string.Join("", cleanProductName.Split(' '));
            return cleanProductName;
        }

        protected virtual BuildOptions GetBuildOptions()
        {
            var buildOptions = BuildOptions.ShowBuiltPlayer;
            if (buildConfig.developmentBuild)
            {
                buildOptions |= BuildOptions.Development | BuildOptions.ConnectWithProfiler | BuildOptions.EnableDeepProfilingSupport;
            }
            return buildOptions;
        }

        protected string[] GetActiveScenes()
        {
            return EditorBuildSettingsScene.GetActiveSceneList(EditorBuildSettings.scenes);
        }

        protected string CreateOrGetBuildDirectory()
        {
            var buildsFolder = Path.Combine(Directory.GetCurrentDirectory(), "Builds");
            if (!Directory.Exists(buildsFolder))
            {
                Directory.CreateDirectory(buildsFolder);
            }
            return buildsFolder;
        }

        protected void HandleBuildReport(BuildReport report)
        {
            var summary = report.summary;
            if (summary.result == BuildResult.Succeeded)
            {
                var totalTime = summary.totalTime;
                Debug.Log($"Build completed with a result of 'Succeeded' in {totalTime.TotalSeconds:0} seconds ({totalTime.TotalMilliseconds:0} ms)");
            }
            if (summary.result == BuildResult.Failed)
            {
                Debug.Log("Build completed with a result of 'Failed'");
            }
        }
    }
}

