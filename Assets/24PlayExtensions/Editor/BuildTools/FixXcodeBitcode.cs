#if UNITY_IOS
using System.IO;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using Xcode = UnityEditor.iOS.Xcode;

namespace TFPlay.BuildTools
{
    public class FixXcodeBitcode : IPostprocessBuildWithReport
    {
        public int callbackOrder => 998;

        public void OnPostprocessBuild(BuildReport report)
        {
            if (report.summary.platform == BuildTarget.iOS)
            {
                var projPath = report.summary.outputPath + "/Unity-iPhone.xcodeproj/project.pbxproj";

                var project = new Xcode.PBXProject();
                project.ReadFromString(File.ReadAllText(projPath));

                var mainTargetGuid = project.GetUnityMainTargetGuid();
                project.SetBuildProperty(mainTargetGuid, "ENABLE_BITCODE", "NO");

                var unityFrameWorkTargetGuid = project.GetUnityFrameworkTargetGuid();
                project.SetBuildProperty(unityFrameWorkTargetGuid, "ENABLE_BITCODE", "NO");

                File.WriteAllText(projPath, project.WriteToString());
            }
        }
    }
}
#endif