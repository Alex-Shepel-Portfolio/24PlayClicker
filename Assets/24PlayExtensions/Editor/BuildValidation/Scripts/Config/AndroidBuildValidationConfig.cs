using UnityEditor;

namespace TFPlay.BuildValidation
{
    public class AndroidBuildValidationConfig : BaseBuildValidationConfig
    {
        public bool useAutoGraphicsAPI;
        public AndroidSdkVersions requiredMinSdkVersion;
        public AndroidSdkVersions requiredTargetSdkVersion;
        public ScriptingImplementation scriptingBackend;
        public AndroidArchitecture targetArchitectures;

        public static AndroidBuildValidationConfig Default = new AndroidBuildValidationConfig
        {
            showUnityLogo = false,
            useAutoGraphicsAPI = false,
            requiredMinSdkVersion = (AndroidSdkVersions)23,
            requiredTargetSdkVersion = (AndroidSdkVersions)31,
            scriptingBackend = ScriptingImplementation.IL2CPP,
            targetArchitectures = AndroidArchitecture.ARMv7 | AndroidArchitecture.ARM64
        };
    }
}
