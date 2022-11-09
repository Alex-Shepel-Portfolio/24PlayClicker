using UnityEditor.Build;
using UnityEditor.Build.Reporting;

namespace TFPlay.BuildIncrementor
{
    public class BuildIncrementorPostprocessor : IPostprocessBuildWithReport
    {
        public int callbackOrder => 0;

        public void OnPostprocessBuild(BuildReport report)
        {
            BuildIncrementorHelper.IncreaseBuildVersion();
        }
    }
}