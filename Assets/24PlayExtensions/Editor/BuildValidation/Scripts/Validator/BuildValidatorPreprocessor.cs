using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace TFPlay.BuildValidation
{
    public class BuildValidatorPreprocessor : IPreprocessBuildWithReport
    {
        public int callbackOrder => 0;

        public void OnPreprocessBuild(BuildReport report)
        {
            if (Application.isBatchMode)
                return;

            if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.Android)
            {
                if (EditorUserBuildSettings.buildAppBundle)
                {
                    ValidateAndroidAABBuild();
                }
                else
                {
                    ValidateAndroidAPKBuild();
                }
            }
            else if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.iOS)
            {
                ValidateiOSBuild();
            }
        }

        private void ValidateAndroidAPKBuild()
        {
            AndroidBuildValidationConfig validationConfig = AndroidBuildValidationConfig.Default;
            AndroidBuildValidator buildValidator = new AndroidBuildValidator(validationConfig);
            buildValidator.Register(new DisableKeystoreValidationProcess());
            buildValidator.Register(new DisableSplitApplicationBinaryValidationProcess());
            buildValidator.Validate();
        }

        private void ValidateAndroidAABBuild()
        {
            AndroidBuildValidationConfig validationConfig = AndroidBuildValidationConfig.Default;
            AndroidBuildValidator buildValidator = new AndroidBuildValidator(validationConfig);
            buildValidator.Register(new UnityLogoValidationProcess());
            buildValidator.Register(new AutoGraphicsAPIValidationProcess());
            buildValidator.Register(new TargetAPILevelValidationProcess());
            buildValidator.Register(new ScriptingBeckendValidationProcess());
            buildValidator.Register(new TargetArchitecturesValidationProcess());
            buildValidator.Register(new AndroidManifestDebuggableValidationProcess());
            buildValidator.Register(new EnableKeystoreValidationProcess());
            buildValidator.Validate();
        }

        private void ValidateiOSBuild()
        {
            iOSBuildValidationConfig validationConfig = iOSBuildValidationConfig.Default;
            iOSBuildValidator buildValidator = new iOSBuildValidator(validationConfig);
            buildValidator.Register(new UnityLogoValidationProcess());
            buildValidator.Validate();
        }
    }
}