#if UNITY_IOS
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.iOS.Xcode;
using System.IO;
using UnityEditor.Build.Reporting;

public class ProvideAppTrackingTransparency : IPostprocessBuildWithReport
{
    private const string TrackingDescription = "Your data will be used to provide you a better and personalized ad experience.";

    public int callbackOrder => 0;

    public void OnPostprocessBuild(BuildReport report)
    {
        if (report.summary.platform == BuildTarget.iOS)
        {
            AddNSUserTrackingUsageDescription(report.summary.outputPath);
        }
    }

    private static void AddNSUserTrackingUsageDescription(string pathToXcode)
    {
        var plistPath = pathToXcode + "/Info.plist";
        var plistObj = new PlistDocument();
        plistObj.ReadFromString(File.ReadAllText(plistPath));
        var plistRoot = plistObj.root;
        plistRoot.SetString("NSUserTrackingUsageDescription", TrackingDescription);
        File.WriteAllText(plistPath, plistObj.WriteToString());
    }
}
#endif