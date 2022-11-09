using UnityEditor;

namespace TFPlay.BuildValidation
{
    public class DisableKeystoreValidationProcess : BaseAndroidValidationProcess
    {
        public override void Execute(AndroidBuildValidationConfig validationConfig)
        {
            ShowMessage("Checking Keystore");
            bool keystoreEnabled = PlayerSettings.Android.useCustomKeystore;
            if (keystoreEnabled)
            {
                OnValidationProcessFailed("Disable Custom Keystore flag while building apk");
            }
        }
    }
}